using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SySSensor.Core.DAL;
using SySSensor.Web.Models;

namespace SySSensor.Web.Controllers
{
    public class LogsController : ApiController
    {
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
    }
}