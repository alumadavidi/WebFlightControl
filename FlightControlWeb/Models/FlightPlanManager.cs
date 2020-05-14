using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightPlanManager
    {
        private SqliteDB db = SqliteDB.Instance;
        
       
        public void AddNewFlightPlan(FlightPlan flightPlan)
        {
            if (flightPlan == null || flightPlan.isNull() ||
                    !TimeFunc.ValidStringDate(flightPlan.InitialLocation.DateTime))
            {
                throw new Exception();
            }
            db.AddFlightPlan(flightPlan, createId());
        }
        public FlightPlan GetFlightPlanById(string id)
        {
            return db.GetFlightPlanById(id);
        }
        public void Test()
        {
            List<Segment> s0 = new List<Segment>()
            {
            new Segment(33.234, 32,700),
                new Segment(33.23, 31.1,700),
                new Segment(254, 31.18,700)
            };

            FlightPlan f11 = new FlightPlan(216, "swir1",
            new InitialLocation(33.244, 31.12, "2020-05-07T13:40:21Z"),
            s0);
            FlightManager flightManager = new FlightManager();
            db.AddFlightPlan(f11, createId());
            flightManager.GetFlightsFromServer("2020-05-07T13:46:35Z");

        }
        private string createId()
        {
            string id = "";
            string randomChar;
            Random rnd = new Random();
            for (int i = 0; i < 8; i++)
            {
                if (i < 2)
                {
                    randomChar = ""+(char)rnd.Next('A', 'Z');
                } else
                {
                    randomChar = rnd.Next(0, 9).ToString();
                }
                id += randomChar;
            }
            return id;
        }
    }
}
