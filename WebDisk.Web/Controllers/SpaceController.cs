using System.Web.Mvc;
using WebDisk.BusinessLogic.Interfaces;
using WebDisk.Database.IdentityExtensions;

namespace WebDisk.Web.Controllers
{
    [Authorize]
    [RoutePrefix("Space")]
    public class SpaceController : Controller
    {
        private ISpaceService _spaceService;

        public SpaceController(ISpaceService spaceService)
        {
            _spaceService = spaceService;
        }

        [Route("")]
        //[AutoMap(typeof(Space), typeof(SpaceOverviewViewModel))]
        public ActionResult Index()
        {
            
            return View(_spaceService.GetSpace(User.Identity.GetUserId()));
        }
    }
}