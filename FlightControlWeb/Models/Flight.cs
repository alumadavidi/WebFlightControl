using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class Flight
    {
        [JsonConstructor]
        public Flight(string flightId, double longitude, double latitude, int passengers,
            string companyName, string dateTime, bool isExternal)
        {
            Flight_Id = flightId;
            //convert 
            if(longitude > 180)
            {
                longitude = 180;
            }
            if(longitude < -180)
            {
                longitude = -180;
            }
            Longitude = longitude;
            if (latitude > 90)
            {
                latitude = 90;
            }
            if (latitude < -90)
            {
                latitude = -90;
            }
            Latitude = latitude;
            Passengers = passengers;
            CompanyName = companyName;
            DateTime = dateTime;
            IsExternal = isExternal;
        }
        [JsonProperty("flight_Id")]
        public string Flight_Id { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("passengers")]
        public int Passengers { get; set; }
        [JsonProperty("company_name")]
        public string CompanyName { get; set; }
        [JsonProperty("date_time")]
        public string DateTime { get; set; }
        [JsonProperty("is_external")]
        public bool IsExternal { get; set; }
    }
}
