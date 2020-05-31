using FlightControlWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightManager : IFlightManager
    {
        private readonly IDataManager db;
        private readonly IExternalFlight externalFlight;
        public FlightManager(IDataManager db, IExternalFlight externalFlight)
        {
            this.db = db;
            this.externalFlight = externalFlight;
        }

        public List<Flight> GetFlightsFromServer(string relative_to)
        {
            //interuplation of flight place
            List<Flight> currentFlight = new List<Flight>();
            List<FlightPlanId> flightPlan = db.GetFlightPlans();

            foreach (FlightPlanId fid in flightPlan)
            {
                FlightPlan f = fid.FlightP;
                DateTime requestTime =
                    TimeFunc.CreateDateTimeFromString(relative_to);
                List<Segment> segments = f.Segments;
                List<DateTime> dateTimes = CreateAllDataTimeClient(segments,
                    TimeFunc.CreateDateTimeFromString(f.InitialLocation.DateTime));
               //if flight is now
                if (FlightIsNow(requestTime, dateTimes[0], dateTimes[dateTimes.Count - 1]))
                {
                    //get the new place of flight
                    UpdateFlight(dateTimes, requestTime, fid, segments, currentFlight);
                }
            }
            return currentFlight;
        }

        private void UpdateFlight(List<DateTime> dateTimes, DateTime requestTime, 
            FlightPlanId fid, List<Segment> segments, List<Flight> currentFlight)
        {
            FlightPlan f = fid.FlightP;
            //find current segment of flight
            int numSegment = FindSpecificSegment(dateTimes, requestTime);

            double longStart, longEnd, latStart, latEnd;
            DateTime startSegment = dateTimes[numSegment];
            DateTime endSegment = dateTimes[numSegment + 1];
            //num seconds passed from the beginning of the segment 
            double secondsPast = (requestTime - startSegment).TotalSeconds;
            //total seconds that left in segment
            double secondLeft = (endSegment - requestTime).TotalSeconds;
            //calculate current place
            if (numSegment == 0)
            {

                longStart = f.InitialLocation.Longitude;
                longEnd = segments[numSegment].Longitude;

                latStart = f.InitialLocation.Latitude;
                latEnd = segments[numSegment].Latitude;
            }
            else
            {
                longStart = segments[numSegment - 1].Longitude;
                longEnd = segments[numSegment].Longitude;

                latStart = segments[numSegment - 1].Latitude;
                latEnd = segments[numSegment].Latitude;

            }
            double newX = (longStart * secondLeft + longEnd * secondsPast) /
              (secondsPast + secondLeft);
            double newY = (latStart * secondLeft + latEnd * secondsPast) /
              (secondsPast + secondLeft);

            //add the new Flight to list
            currentFlight.Add(new Flight(fid.ID, newX, newY,
                f.Passengers, f.CompanyName, TimeFunc.ConvertDate(requestTime), false));
        }

        private List<DateTime> CreateAllDataTimeClient(List<Segment> segments, DateTime start)
        {
            //create time of segments 
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
        private int FindSpecificSegment(List<DateTime> dateTimes, DateTime client)
        {
            //find the current segment of flight
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
        private bool FlightIsNow(DateTime request, DateTime start, DateTime end)
        {
            bool now = false;
            //doenst start or finish
            if((start <= request) && (request <= end))
            {
                now = true;
            }
            return now;
        }


        private async Task<List<Flight>> GetExternalFlights(string relative_to)
        {
            //get flight from external server
            List<Flight> flight = await externalFlight
                .GetExternalFlightAsync(relative_to);
            return flight;
        }

        public async Task<List<Flight>> GetAllFlights(string relative_to)
        {
            //get all flight - both external and inner server
            List<Flight> localServer = GetFlightsFromServer(relative_to);
            List<Flight> externalServer = await GetExternalFlights(relative_to);

            localServer.AddRange(externalServer);
            return localServer;
        }

        public void DeleteFlight(string id)
        {
            //delete flight from DB
            db.RemoveFlightPlan(id);
        }
    }
}
