using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SySSensor.Core.DAL;
using SySSensor.Web.Models;
using SySSensor.Web.Models.Logs;

namespace SySSensor.Web.Controllers
{
    [RoutePrefix("api/logs")]
    public class LogsController : ApiController
    {
        [HttpGet]
        [Route("get-logs")]
        public IList<SensorLogDataViewModel> GetLogs()
        {
            var db = new SySDB();
            var dateFrom = new DateTime(2017, 08, 31, 9, 0, 0);
            var dateTo = new DateTime(2017, 08, 31, 12, 0, 0);

            return db.SensorLogs.Where(x => x.ReadDate >= dateFrom && x.ReadDate <= dateTo).Select(x => new SensorLogDataViewModel
            {
                Date = x.ReadDate,
                Temperature = x.Temperature,
                Humidity = x.Humidity
            }).ToList();
        }

        [HttpGet]
        [Route("ready-get")]
        [AllowAnonymous]
        public bool ReadyGet()
        {
            return true;
        }

        [HttpPost]
        [Route("ready-post")]
        [AllowAnonymous]
        public bool ReadyPost()
        {
            return true;
        }

        [HttpPost]
        [Route("save")]
        [AllowAnonymous]
        public bool Save(LogFileViewModel model)
        {
            return true;
        }
    }
}

//https://github.com/esp8266/Arduino/issues/1390   HTTP POST, GET, etc
//http://playground.arduino.cc/Code/WebClient
