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
        private string companyName;
        private InitialLocation initialLocation;
        private List<Segment> segments;
        public FlightPlan(string id, int pass, string company,
            InitialLocation initialLoc, List<Segment> seg)
        {
            flightPlanId = id;
            passengers = pass;
            companyName = company;
            initialLocation = initialLoc;
            segments = seg;
        }
        public string FlightPlanId
        {
            set
            {
                flightPlanId = value;
            }
            get
            {
                return flightPlanId;
            }
        }
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
                companyName = value;
            }
            get
            {
                return companyName;
            }
        }
        public InitialLocation InitialLocationFlight
        {
            set
            {
                initialLocation = value;
            }
            get
            {
                return initialLocation;
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
