using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightPlanId
    {
        private FlightPlan flightPlan;
        private string id;
        public FlightPlanId(string id, FlightPlan f)
        {
            this.id = id;
            this.flightPlan = f;

        }
        public FlightPlan FlightP
        {
            get
            {
                return flightPlan;
            }
            set
            {
                flightPlan = value;
            }
        }

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
    }
}
