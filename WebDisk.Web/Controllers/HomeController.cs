using System.Collections.Generic;
using System.Web.Mvc;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.DatabaseModel;
using WebDisk.Web.Attributes;
using WebDisk.Web.Models.Home;



namespace WebDisk.Web.Controllers
{

    [Authorize]
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        //Return default application view        
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}