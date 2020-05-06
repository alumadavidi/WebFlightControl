using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightPlanManager
    {
        private SqliteDB db = SqliteDB.Instance;
        
       
        public FlightPlan AddNewFlightPlan(FlightPlan flightPlan)
        {
            db.AddFlightPlan(flightPlan, createId());
            return flightPlan;
        }
        public FlightPlan GetFlightPlanById(string id)
        {
            return db.GetFlightPlanById(id);
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
