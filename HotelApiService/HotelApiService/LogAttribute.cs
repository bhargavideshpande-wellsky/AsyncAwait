using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc.Filters;
using RoomBookingDataAccess;

namespace HotelApiService
{
    public class LogAttribute : ResultFilterAttribute, IActionFilter
    {
        LogDetail log = new LogDetail();
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            log.RequestType = context.RouteData.Values["action"].ToString() + " " + context.RouteData.Values["controller"].ToString();
            log.RequestStatus = "sending request";
            log.ExceptionDetails = "Null";
            using (HotelsRoomEntities entities = new HotelsRoomEntities())
            {
                entities.LogDetails.Add(log);
                entities.SaveChanges();
            }
        }
    }
}