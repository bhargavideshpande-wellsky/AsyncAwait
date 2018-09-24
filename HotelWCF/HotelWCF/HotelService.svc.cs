using Cassandra;
using HotelWCF.Controller;
using HotelWCF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HotelWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HotelService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HotelService.svc or HotelService.svc.cs at the Solution Explorer and start debugging.
    public class HotelService : IHotelService
    {
        IRepository obj = new HotelCassandra();
        public List<HotelData> GetAllHotels()
        {
            return obj.GetHotels();
        }

        public List<RoomData> GetAllRooms(string hotelId)
        {
            return obj.GetRooms(hotelId);
        }
        
        public void RoomBooking(NoOfRoomsBookedData noOfRoomsBook)
        {
             obj.UpdateNoOfRooms(noOfRoomsBook);
        }
    }
}
