using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SySSensor.Core.DAL;
using SySSensor.Core.Services;
using SySSensor.Web.Models;
using SySSensor.Web.Models.Logs;

namespace SySSensor.Web.Controllers
{
    [RoutePrefix("api")]
    public class SySApiController : ApiController
    {
        [HttpGet]
        [Route("logs/get-logs")]
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
        [Route("logs/ready")]
        [AllowAnonymous]
        public bool ServerReady()
        {
            return true;
        }

        //[HttpPost]
        //[Route("logs/ready-post")]
        //[AllowAnonymous]
        //public bool ReadyPost()
        //{
        //    return true;
        //}

        [HttpPost]
        [Route("logs/save")]
        [AllowAnonymous]
        public int SaveLogFile(LogFileViewModel model)
        {
            var service = new LogsService();
            return service.SaveLogFile(model.Filename, model.FileContent);
        }

        [HttpGet]
        [Route("logs/check-file/{filename}")]
        [AllowAnonymous]
        public bool CheckLogFile(string filename)
        {
            var db = new SySDB();
            return db.LogFiles.Any(x => x.FileName.Equals(filename, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}

//https://github.com/esp8266/Arduino/issues/1390   HTTP POST, GET, etc
//http://playground.arduino.cc/Code/WebClient
