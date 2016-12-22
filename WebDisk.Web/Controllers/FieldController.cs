using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDisk.BusinessLogic.Services;
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
        
        public FieldController(DirectoryService directoryService)
        {
            _directoryService = directoryService;
        }

        /// <summary>
        /// Get file details like creation date, access mode, owner informations
        /// </summary>
        /// <param name="fieldId">id of field that we're looking for</param>
        /// <returns>Json with field informations</returns>
        [Route("Details/{fieldId}")]
        public ActionResult Details(Guid fieldId)
        {
            Guid userId = Identity.GetUserId(User.Identity);
            return Json(_directoryService
                            .GetFieldDetails(userId, fieldId),
                            JsonRequestBehavior.AllowGet);
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

        [Route("Create/{fieldId}/{fieldName}")]
        public ActionResult Create(Guid fieldId, string fieldName)
        {
            throw new NotImplementedException();
        }
    }
}