using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SySSensor.Core.DAL;
using SySSensor.Web.Models.Logs;

namespace SySSensor.Web.Controllers
{
    [Authorize]
    [Route("{action=Index}")]
    public class LogFilesController : Controller
    {
        [Route("")]
        [Route("Index")]
        public ActionResult Index()
        {
            var db = new SySDB();
            var model = new List<LogFileViewModel>();

            foreach (var f in db.LogFiles.OrderByDescending(x => x.ProcessDate).ThenByDescending(x => x.DateCreated).ToList())
            {
                model.Add(new LogFileViewModel { Filename = f.FileName, DateCreated = f.DateCreated });
            }

            return View(model);
        }
    }
}

//https://github.com/esp8266/Arduino/issues/1390   HTTP POST, GET, etc
//http://playground.arduino.cc/Code/WebClient
