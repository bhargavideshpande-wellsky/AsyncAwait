using HotelWCF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HotelWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHotelService" in both code and config file together.
    [ServiceContract]
    public interface IHotelService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/Hotels", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<HotelData> GetAllHotels();

        [OperationContract]
        [WebGet(UriTemplate = "/Rooms/{hotelId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<RoomData> GetAllRooms(string hotelId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/Hotels/RoomBooking", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void RoomBooking(NoOfRoomsBookedData noOfRoomsBook);
    }
}
