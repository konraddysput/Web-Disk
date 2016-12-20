using Microsoft.Practices.Unity;
using System.Web.Mvc;
using WebDisk.BusinessLogic.Interfaces;
using WebDisk.BusinessLogic.Services;
using WebDisk.Web.Configuration;

namespace WebDisk.Web.App_Start
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
            container.RegisterType<ISpaceService, SpaceService>();
            container.RegisterType<IDirectoryService, DirectoryService>();

        }
    }
}