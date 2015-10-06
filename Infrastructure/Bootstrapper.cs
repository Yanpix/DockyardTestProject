using System.Configuration;
using System.Web.Mvc;
using BLL.IService;
using BLL.Service;
using DropboxRestAPI;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace Infrastructure
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            var appSettings = ConfigurationManager.AppSettings;

            var options = new Options
            {
                ClientId = appSettings["ClientId"],
                ClientSecret = appSettings["ClientSecret"],
                RedirectUri = appSettings["RedirectUri"],
               // AccessToken = appSettings["AccessToken"]
            };

            container.RegisterType<IDropboxService, DropboxService>();
            container.RegisterType<IClient, Client>(new InjectionConstructor(options));
        }
    }
}
