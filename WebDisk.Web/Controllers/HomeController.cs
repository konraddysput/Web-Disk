using System.Collections.Generic;
using System.Web.Mvc;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.DatabaseModel;
using WebDisk.Web.Attributes;
using WebDisk.Web.Models.Home;

using Identity = WebDisk.Database.IdentityExtensions.IdentityExtensions;

namespace WebDisk.Web.Controllers
{

    [Authorize]
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private readonly DirectoryService _directoryService;

        public HomeController(DirectoryService directoryService)
        {
            _directoryService = directoryService;
        }
        //Return default application view        
        [Route("")]
        public ActionResult Index()
        {
            var userId = Identity.GetUserId(User.Identity);
            ViewBag.DirectoryId = _directoryService.GetRootField(userId)?.FieldId;
            return View();
        }
    }
}