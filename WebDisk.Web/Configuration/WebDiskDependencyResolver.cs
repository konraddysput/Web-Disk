using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebDisk.Web.Configuration
{
    internal class WebDiskDependencyResolver : IDependencyResolver
    {
        private IUnityContainer _unitContainer;

        public WebDiskDependencyResolver(IUnityContainer unityContainer)
        {
            _unitContainer = unityContainer;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _unitContainer.Resolve(serviceType);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _unitContainer.ResolveAll(serviceType);
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }
    }
}