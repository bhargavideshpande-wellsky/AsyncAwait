using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelApiService.Models
{
    public class HotelAllData
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public int HotelRating { get; set; }
        public long HotelContactNo { get; set; }
        public string HotelAmminities { get; set; }
        public string HotelPolicy { get; set; }
        public string HotelImageUrl { get; set; }
    }
}