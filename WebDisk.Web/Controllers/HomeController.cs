using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDisk.BusinessLogic.Services;

namespace WebDisk.Web.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private DirectoryService _directoryService;

        public HomeController(DirectoryService directoryService)
        {
            _directoryService = directoryService;
        }
        // GET: Home
        public ActionResult Index()
        {
            //var userId = IdentityExtensions.GetUserId(User.Identity);
            var userId = Guid.NewGuid();

            _directoryService.GetAvailableFields(userId,Guid.NewGuid());
            return View();
        }
    }
}