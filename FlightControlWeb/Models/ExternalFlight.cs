using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class ExternalFlight
    {
        private SqliteDB db = SqliteDB.Instance;

        private string url;
        private List<Flight> flight;



        public ExternalFlight(string time)
        {
           
            url = "api/Flights?relative_to="+time;
            flight = new List<Flight>();
           
        }

        public async Task<List<Flight>> GetRequestAsync()
        {
            List<ServerFlight> servers = db.GetServers();
            foreach (ServerFlight s in servers)
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(s.ServerUrl);
                //add header fields for jyson
                httpClient.DefaultRequestHeaders.Add("User-Agent", "C# console program");
                httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                try
                { 
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var resp = await response.Content.ReadAsStringAsync();

                    List<Flight> newFlight = JsonConvert.DeserializeObject<List<Flight>>(resp);
                    flight.AddRange(newFlight);
                } catch(Exception) {
                    Console.WriteLine("failed in external filght get response");
                }
            }
            return flight;
            
        }
          
    }
}
