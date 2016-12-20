using System.Web.Mvc;

namespace WebDisk.Web.Attributes.Common
{
    public abstract class BaseActionFilter : IActionFilter, IResultFilter
    {
        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }


        public virtual void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }


        public virtual void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }


        public virtual void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }
    }
}