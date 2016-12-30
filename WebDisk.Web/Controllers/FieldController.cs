using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebDisk.BusinessLogic.Services;
using WebDisk.BusinessLogic.ViewModels;
using WebDisk.Database.DatabaseModel;
using WebDisk.Web.Attributes;
using WebDisk.Web.Models.Field;
using Identity = WebDisk.Database.IdentityExtensions.IdentityExtensions;

namespace WebDisk.Web.Controllers
{

    /// <summary>
    /// Field Controller allow user to make basic async operations with fields - files and directories
    /// </summary>
    [Authorize]
    [RoutePrefix("Field")]
    public class FieldController : Controller
    {
        /// <summary>
        /// service that allow us to make a operations on fields
        /// </summary>
        private DirectoryService _directoryService;

        private FieldService _fieldService;
        public FieldController(DirectoryService directoryService, FieldService fieldService)
        {
            _directoryService = directoryService;
            _fieldService = fieldService;

        }

        /// <summary>
        /// Get file details like creation date, access mode, owner informations
        /// </summary>
        /// <param name="fieldId">id of field that we're looking for</param>
        /// <returns>Json with field informations</returns>
        [Route("Details/{fieldId}")]
        [AjaxAction]
        [AutoMap(typeof(Field),typeof(FieldDescriptionViewModel))]
        public ActionResult Details(Guid fieldId)
        {
            Guid userId = Identity.GetUserId(User.Identity);
            return PartialView("_FieldPropertyDescription",_directoryService
                                                            .GetFieldDetails(userId, fieldId));
        }

        /// <summary>
        /// Allow user to rename field
        /// </summary>
        /// <param name="fieldId">id of fie.d</param>
        /// <param name="fieldName">new field name</param>
        /// <returns>Status code - 200 OK, for a good result, 401 - BadRequest, when receive an exception</returns>
        [Route("Update/{fieldId}/{fieldName}")]
        public ActionResult Update(Guid fieldId, string fieldName)
        {
            throw new NotImplementedException();
        }


        [Route("Delete/{fieldId}")]
        public ActionResult Delete(Guid fieldId)
        {
            throw new NotImplementedException();
        }


        [HttpPost]
        [Route("Update/{directoryId}")]
        public ActionResult Update(IEnumerable<HttpPostedFileBase> files, Guid directoryId)
        {
            try
            {
                var fileViewModel = AutoMapper.Mapper.Map<IEnumerable<FileViewModel>>(files);
                Guid userId = Identity.GetUserId(User.Identity);
                _fieldService.CreateField(userId, directoryId, fileViewModel);
                return RedirectToAction("IndexDetails", "Directory", new { directoryId = directoryId });
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}