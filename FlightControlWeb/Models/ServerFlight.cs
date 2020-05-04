using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class ServerFlight
    {
        private string serverId;
        private string serverUrl;
        public ServerFlight(string id, string url)
        {
            serverId = id;
            serverUrl = url;
        }
        public string ServerId
        {
            set
            {
                serverId = value;
            }
            get
            {
                return serverId;
            }
        }
        public string ServerUrl
        {
            set
            {
                serverUrl = value;
            }
            get
            {
                return serverUrl;
            }
        }

    }
}
