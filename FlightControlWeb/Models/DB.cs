using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class DB
    {
        private static DB instance;
        private SqlDataAdapter dataAdapter;
        private DataSet datatSet;
        private SqlConnection connection;

        private DB()
        {
            dataAdapter = new SqlDataAdapter();
            connection = new SqlConnection("Data Source=(local);" +
                "Initial Catalog = SpringfieldDB;Integrated Security = SSPI");
            //create DB
            datatSet = new DataSet();
            //create tables
            BuildFlightPlanTable();
            BuildSegmentTable();
            BuildServerTable();
            //update sql server
            
        }
        public static DB Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DB();
                }
                return instance;
            }
        }
        
      

        private void BuildFlightPlanTable()
        {
            DataTable flightPlanTable = new DataTable("FlightPlanTable");
            //primary key
            DataColumn id = flightPlanTable.Columns.Add("FlightPlanID", typeof(string));
            id.AllowDBNull = false;
            id.Unique = true;
            //other column
            flightPlanTable.Columns.Add("CompanyName", typeof(string));
            flightPlanTable.Columns.Add("Passengers", typeof(Int32));
            flightPlanTable.Columns.Add("DateTime", typeof(string));
            flightPlanTable.Columns.Add("Longitude", typeof(double));
            flightPlanTable.Columns.Add("Latitude", typeof(double));

            //add table to DB
            datatSet.Tables.Add(flightPlanTable);
            dataAdapter.Update(datatSet, "FlightPlanTable");
        }

        private void BuildSegmentTable()
        {
            DataTable segmentTable = new DataTable("SegmentTable");
            //primary key
            DataColumn id = segmentTable.Columns.Add("FlightPlanID", typeof(string));
            id.AllowDBNull = false;
            id.Unique = true;
            //other column
            segmentTable.Columns.Add("Longitude", typeof(double));
            segmentTable.Columns.Add("Latitude", typeof(double));
            segmentTable.Columns.Add("TimespanSeconds", typeof(Int32));

            //add table to DB
            datatSet.Tables.Add(segmentTable);

            //add relation between segment and flightplan
            DataRelation dr = new DataRelation("FlightPlanTableSegmentTable",
                datatSet.Tables["FlightPlanTable"].Columns["FlightPlanID"],
                datatSet.Tables["SegmentTable"].Columns["FlightPlanID"]);
            datatSet.Relations.Add(dr);
            dataAdapter.Update(datatSet, "SegmentTable");
        }

        private void BuildServerTable()
        {
            DataTable serverTable = new DataTable("ServerTable");
            //primary key
            DataColumn id = serverTable.Columns.Add("ServerID", typeof(string));
            id.AllowDBNull = false;
            id.Unique = true;
            //other column
            serverTable.Columns.Add("ServerURL", typeof(string));

            //add table to DB
            datatSet.Tables.Add(serverTable);
            dataAdapter.Update(datatSet, "ServerTable");
        }

        public void AddServer(ServerFlight s)
        {
            //create new row
            DataRow row = datatSet.Tables["ServerTable"].NewRow();
            row["ServerID"] = s.ServerId;
            row["ServerURL"] = s.ServerUrl;
            datatSet.Tables["ServerTable"].Rows.Add(row);
            //update server sql
            dataAdapter.Update(datatSet, "ServerTable");
        }

        public void AddFlightPlan(FlightPlan f)
        {
            //create new row
            DataRow row = datatSet.Tables["FlightPlanTable"].NewRow();
            row["FlightPlanID"] = f.FlightPlanId;
            row["CompanyName"] = f.CompanyName;
            row["Passengers"] = f.Passengers;
            row["DateTime"] = f.InitialLocationFlight.DataTime;
            row["Longitude"] = f.InitialLocationFlight.Longitude;
            row["Latitude"] = f.InitialLocationFlight.Latitude;
            //update server sql
            dataAdapter.Update(datatSet, "FlightPlanTable");
            AddSegmentOfFlight(f.Segments, f.FlightPlanId);
        }

        private void AddSegmentOfFlight(List<Segment> segments, string id)
        {
            foreach (Segment s in segments)
            {
                //create new row
                DataRow row = datatSet.Tables["SegmentTable"].NewRow();
                row["FlightPlanID"] = id;
                row["Longitude"] = s.Longitude;
                row["Latitude"] = s.Latitude;
                row["TimespanSeconds"] = s.TimespanSeconds;
            }
            //update server sql
            dataAdapter.Update(datatSet, "SegmentTable");
        }

        public List<ServerFlight> GetServers()
        {
            List<ServerFlight> serverFlights = new List<ServerFlight>();
            SqlCommand command = new SqlCommand("select * from ServerTable", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Call Read before accessing data.
            while (reader.Read())
            {
                serverFlights.Add(
                    new ServerFlight((string)reader["ServerID"], (string)reader["ServerURL"]));
            }
            // Call Close when done reading.
            reader.Close();
            return serverFlights;
        }

        public List<FlightPlan> GetFlightPlans()
        {
            List<FlightPlan> flights = new List<FlightPlan>();
            SqlCommand command = new SqlCommand("select * from FlightPlanTable", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Call Read before accessing data.
            while (reader.Read())
            {
                string id = (string)reader["FlightPlanID"];
                int pass = (Int32)reader["Passengers"];
                string company = (string)reader["CompanyName"];

                InitialLocation initLoc = new InitialLocation(
                    (double)reader["Longitude"], (double)reader["Latitude"],
                   (string)reader["DateTime"]);

                List<Segment> seg = GetSegments(id);
                flights.Add(new FlightPlan(id, pass, company, initLoc, seg));
            }
            // Call Close when done reading.
            reader.Close();
            return flights;
        }

        public List<Segment> GetSegments(string id)
        {
            List<Segment> segments = new List<Segment>();

            SqlCommand command = new SqlCommand("select * from SegmentTable where FlightPlanID="+id,
                connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Call Read before accessing data.
            while (reader.Read())
            {
                segments.Add(new Segment((double)reader["Longitude"],
                    (double)reader["Latitude"], (Int32)reader["TimespanSeconds"]));
            }
            // Call Close when done reading.
            reader.Close();

            return segments;
        }

        public void RemoveFlightPlan(string id)
        {
            SqlCommand command = new SqlCommand("delete from FlightPlanTable where FlightPlanID=" + id,
                connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            RemoveSegments(id);
        }

        public void RemoveSegments(string id)
        {
            SqlCommand command = new SqlCommand("delete from SegmentTable where FlightPlanID=" + id,
                connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveServer(string id)
        {
            SqlCommand command = new SqlCommand("delete from ServerTable where ServerID=" + id,
                connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public FlightPlan GetFlightPlanById(string id)
        {
            SqlCommand command = new SqlCommand("select * from FlightPlanTable where FlightPlanID=" + id, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Call Read before accessing data.

            
            id = (string)reader["FlightPlanID"];
            int pass = (Int32)reader["Passengers"];
            string company = (string)reader["CompanyName"];

            InitialLocation initLoc = new InitialLocation(
                (double)reader["Longitude"], (double)reader["Latitude"],
                (string)reader["DateTime"]);

            List<Segment> seg = GetSegments(id);
            FlightPlan f = new FlightPlan(id, pass, company, initLoc, seg));
            // Call Close when done reading.
            reader.Close();
            return f;
        }
    }
}
