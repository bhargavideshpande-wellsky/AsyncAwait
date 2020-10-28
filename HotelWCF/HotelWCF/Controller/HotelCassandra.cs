using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cassandra;
using HotelWCF.Model;

namespace HotelWCF.Controller
{
    public class HotelCassandra : IRepository
    {
        
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
        
        public List<HotelData> GetHotels()
        {
            
            List<HotelData> hotel = new List<HotelData>();
            ISession session = cluster.Connect("hotel");
            string query = "select * from hotel.hotelsdetails";
            var dataReader = session.Execute(query);
            foreach(var row in dataReader)
            {
                HotelData data = new HotelData();
                data.HotelId = row.GetValue<int>("hotelid");
                data.HotelName = row.GetValue<string>("hotelname");
                data.HotelAddress = row.GetValue<string>("hoteladdress");
                data.HotelRatings = row.GetValue<int>("hotelrating");
                hotel.Add(data);
            }

            return hotel;
        }

        public List<RoomData> GetRooms(string HotelId)
        {
            List<RoomData> room = new List<RoomData>();
            ISession session = cluster.Connect("hotel");
            int id = int.Parse(HotelId);
            string query = "select * from hotel.rooms where hotelid =" + id +" allow filtering";
            var dataReader = session.Execute(query);
            foreach (var row in dataReader)
            {
                RoomData data = new RoomData();
                data.RoomId = row.GetValue<int>("roomid");
                data.HotelId = row.GetValue<int>("hotelid");
                data.RoomType = row.GetValue<string>("roomtype");
                data.RoomsAvailable = row.GetValue<int>("roomsavialable");
                room.Add(data);
            }
            return room;
        }
        public void UpdateNoOfRooms(NoOfRoomsBookedData noOfRoomsBook)
        {
            ISession session = cluster.Connect("hotel");
            int availableRooms = 0;
            int noOfRoomsBooked = noOfRoomsBook.NoOfRoomBooked;
            int roomId = noOfRoomsBook.RoomId;

            string query = "select * from hotel.rooms where roomid =" + roomId + " allow filtering";
            var dataReader = session.Execute(query);
            foreach (var row in dataReader)
            {
                availableRooms= row.GetValue<int>("roomsavialable");
            }
            availableRooms = availableRooms - noOfRoomsBooked;
            query = "update hotel.rooms set roomsavialable =" + availableRooms + " where roomid=" + roomId;
            session.Execute(query);
        }
    }
}