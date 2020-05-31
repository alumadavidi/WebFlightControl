using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;

namespace FlightControlWeb.Models
{
    public class SqliteDB : IDataManager
    {
        private readonly SQLiteConnection connection;

        public SqliteDB()
        {
            string cs = "DataSource=FlightControl.db;";
            if (!File.Exists("FlightControl.db"))
            {

                //open connection
                connection = new SQLiteConnection(cs);
                connection.Open();

                try
                {
                    BuildFlightPlanTable();
                    BuildSegmentTable();
                    BuildServerTable();
                }
                catch
                {
                    Console.WriteLine("FAILED IN BUILD DB");
                }
            }
            else
            {
                connection = new SQLiteConnection(cs);
                connection.Open();
            }
        }
        private void BuildFlightPlanTable()
        {
            using var cmd = new SQLiteCommand(connection);
            //create table
            cmd.CommandText = "DROP TABLE IF EXISTS FlightPlanTable";
            int s= cmd.ExecuteNonQuery();
            //add column
            cmd.CommandText = @"CREATE TABLE FlightPlanTable(id TEXT PRIMARY KEY,
                    companyName TEXT, passengers INT, dateTime TEXT, longitude DOUBLE,
                    latitude DOUBLE)";
            s = cmd.ExecuteNonQuery();
        }
        private void BuildSegmentTable()
        {
            using var cmd = new SQLiteCommand(connection);
            //create table
            cmd.CommandText = "DROP TABLE IF EXISTS SegmentTable";
            int s = cmd.ExecuteNonQuery();
            //add column
            cmd.CommandText = @"CREATE TABLE SegmentTable(id TEXT,
                    longitude DOUBLE, latitude DOUBLE, timespanSeconds INT)";
            s = cmd.ExecuteNonQuery();
        }


        private void BuildServerTable()
        {
            using var cmd = new SQLiteCommand(connection);
            //create table
            cmd.CommandText = "DROP TABLE IF EXISTS ServerTable";
            int s = cmd.ExecuteNonQuery();
            //add column
           
            cmd.CommandText = @"CREATE TABLE ServerTable(
                id TEXT NOT NULL,
                url TEXT NOT NULL,
                PRIMARY KEY(id, url)
                );";
            s = cmd.ExecuteNonQuery();
        }

        public void AddServer(ServerFlight s)
        {
            using var cmd = new SQLiteCommand(connection);
            string a = " VALUES(" + "\'" + s.ServerId + "\',\'" + s.ServerUrl + "\')";
            cmd.CommandText = "INSERT INTO ServerTable(id,url)" + a;
                
            int suc = cmd.ExecuteNonQuery();
            //0 - not insert, -1 exception
            if (suc == 0 || suc == -1)
            {
                throw new Exception();
            }
         
        }


        public void AddFlightPlan(FlightPlan f, string id)
        {

            using var cmd = new SQLiteCommand(connection);
            string a = " VALUES(" + "\'" + id + "\',\'" + f.CompanyName + "\'," +
                f.Passengers+ ",\'" + f.InitialLocation.DateTime + "\',"+
                f.InitialLocation.Longitude+","+ f.InitialLocation.Latitude+")";
            cmd.CommandText = "INSERT INTO FlightPlanTable(id, " +
                "companyName, passengers, dateTime, longitude, latitude )" + a;
            int suc = cmd.ExecuteNonQuery();
            //0 - not insert, -1 exception
            if (suc == 0 || suc == -1)
            {
                throw new Exception();
            } else
            {
                AddSegmentOfFlight(f.Segments, id);
            }
        }

        private void AddSegmentOfFlight(List<Segment> segments, string id)
        {
            //add segments of flight plan to DB
            foreach (Segment s in segments)
            {
                using var cmd = new SQLiteCommand(connection);
                string a = " VALUES(" + "\'" + id + "\'," + s.Longitude + "," +
                    s.Latitude + "," + s.TimespanSeconds+ ")";
                cmd.CommandText = "INSERT INTO SegmentTable(id,longitude," +
                    " latitude, timespanSeconds)" + a;
                int suc = cmd.ExecuteNonQuery();
                //0 - not insert, -1 exception
                if (suc == 0 || suc == -1)
                {
                    throw new Exception();
                }
                
            }
        }


        public List<ServerFlight> GetServers()
        {
            //get all server fron DB
            List<ServerFlight> serverFlights = new List<ServerFlight>();
            string stm = "SELECT * FROM ServerTable";

            using var cmd1 = new SQLiteCommand(stm, connection);
            using SQLiteDataReader rdr = cmd1.ExecuteReader();

            while (rdr.Read())
            {
                serverFlights.Add(
                    new ServerFlight(rdr.GetString(0), rdr.GetString(1)));
            }
            return serverFlights;
        }

        public List<FlightPlanId> GetFlightPlans()
        {
            //get all flight plan from DB
            List<FlightPlanId> flights = new List<FlightPlanId>();
            string stm = "SELECT * FROM FlightPlanTable";

            using var cmd1 = new SQLiteCommand(stm, connection);
            using SQLiteDataReader rdr = cmd1.ExecuteReader();
           
            //(id, companyName, passengers, dateTime, longitude, latitude )
            while (rdr.Read())
            {
                List<Segment> seg = GetSegmentById(rdr.GetString(0));
                InitialLocation initLoc = new InitialLocation(
                    rdr.GetDouble(4), rdr.GetDouble(5), rdr.GetString(3));
                flights.Add(new FlightPlanId(rdr.GetString(0), new FlightPlan(
                    rdr.GetInt32(2), rdr.GetString(1), initLoc, seg)));
            }
            return flights;
        }
        //throw exception if not found
        public FlightPlan GetFlightPlanById(string id)
        {
            //get flight plan from DB by specific id
            FlightPlan f = null;
            string stm = "SELECT * FROM FlightPlanTable WHERE id=\'" + id + "\'";

            using var cmd1 = new SQLiteCommand(stm, connection);
            using SQLiteDataReader rdr = cmd1.ExecuteReader();
            if (rdr.Read())
            {
                List<Segment> seg = GetSegmentById(rdr.GetString(0));

                InitialLocation initLoc = new InitialLocation(
                    rdr.GetDouble(4), rdr.GetDouble(5), rdr.GetString(3));
                f = new FlightPlan(rdr.GetInt32(2),
                    rdr.GetString(1), initLoc, seg);
            }
            return f;
        }

        public ServerFlight GetServerById(string id)
        {
            //get server from DB by specific id
            ServerFlight f = null;
            string stm = "SELECT * FROM ServerTable WHERE id=\'" + id + "\'";

            using var cmd1 = new SQLiteCommand(stm, connection);
            using SQLiteDataReader rdr = cmd1.ExecuteReader();
            if (rdr.Read())
            {
                f = new ServerFlight(rdr.GetString(0), rdr.GetString(1));
            }
            return f;
        }

        private List<Segment> GetSegmentById(string id)
        {
            //get segments from DB by specific id
            List<Segment> segments = new List<Segment>();

            string stm = "SELECT * FROM SegmentTable WHERE id=\'"+id+"\'";

            using var cmd1 = new SQLiteCommand(stm, connection);
            using SQLiteDataReader rdr = cmd1.ExecuteReader();

            while (rdr.Read())
            {
                //(id,longitude, latitude, timespanSeconds)
                segments.Add(new Segment(rdr.GetDouble(1),rdr.GetDouble(2),
                    rdr.GetInt32(3)));
            }
            return segments;
        }

        public void RemoveFlightPlan(string id)
        {
            //remove flight plan from DB by specific id
            string stm = "DELETE FROM FlightPlanTable WHERE id=\'" + id+"\'";

            using var cmd1 = new SQLiteCommand(stm, connection);
            int s = cmd1.ExecuteNonQuery();
            //0 - not found, -1 exception
            if (s == 0 || s == -1) {
                throw new Exception();
            } else
            {
                RemoveSegments(id);
            }
           
        }

        private void RemoveSegments(string id)
        {
            //remove segments from DB by specific id
            string stm = "DELETE FROM SegmentTable WHERE id=\'" + id+"\'";

            using var cmd1 = new SQLiteCommand(stm, connection);
            int s = cmd1.ExecuteNonQuery();
            //0 - not found, -1 exception
            if (s == 0 || s == -1)
            {
                throw new Exception();
            }
        }

        public void RemoveServer(string id)
        {
            //remove server from DB by specific id
            string stm = "DELETE FROM ServerTable WHERE id=\'" + id + "\'";

            using var cmd1 = new SQLiteCommand(stm, connection);
            int s = cmd1.ExecuteNonQuery();
            //0 - not found, -1 exception
            if (s == 0 || s == -1)
            {
                throw new Exception();
            }
        }


        public static string LoadConnectionStrings(string id = "default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;

        }

        public static void Fun2()
        {
            string cs = "Data Source=:memory:";
            string stm = "SELECT SQLITE_VERSION()";

            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(stm, con);
            string version = cmd.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version: {version}");
        }

       


    }
}
