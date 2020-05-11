using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class Segment
    {
        private double longitude;
        private double latitude;
        private int timespanSeconds;
        public Segment(double longi, double lat, int tSpan)
        {
            longitude = longi;
            latitude = lat;
            timespanSeconds = tSpan;
        }
        [JsonProperty("longitude")]
        public double Longitude
        {
            set
            {
                longitude = value;
            }
            get
            {
                return longitude;
            }
        }
        [JsonProperty("latitude")]
        public double Latitude
        {
            set
            {
                latitude = value;
            }
            get
            {
                return latitude;
            }
        }
        [JsonProperty("timespan_seconds")]
        public int TimespanSeconds
        {
            set
            {
                timespanSeconds = value;
            }
            get
            {
                return timespanSeconds;
            }
        }

    }
}
