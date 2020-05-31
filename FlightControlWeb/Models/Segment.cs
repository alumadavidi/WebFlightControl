using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class Segment
    {
        public Segment(double longi, double lat, int tSpan)
        {
            Longitude = longi;
            Latitude = lat;
            TimespanSeconds = tSpan;
        }
        [JsonProperty("longitude")]
        public double Longitude { set; get; }
        [JsonProperty("latitude")]
        public double Latitude { set; get; }
        [JsonProperty("timespan_seconds")]
        public int TimespanSeconds { set; get; }

        public bool IsNull()
        {
            //check validation
            if(Longitude == null || Latitude == null || TimespanSeconds == null)
            {
                return true;
            }
            return false;
        }
    }
}
