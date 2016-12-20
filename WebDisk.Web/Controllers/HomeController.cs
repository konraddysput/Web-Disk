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
    public class HomeController : Controller
    {
        private DirectoryService _directoryService;

        public HomeController(DirectoryService directoryService)
        {
            _directoryService = directoryService;
        }
        // GET: Home
        [AutoMap(typeof(IEnumerable<Field>), typeof(IEnumerable<FieldViewModel>))]
        public ActionResult Index()
        {
            var userId = Identity.GetUserId(User.Identity);

            return View(_directoryService
                            .GetAvailableFields(userId));
        }
    }
}