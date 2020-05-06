using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class Flight
    {
        private string flight_id;
        private double longitude;
        private double latitude;
        private int passengers;
        private string company_name;
        private string date_time;
        private bool is_external;
        [JsonConstructor]
        public Flight(string flight_id, double longitude, double latitude, int passengers,
            string company_name, string date_time, bool is_external)
        {
            this.flight_id = flight_id;
            this.longitude = longitude;
            this.latitude = latitude;
            this.passengers = passengers;
            this.company_name = company_name;
            this.date_time = date_time;
            this.is_external = is_external;
        }

        public string Flight_Id
        {
            get
            {
                return flight_id;
            }
            set
            {
                flight_id = value;
            }
        }
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
        public string Company_name
        {
            get
            {
                return company_name;
            }
            set
            {
                company_name = value;
            }
        }
        public string Date_time
        {
            get
            {
                return date_time;
            }
            set
            {
                date_time = value;
            }
        }
        public bool Is_external
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
