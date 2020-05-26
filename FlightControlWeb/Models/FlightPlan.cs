using Newtonsoft.Json;
using System.Collections.Generic;

namespace FlightControlWeb.Models
{
    public class FlightPlan
    {
        public FlightPlan(int pass, string company,
            InitialLocation initialLoc, List<Segment> seg)
        {
            Passengers = pass;
            CompanyName = company;
            InitialLocation = initialLoc;
            Segments = seg;
        }

        [JsonProperty("passengers")]
        public int Passengers { set; get; }
        [JsonProperty("company_name")]
        public string CompanyName { set; get; }
        [JsonProperty("initial_location")]
        public InitialLocation InitialLocation { set; get; }
        [JsonProperty("segments")]
        public List<Segment> Segments { set; get; }

        public bool IsNull()
        {
            if (Passengers == null || CompanyName == null 
                || InitialLocation == null || InitialLocation.IsNull() 
                || Segments == null || SegmentNull(Segments))
            {
                return true;
            }
            return false;
        }

        public bool SegmentNull(List<Segment> segments)
        {
            foreach(Segment s in segments)
            {
                if(s == null || s.IsNull())
                {
                    return true;
                }
            }
            return false;
        }

       
    }
}
