using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDisk.Web.Attributes.Common
{
    public class AutoMapFilter : BaseActionFilter
    {
        private readonly Type _sourceType;
        private readonly Type _destType;

        public AutoMapFilter(Type sourceType, Type destType)
        {
            _sourceType = sourceType;
            _destType = destType;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model;

            object viewModel = Mapper.Map(model, _sourceType, _destType);

            filterContext.Controller.ViewData.Model = viewModel;
        }
    }
}