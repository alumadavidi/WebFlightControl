using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class Flight
    {
        private string flightId;
        private double longitude;
        private double latitude;
        private int passengers;
        private string companyName;
        private string dateTime;
        private bool is_external;
        [JsonConstructor]
        public Flight(string flightId, double longitude, double latitude, int passengers,
            string companyName, string dateTime, bool isExternal)
        {
            this.flightId = flightId;
            this.longitude = longitude;
            this.latitude = latitude;
            this.passengers = passengers;
            this.companyName = companyName;
            this.dateTime = dateTime;
            this.is_external = isExternal;
        }
        [JsonProperty("flight_Id")]
        public string Flight_Id
        {
            get
            {
                return flightId;
            }
            set
            {
                flightId = value;
            }
        }
        [JsonProperty("longitude")]
        public double Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                longitude = value;
            }
        }
        [JsonProperty("latitude")]
        public double Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                latitude = value;
            }
        }
        [JsonProperty("passengers")]
        public int Passengers
        {
            get
            {
                return passengers;
            }
            set
            {
                passengers = value;
            }
        }
        [JsonProperty("company_name")]
        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;
            }
        }
        [JsonProperty("date_time")]
        public string DateTime
        {
            get
            {
                return dateTime;
            }
            set
            {
                dateTime = value;
            }
        }
        [JsonProperty("is_external")]
        public bool IsExternal
        {
            get
            {
                return is_external;
            }
            set
            {
                is_external = value;
            }
        }
    }
}
