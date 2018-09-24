using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelApiService.Models
{
    public class RoomBookingData
    {
        public string RoomType { get; set; }
        public int NoOfRoomsBooked { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public int RoomId { get; set; }
    }
}