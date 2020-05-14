using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class TimeFunc
    {
        public static DateTime CreateDateTimeFromString(string t)
        {
            DateTime clientTime;
            try
            {
                //split the time from client
                string[] dataTime = t.Split("T");
                string[] data = dataTime[0].Split("-");
                string[] time = dataTime[1].Split(":");
                clientTime = new DateTime(Convert.ToInt32(data[0]),
                    Convert.ToInt32(data[1]), Convert.ToInt32(data[2]),
                    Convert.ToInt32(time[0]), Convert.ToInt32(time[1]),
                    Convert.ToInt32(time[2].Split("Z")[0]));
            }
            catch
            {
                throw new Exception();
            }

            return clientTime;
        }

        public static bool ValidStringDate(string date)
        {
            try
            {
                CreateDateTimeFromString(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ConvertDate(DateTime dateTime)
        {
           
            return "" + dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day
                + "T" + dateTime.Hour + ":" + dateTime.Minute + ":" + dateTime.Minute
                + ":" + dateTime.Second + "Z";
        }
    }
}
