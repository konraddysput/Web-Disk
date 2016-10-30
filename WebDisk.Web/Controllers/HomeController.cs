using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDisk.BusinessLogic.Services;

namespace WebDisk.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private DirectoryService _directoryService;


        // GET: Home
        public ActionResult Index()
        {

            return View();
        }
    }
}