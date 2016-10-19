﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDisk.Web.Attributes.Common;

namespace WebDisk.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AutoMapAttribute : ActionFilterAttribute
    {
        private readonly Type _sourceType;
        private readonly Type _destType;

        public AutoMapAttribute(Type sourceType, Type destType)
        {
            _sourceType = sourceType;
            _destType = destType;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var filter = new AutoMapFilter(SourceType, DestType);

            filter.OnActionExecuted(filterContext);
        }

        public Type SourceType
        {
            get { return _sourceType; }
        }

        public Type DestType
        {
            get { return _destType; }
        }
    }
}