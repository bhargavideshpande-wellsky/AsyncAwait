using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HotelWCF.Model
{
    [DataContract]
    public class NoOfRoomsBookedData
    {
        [DataMember]
        public int RoomId { get; set; }

        [DataMember]
        public int NoOfRoomBooked { get; set; }
    }
}