using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealtimeUsage.Application;
using RealtimeUsage.Repository;
using RealtimeUsage.Domain;
using System.Threading;

namespace WebAsp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            RepositoryRealtimeUsage repositoryRealtimeUsage = new RepositoryRealtimeUsage();
            ClassRealtimeUsage classRealtimeUsage = new ClassRealtimeUsage();
            ApplicationRealtimeUsage _applicationRealtimeUsage = new ApplicationRealtimeUsage(repositoryRealtimeUsage);

     

            while(true)
            {
                _applicationRealtimeUsage.getNameMachine(classRealtimeUsage);
                _applicationRealtimeUsage.getUsageCPU(classRealtimeUsage);
                _applicationRealtimeUsage.getUsageCPU(classRealtimeUsage);
                _applicationRealtimeUsage.getUsageRAM(classRealtimeUsage);
                Thread.Sleep(5000);
                return View(classRealtimeUsage);
            }
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
    }
}