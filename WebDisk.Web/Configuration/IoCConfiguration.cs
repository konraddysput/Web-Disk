using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDisk.BusinessLogic.Services;
using WebDisk.BusinessLogic.Services.Interfaces;

namespace WebDisk.Web.Configuration
{
    public class IoCConfiguration
    {
        public static void ConfigureIocUnityContainer()
        {
            IUnityContainer container = new UnityContainer();

            RegisterServices(container);

            DependencyResolver.SetResolver(new WebDiskDependencyResolver(container));
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IFileServiceProvider, FileServiceProvider>(new PerResolveLifetimeManager());
            
        }
    }
}