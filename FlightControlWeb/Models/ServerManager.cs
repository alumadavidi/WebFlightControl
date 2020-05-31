using System;
using System.Collections.Generic;

namespace FlightControlWeb.Models
{
    public class ServerManager : IServerManager
    {
        //private SqliteDB db = SqliteDB.Instance;
        private readonly IDataManager db;
        private readonly IExternalFlight externalFlight;
        public ServerManager(IDataManager db, IExternalFlight external)
        {
            this.db = db;
            externalFlight = external;
        }
        public void AddServer(ServerFlight s)
        {
            //add new server to DB
            List<ServerFlight> list = db.GetServers();
            foreach(ServerFlight server in list)
            {
                //same url - throw exception
                if (server.ServerUrl == s.ServerUrl)
                {
                    throw new Exception();
                }
            }

            if (s.ServerId == null || s.ServerUrl == null)
            {
                throw new Exception();
            }
            db.AddServer(s);
        }

        public void deleteServer(string id)
        {
            //delete server from DB and dic
            db.RemoveServer(id);
            externalFlight.RemoveServerFromDic(id);

        }

        public List<ServerFlight> GetServerFlights()
        {
            return db.GetServers();
        }
    }
}
