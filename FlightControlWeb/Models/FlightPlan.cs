using Newtonsoft.Json;
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
        public FlightPlan(int pass, string company,
            InitialLocation initialLoc, List<Segment> seg)
        {
            //flightPlanId = id;
            passengers = pass;
            companyName = company;
            initialLocation = initialLoc;
            segments = seg;
        }

        [JsonProperty("passengers")]
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
        [JsonProperty("company_name")]
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
        [JsonProperty("initial_location")]
        public InitialLocation InitialLocation
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
        [JsonProperty("segments")]
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
