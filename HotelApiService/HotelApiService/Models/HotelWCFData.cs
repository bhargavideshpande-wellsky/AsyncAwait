using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelApiService.Models
{
    public class HotelWCFData
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public int HotelRating { get; set; }
    }
}