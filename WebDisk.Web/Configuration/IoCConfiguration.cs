using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDisk.BusinessLogic.Interfaces;
using WebDisk.BusinessLogic.Services;


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
            container.RegisterType<ISpaceService, SpaceService>(new PerResolveLifetimeManager());

        }
    }
}