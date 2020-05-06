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
        private int timespan_seconds;
        public Segment(double longi, double lat, int tSpan)
        {
            longitude = longi;
            latitude = lat;
            timespan_seconds = tSpan;
        }
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
        public int TimespanSeconds
        {
            set
            {
                timespan_seconds = value;
            }
            get
            {
                return timespan_seconds;
            }
        }

    }
}
