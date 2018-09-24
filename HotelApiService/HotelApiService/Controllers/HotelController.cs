
using HotelApiService.Models;
using Newtonsoft.Json;
using RoomBookingDataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HotelApiService.Controllers
{
    
    public class HotelController : ApiController
    {
        
        [Route("api/Hotel/AllHotels")]
        public async Task<List<HotelAllData>> GetAllDetailsOfHotels()
        {
            List<HotelAllData> hotelslist = new List<HotelAllData>();

            List<HotelStaticData> HotelListOfJson = new List<HotelStaticData>();
            HotelListOfJson = GetHotelsByJson();

            List<HotelWCFData> HotelsWcf = new List<HotelWCFData>();

            Task<List<HotelWCFData>> HotelWcfTask = GetHotelsByWCF();
            HotelsWcf = await HotelWcfTask;
            
            foreach(var hotels in HotelsWcf)
            {
                HotelAllData hotel = new HotelAllData();

                hotel.HotelId = hotels.HotelId;
                hotel.HotelName = hotels.HotelName;
                hotel.HotelAddress = hotels.HotelAddress;
                hotel.HotelRating = hotels.HotelRating;

                HotelStaticData detailsOfHotel = HotelListOfJson.Find(x => x.HotelId == hotels.HotelId);

                hotel.HotelContactNo = detailsOfHotel.HotelContactNo;
                hotel.HotelAmminities = detailsOfHotel.HotelAmminities;
                hotel.HotelPolicy = detailsOfHotel.HotelPolicy;
                hotel.HotelImageUrl = detailsOfHotel.HotelImageUrl;
                hotelslist.Add(hotel);
            }

            if (hotelslist == null)
            {
                LogDetails.Instance.AddLoggingDetails("GetAllHotels","Failure", "Exception Occured:No Data Found");
                throw new Exception("No Data Found");
            }
            else
            {
                LogDetails.Instance.AddLoggingDetails("GetAllHotels","Success","Get all details of hotels from WCF and Json File");
                return hotelslist;
            }
  
        }

        [Route("api/Hotel/hotelWcf")]
        public async Task<List<HotelWCFData>> GetHotelsByWCF()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:57246/HotelService.svc/Hotels");
            List<HotelWCFData> content = new List<HotelWCFData>();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                content = await response.Content.ReadAsAsync<List<HotelWCFData>>();
            }

            if (content == null)
            {
                LogDetails.Instance.AddLoggingDetails("GetHotelsByWCF","Failure", "Exception Occured:Please Check WCF Connection");
                throw new Exception("Please Check WCF Connection!");
            }
            else
            {
                LogDetails.Instance.AddLoggingDetails("GetHotelsByWCF", "Success","Get all details of hotels from WCF");
                return content;
            }
            
        }

        [Route("api/Hotel/hotelJson")]
        public List<HotelStaticData> GetHotelsByJson()
        {
            string path = @"C:\Users\bdeshpande\source\repos\HotelApiService\HotelApiService\Controllers\HotelAdditionalInfo.Json";
            using (StreamReader r = new StreamReader(path))
            {
                var json = r.ReadToEnd();
                var hotels = JsonConvert.DeserializeObject<List<HotelStaticData>>(json);

                if (hotels == null)
                {
                    LogDetails.Instance.AddLoggingDetails("GetHotelsByJson", "Failure", "Exception Occured:No Data Found");
                    throw new Exception("No Data Found!");
                }
                else
                {
                    LogDetails.Instance.AddLoggingDetails("GetHotelsByJson", "Success", "Get all details of hotels from Json");
                    return hotels;
                }
                
            }
        }

        [Route("api/Hotel/Room/{hotelId}")]
        public async Task<List<RoomWCFData>> GetRoomsByHotelId(string hotelId)
        {
            
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:57246/HotelService.svc/Rooms/"+ hotelId);
            List<RoomWCFData> content = new List<RoomWCFData>();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                content = await response.Content.ReadAsAsync<List<RoomWCFData>>();
            }
            if (content == null)
            {
                LogDetails.Instance.AddLoggingDetails("GetRoomsByHotelId","Failure", "Exception Occured:Please Check WCF Connection");
                throw new Exception("Please Check WCF Connection!");
            }
            else
            {
                LogDetails.Instance.AddLoggingDetails("GetRoomsByHotelId", "Success","Get all rooms available in specific hotel");
                return content;
            }
        }

        [Route("api/Hotel/RoomBooking")]
        public void PostRoomBooking([FromBody]RoomBooking bookingDetails)
        {
            using (HotelsRoomEntities entities = new HotelsRoomEntities())
            {
                entities.RoomBookings.Add(bookingDetails);
                entities.SaveChanges();
            }
            LogDetails.Instance.AddLoggingDetails("PostRoomBooking", "Success","Rooms Booked successfully");
        }

        [Route("api/Hotel/NoOfRoomBook")]
        public async Task PutNoOfRoomBookAsync([FromBody]NoOfRoomBook bookedRoomDetails)
        {
            HttpResponseMessage response = null;
            using(var client = new HttpClient())
            {
                string url = "http://localhost:57246/HotelService.svc/Hotels/RoomBooking";
                response = await client.PutAsJsonAsync(url, bookedRoomDetails);
            }
            LogDetails.Instance.AddLoggingDetails("PutNoOfRoomBookAsync", "Success", "Updation is done at cassandra");
        }
    }
}
