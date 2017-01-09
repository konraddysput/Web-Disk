using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebDisk.BusinessLogic.Common;
using WebDisk.BusinessLogic.Services;
using WebDisk.BusinessLogic.ViewModels;
using WebDisk.Database.DatabaseModel;
using WebDisk.Web.Attributes;
using WebDisk.Web.Models.Field;
using PdfConverter = Microsoft.Office.Interop.Word;
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
        private readonly DirectoryService _directoryService;
        private readonly FieldService _fieldService;
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
        [AutoMap(typeof(Field), typeof(FieldDescriptionViewModel))]
        public ActionResult Details(Guid fieldId)
        {
            Guid userId = Identity.GetUserId(User.Identity);
            return PartialView("_FieldPropertyDescription", _directoryService
                                                            .GetFieldDetails(userId, fieldId));
        }

        /// <summary>
        /// Allow user to rename field
        /// </summary>
        /// <param name="fieldId">id of fie.d</param>
        /// <param name="fieldName">new field name</param>
        /// <returns>Status code - 200 OK, for a good result, 401 - BadRequest, when receive an exception</returns>
        [HttpPost]
        [AjaxAction]
        [Route("Copy/{destinationId}/{fieldId}")]
        public ActionResult Copy(Guid fieldId, Guid destinationId)
        {
            Guid userId = Identity.GetUserId(User.Identity);
            _fieldService.Copy(userId, destinationId, fieldId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [AjaxAction]
        [Route("Cut/{destinationId}/{fieldId}")]
        public ActionResult Cut(Guid fieldId, Guid destinationId)
        {
            Guid userId = Identity.GetUserId(User.Identity);
            _fieldService.Cut(userId, destinationId, fieldId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }



        [Route("Delete/{fieldId}")]
        public ActionResult Delete(Guid fieldId)
        {
            Guid userId = Identity.GetUserId(User.Identity);
            _fieldService.Delete(userId, fieldId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        [HttpPost]
        [Route("Update/{directoryId}")]
        public ActionResult Create(IEnumerable<HttpPostedFileBase> files, Guid directoryId)
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
        [HttpGet]
        [Route("Download/{fieldId}")]
        public ActionResult Download(Guid fieldId)
        {
            Guid userId = Identity.GetUserId(User.Identity);
            try
            {
                var fileModel = _fieldService.Get(userId, fieldId);
                return File(fileModel.InputStream,
                            System.Net.Mime.MediaTypeNames.Application.Octet,
                            fileModel.FileName);
            }
            catch (ArgumentException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        [HttpGet]
        [Route("Update/{fieldId}/{fieldName}")]
        public ActionResult Upate(Guid fieldId, string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guid userId = Identity.GetUserId(User.Identity);
            _fieldService.Update(userId, fieldId, fieldName);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        [HttpGet]
        [Route("Display/{fieldId}")]
        public ActionResult Display(Guid fieldId)
        {
            Guid userId = Identity.GetUserId(User.Identity);

            var fileModel = _fieldService.Get(userId, fieldId);
            return new FileContentResult(ByteHelper.ReadToEnd(fileModel.InputStream), "application/pdf");
        }
    }
}