using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LocationSvc.Controllers
{
    [Authorize]
    public class LocationController : ApiController
    {
        //https://localhost:44387/api/Location?cityName=dc
        public Models.Location GetLocation(string cityName)
        {
            return new Models.Location() {  Latitute=10, Longitude = 20};
        }
    }
}
