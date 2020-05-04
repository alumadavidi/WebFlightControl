using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightPlanManager
    {
        private DB db = DB.Instance;
       
        public FlightPlan AddNewFlightPlan(FlightPlan flightPlan)
        {
            db.AddFlightPlan(flightPlan);
            return flightPlan;
        }
        public FlightPlan GetFlightPlanById(string id)
        {
            return db.GetFlightPlanById(id);
        }

        public string createIdPlan()
        {
            //TODO: implement
            return null;
        }
    }
}
