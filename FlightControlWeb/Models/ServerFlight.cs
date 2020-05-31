using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class ServerFlight
    {
        public ServerFlight(string id, string url)
        {
            ServerId = id;
            ServerUrl = url;
        }
        [JsonProperty("ServerId")]
        public string ServerId { set; get; }
        [JsonProperty("ServerUrl")]
        public string ServerUrl { set; get; }

    }
}
