using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class InitialLocation
    {
        private double longitude;
        private double latitude;
        private string dateTime;
        public InitialLocation(double longi, double lat, string data)
        {
            longitude = longi;
            latitude = lat;
            dateTime = data;
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
        [JsonProperty("date_time")]
        public string DateTime
        {
            set
            {
                dateTime = value;
            }
            get
            {
                return dateTime;
            }
        }

        public bool IsNull()
        {
           if(Longitude == null || Latitude == null || DateTime == null)
            {
                return true;
            }
            return false;
        }
    }
}

