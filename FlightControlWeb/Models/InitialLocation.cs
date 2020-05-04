using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class InitialLocation
    {
        private double longitude;
        private double latitude;
        private string dataTime;
        public InitialLocation(double longi, double lat, string data)
        {
            longitude = longi;
            latitude = lat;
            dataTime = data;
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
        public string DataTime
        {
            set
            {
                dataTime = value;
            }
            get
            {
                return dataTime;
            }
        }

    }
}

