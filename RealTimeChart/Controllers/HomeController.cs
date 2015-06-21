using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
//using DatabaseChange.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RealTimeChart.Controllers
{
    public class HomeController : Controller
    {
        private CloudQueue thumbnailRequestQueue;
        public HomeController()
        {
            InitializeStorage();
        }
        private void InitializeStorage()
        {
            // Open storage account using credentials from .cscfg file.
            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ToString());
            // Get context object for working with queues, and 
            // set a default retry policy appropriate for a web user interface.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            //queueClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);
            // Get a reference to the queue.
            thumbnailRequestQueue = queueClient.GetQueueReference("machinerequest");
        }
       //for Azure deployment purposes
        public ActionResult Demo()
        {
            var queueMessage = new CloudQueueMessage("demo rumen");
            thumbnailRequestQueue.AddMessageAsync(queueMessage);
            Trace.TraceInformation("Created queue message for AdId {0}", "demo rumen");
            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }    
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Chart(string id)
        {
            ViewBag.CustomValue = id;
           
            return View();
        }
        public ActionResult Status()
        {
            return View();
        }

    }
}