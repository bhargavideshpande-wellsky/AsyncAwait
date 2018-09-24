using HotelWCF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelWCF.Controller
{
    interface IRepository
    {
        List<HotelData> GetHotels();
        List<RoomData> GetRooms(string HotelId);
        void UpdateNoOfRooms(NoOfRoomsBookedData noOfRoomsBook);
    }
}
