using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HotelWCF.Model
{
    [DataContract]
    public class RoomData
    {
        [DataMember]
        public int RoomId { get; set; }

        [DataMember]
        public int HotelId { get; set; }

        [DataMember]
        public string RoomType { get; set; }

        [DataMember]
        public int RoomsAvailable { get; set; }
    }
}