using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightManager
    {
        private SqliteDB db = SqliteDB.Instance;

        public List<Flight> GetFlightsFromServer(string relative_to)
        {
            DateTime serverTime = DateTime.UtcNow;
            List<Flight> currentFlight = new List<Flight>();
            List<FlightPlanId> flightPlan = db.GetFlightPlans();

            foreach(FlightPlanId fid in flightPlan){
                FlightPlan f = fid.FlightP;
                DateTime requestTime = 
                    createDateTimeToClient(relative_to);
                List<Segment> segments = f.Segments;
                List<DateTime> dateTimes = createAllDataTimeClient(segments,
                    createDateTimeToClient(f.InitialLocation.DateTime));
                //DateTime request, DateTime start, DateTime end
                if (flightIsNow(requestTime, dateTimes[0], dateTimes[dateTimes.Count -1]))
                {
                    int numSegment = findSpecificSegment(dateTimes, requestTime);

                    double longStart, longEnd, latStart, latEnd;
                    DateTime startSegment = dateTimes[numSegment];
                    DateTime endSegment = dateTimes[numSegment + 1];
                    //num seconds passed from the beginning of the segment 
                    double secondsPast = (requestTime - startSegment).TotalSeconds;
                    //total seconds that left in segment
                    double secondLeft = (endSegment - requestTime).TotalSeconds;
                    if (numSegment == 0)
                    {
                        
                        longStart = f.InitialLocation.Longitude;
                        longEnd = segments[numSegment].Longitude;

                        latStart = f.InitialLocation.Latitude;
                        latEnd = segments[numSegment].Latitude;
                    }
                    else 
                    {
                        longStart = segments[numSegment-1].Longitude;
                        longEnd = segments[numSegment].Longitude;

                        latStart = segments[numSegment - 1].Latitude;
                        latEnd = segments[numSegment].Latitude;

                    }
                    double newX = (longStart * secondLeft + longEnd * secondsPast) /
                      (secondsPast + secondLeft)  ;
                    double newY = (latStart * secondLeft + latEnd * secondsPast) /
                      (secondsPast + secondLeft);
                  
                    //add the new Flight to list
                    currentFlight.Add(new Flight(fid.ID, newX, newY,
                        f.Passengers, f.CompanyName, convertString(serverTime), false));
                }
            }
           
            return currentFlight;
        }

        private string convertString(DateTime serverTime)
        {
            return "" + serverTime.Year + "-" + serverTime.Month + "-" + serverTime.Day
                + "T" + serverTime.Hour + ":" + serverTime.Minute + ":" + serverTime.Minute
                + ":" + serverTime.Second + "Z";
        }

        private List<DateTime> createAllDataTimeClient(List<Segment> segments, DateTime start)
        {
            List<DateTime> dateTime = new List<DateTime>();
            dateTime.Add(start);
            DateTime t = start;
            for (int i = 0; i < segments.Count; i++)
            {
               
                t = t.AddSeconds(segments[i].TimespanSeconds);
                dateTime.Add(t);
            }
           
            return dateTime;
        }
        private int findSpecificSegment(List<DateTime> dateTimes, DateTime client)
        {
            int i;
            for(i = 1; i < dateTimes.Count; i++)
            {
                if(dateTimes[i-1] <= client && client <= dateTimes[i])
                {
                    break;
                }
            }
            return i-1;
        }
        private bool flightIsNow(DateTime request, DateTime start, DateTime end)
        {
            bool now = false;
            //doenst start or finish
            if((start <= request) && (request <= end))
            {
                now = true;
            }
            return now;
        }
      
        private DateTime createDateTimeToClient(string t)
        {
            //split the time from client
            string[] dataTime = t.Split("T");
            string[] data = dataTime[0].Split("-");
            string[] time = dataTime[1].Split(":");
            DateTime clientTime = new DateTime(Convert.ToInt32(data[0]),
                Convert.ToInt32(data[1]), Convert.ToInt32(data[2]),
                Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), 
                Convert.ToInt32(time[2].Split("Z")[0]));
            return clientTime;

        }
        private List<Flight> GetExternalFlights(string relative_to)
        {
            //TODO: implement
            return null;
        }

        public List<Flight> GetAllFlights(string relative_to)
        {
            List<Flight> localServer = GetFlightsFromServer(relative_to);
            List<Flight> externalServer = GetExternalFlights(relative_to);
            
            localServer.AddRange(externalServer);
            return localServer;
        }

        public void DeleteFlight(string id)
        {
            db.RemoveFlightPlan(id);
        }
    }
}
