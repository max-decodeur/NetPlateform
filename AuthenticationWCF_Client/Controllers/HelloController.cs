using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthenticationWCF_Client.Models;

namespace AuthenticationWCF_Client.Controllers
{
    public class HelloController : Controller
    {
        // GET: Hello
        public ActionResult Index()
        {
            HelloClient hc = new HelloClient();
            ViewBag.Result = hc.HelloWorld() == null ? "Access Denied" : hc.HelloWorld();
            return View();
        }
    }
}