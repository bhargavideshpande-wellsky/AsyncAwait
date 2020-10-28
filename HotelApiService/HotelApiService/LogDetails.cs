using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cassandra;
using RoomBookingDataAccess;

namespace HotelApiService
{
    public sealed class LogDetails
    {
        private static LogDetails instance = null;
        private LogDetails()
        {

        }
        public static LogDetails Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogDetails();
                }
                return instance;
            }
        }

        public void AddLoggingDetails(string request,string response, string discribtion)
        {
            var cluster = Cluster.Builder().AddContactPoints("127.0.0.1").Build();
            var session = cluster.Connect("hotel");
            string query = "Insert into  logger  (id, request, response, describtion, date) values (uuid(),'" + request + "', '" + response + "', '" + discribtion + "', dateof(now()))";
            var res = session.Execute(query);
        }


    }

    
}