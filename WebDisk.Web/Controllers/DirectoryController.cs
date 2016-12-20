using System;
using System.Web.Mvc;

namespace WebDisk.Web.Controllers
{
    [Authorize]
    [RoutePrefix("Directory")]
    public class DirectoryController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
        
        [Route("{directoryId}")]
        public ActionResult Index(Guid directoryId)
        {
            throw new NotImplementedException();
        }

        [Route("Details/{directoryId}")]
        public ActionResult Details(Guid directoryId)
        {
            throw new NotImplementedException();
            
        }

        [Route("Update/{directoryId}")]
        public ActionResult Update(Guid directoryId)
        {
            throw new NotImplementedException();
        }


        [Route("Delete/{directoryId}")]
        public ActionResult Delete(Guid directoryId)
        {
            throw new NotImplementedException();
        }
        
        [Route("Create/{directoryId}/{directoryName}")]
        public ActionResult Create(Guid directoryId, string directoryName)
        {
            throw new NotImplementedException();
        }

    }
}