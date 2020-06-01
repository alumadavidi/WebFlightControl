using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class InitialLocation
    {
        public InitialLocation(double longi, double lat, string data)
        {
            Longitude = longi;
            Latitude = lat;
            DateTime = data;
        }
        [JsonProperty("longitude")]
        public double Longitude { set; get; }
        [JsonProperty("latitude")]
        public double Latitude { set; get; }
        [JsonProperty("date_time")]
        public string DateTime { set; get; }

        public bool IsNull()
        {
            //check validation
           if(DateTime == null || Longitude > 180
                    || Longitude < -180 || Latitude > 90 || Latitude < -90 ||
                    !TimeFunc.ValidStringDate(DateTime))
            {
                return true;
            }
            return false;
        }
    }
}

