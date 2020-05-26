using FlightControlWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class SqliteDB : IDataManager
    {
       // private static SqliteDB instance;
        private SQLiteConnection connection;
        //private DataSet datatSet;
        //private SqlConnection connection;
        //public static SqliteDB Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new SqliteDB();
        //        }
        //        return instance;
        //    }
        //}
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
                } catch
                {
                    Console.WriteLine("FAILED IN BUILD DB");
                }
            } else
            {
                connection = new SQLiteConnection(cs);
                connection.Open();
            }

            ////create DB
            //string cs = "DataSource=FlightControl.db;";
            ////open connection
            //connection = new SQLiteConnection(cs);
            //connection.Open();
            
            //BuildFlightPlanTable();
            //BuildSegmentTable();
            //BuildServerTable();


            //AddServer(new ServerFlight("12345", "www.ghh"));
            //List<Segment> s2 = new List<Segment>()
            // {
            //    new Segment(5, 6,650),
            //    new Segment(8, 9,650)
            // };
            //FlightPlanManager flightPlanManager = new FlightPlanManager();
            //FlightPlan f = new FlightPlan("123450", 216, "swir1",
            //    new InitialLocation(33.244, 31.12, "2020-12-26T23:56:21Z5"),
            //    s2);
            //FlightPlan f1 = new FlightPlan("123451", 216, "swir1",
            //   new InitialLocation(33.244, 31.122, "2020-12-26T23:56:21Z8"),
            //   s2);
            //AddFlightPlan(f);
            //AddFlightPlan(f1);
            //GetServers();
            //GetFlightPlans();
            //GetFlightPlanById("123451");
            //RemoveFlightPlan("123450");


        //using var cmd = new SQLiteCommand(connection);

            //cmd.CommandText = "DROP TABLE IF EXISTS cars";
            //cmd.ExecuteNonQuery();

            //cmd.CommandText = @"CREATE TABLE cars(id INTEGER PRIMARY KEY,
            //        name TEXT, price INT)";
            //cmd.ExecuteNonQuery();

            //cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Skoda',9000)";
            //cmd.ExecuteNonQuery();

            //string stm = "SELECT * FROM cars LIMIT 5";

            //using var cmd1 = new SQLiteCommand(stm, connection);
            //using SQLiteDataReader rdr = cmd1.ExecuteReader();

            //while (rdr.Read())
            //{
            //    Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetInt32(2)}");
            //}

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
            cmd.CommandText = @"CREATE TABLE ServerTable(id TEXT PRIMARY KEY,
                    url TEXT)";
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

            //string stm = "SELECT * FROM ServerTable LIMIT 5";

            //using var cmd1 = new SQLiteCommand(stm, connection);
            //using SQLiteDataReader rdr = cmd1.ExecuteReader();

            //while (rdr.Read())
            //{
            //    Console.WriteLine($"{rdr.GetString(0)} {rdr.GetString(1)}");
            //}
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
           
            //string stm = "SELECT * FROM FlightPlanTable LIMIT 5";

            //using var cmd1 = new SQLiteCommand(stm, connection);
            //using SQLiteDataReader rdr = cmd1.ExecuteReader();

            //while (rdr.Read())
            //{
            //    Console.WriteLine($"{rdr.GetString(0)} {rdr.GetString(1)} " +
            //        $"{rdr.GetInt32(2)} {rdr.GetString(3)} "+
            //        $"{rdr.GetDouble(4)} {rdr.GetDouble(5)}");
            //}


        }

        private void AddSegmentOfFlight(List<Segment> segments, string id)
        {
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

            //string stm = "SELECT * FROM SegmentTable LIMIT 5";

            //using var cmd1 = new SQLiteCommand(stm, connection);
            //using SQLiteDataReader rdr = cmd1.ExecuteReader();
            //while (rdr.Read())
            //{
            //    Console.WriteLine($"{rdr.GetString(0)} {rdr.GetDouble(1)} " +
            //        $"{rdr.GetDouble(2)} {rdr.GetInt32(3)} ");
            //}
        }


        public List<ServerFlight> GetServers()
        {
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

        //
        public List<FlightPlanId> GetFlightPlans()
        {
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

        public static void fun2()
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
