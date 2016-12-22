using System;
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
    [RoutePrefix("Directory")]
    public class DirectoryController : Controller
    {
        private DirectoryService _directoryService;

        public DirectoryController(DirectoryService directoryService)
        {
            _directoryService = directoryService;
        }

        [HttpGet]
        [Route("")]
        [AutoMap(typeof(IEnumerable<Field>), typeof(IEnumerable<FieldViewModel>))]
        public ActionResult Index()
        {

            var userId = Identity.GetUserId(User.Identity);
            return PartialView("_Directory",_directoryService
                                .GetAvailableFields(userId));
        }

        [HttpGet]
        [Route("{directoryId}")]
        [AutoMap(typeof(IEnumerable<Field>), typeof(IEnumerable<FieldViewModel>))]
        public ActionResult Index(Guid directoryId)
        {
            var userId = Identity.GetUserId(User.Identity);
            return PartialView("_Directory",_directoryService
                                .GetAvailableFields(userId, directoryId));
        }

    }
}