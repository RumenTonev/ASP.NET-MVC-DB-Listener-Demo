using RealTimeChart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealTimeChart.Controllers
{
    public class MachinesController : Controller
    {
        // GET: Machies
        public ActionResult Index()
        {
            List<MachineShortModel> data = new List<MachineShortModel>();
             using (var context = new GraphContext())
                {
                  data = context.StationMonitoringInfos.AsQueryable().Select(MachineShortModel.FromMachine).ToList();
                }
            return this.View(data);
        }
        public ActionResult Chart(string id)
        {
            ViewBag.CustomValue = id;
            return PartialView("Chart");
        }
    }
}