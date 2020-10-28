using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelApiService.Models
{
    public class RoomWCFData
    {
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public string RoomType { get; set; }
        public int RoomsAvailable { get; set; }
    }
}