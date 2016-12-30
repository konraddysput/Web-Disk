using System;
using System.Collections.Generic;
using System.Net;
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
            ViewBag.DirectoryId = _directoryService.GetRootField(userId)
                                                    .FieldId;
            return PartialView("_Directory", _directoryService
                                .GetAvailableFields(userId));
        }

        [HttpGet]
        [Route("{directoryId}")]
        [AutoMap(typeof(IEnumerable<Field>), typeof(IEnumerable<FieldViewModel>))]
        public ActionResult IndexDetails(Guid directoryId)
        {
            var userId = Identity.GetUserId(User.Identity);
            ViewBag.DirectoryId = directoryId;
            return PartialView("_Directory", _directoryService
                                .GetAvailableFields(userId, directoryId));
        }


        [HttpPost]
        [Route("Create")]
        [AutoMap(typeof(IEnumerable<Field>), typeof(IEnumerable<FieldViewModel>))]
        public ActionResult Create(Guid rootId, string directoryName)
        {
            try
            {
                var userId = Identity.GetUserId(User.Identity);
                _directoryService.CreateDirectory(userId, rootId, directoryName);
                return IndexDetails(rootId);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

    }
}