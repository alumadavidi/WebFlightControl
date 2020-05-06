using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace FlightControlWeb.Models
{
    public class InitialLocation
    {
        private double longitude;
        private double latitude;
        private string date_time;
        public InitialLocation(double longi, double lat, string data)
        {
            longitude = longi;
            latitude = lat;
            date_time = data;
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
                date_time = value;
            }
            get
            {
                return date_time;
            }
        }

    }
}

