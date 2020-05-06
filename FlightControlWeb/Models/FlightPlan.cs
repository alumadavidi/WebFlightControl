using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightPlan
    {
        private string flightPlanId;
        private int passengers;
        private string company_name;
        private InitialLocation initial_location;
        private List<Segment> segments;
        public FlightPlan(int pass, string company,
            InitialLocation initialLoc, List<Segment> seg)
        {
            //flightPlanId = id;
            passengers = pass;
            company_name = company;
            initial_location = initialLoc;
            segments = seg;
        }
        //public string FlightPlanId
        //{
        //    set
        //    {
        //        flightPlanId = value;
        //    }
        //    get
        //    {
        //        return flightPlanId;
        //    }
        //}
        public int Passengers
        {
            set
            {
                passengers = value;
            }
            get
            {
                return passengers;
            }
        }
        public string CompanyName
        {
            set
            {
                company_name = value;
            }
            get
            {
                return company_name;
            }
        }
        public InitialLocation InitialLocationFlight
        {
            set
            {
                initial_location = value;
            }
            get
            {
                return initial_location;
            }
        }
        public List<Segment> Segments
        {
            set
            {
                segments = value;
            }
            get
            {
                return segments;
            }
        }
    }
}
