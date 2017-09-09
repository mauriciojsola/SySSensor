using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SySSensor.Core.DAL;
using SySSensor.Web.Models;

namespace SySSensor.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

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