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
        private bool isExternal;

        public string FlightId
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
        public bool IsExternal
        {
            get
            {
                return isExternal;
            }
            set
            {
                isExternal = value;
            }
        }
    }
}
