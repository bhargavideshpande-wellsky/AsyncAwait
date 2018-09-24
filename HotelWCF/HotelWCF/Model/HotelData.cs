using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HotelWCF.Model
{
    [DataContract]
    public class HotelData
    {
        [DataMember]
        public int HotelId { get; set; }

        [DataMember]
        public string HotelName { get; set; }

        [DataMember]
        public string HotelAddress { get; set; }

        [DataMember]
        public int HotelRatings { get; set; }


    }
}