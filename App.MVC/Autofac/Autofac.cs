using App.API;
using App.Core;
using App.LinkStorage;
using App.LinkStorage.API;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using System.Web.Mvc;

namespace short_link_tasts.Autofac
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<LinkStorage>().As<LinkStorage>().As<ILinkStorage>().InstancePerRequest();
            builder.RegisterType<ShortLinkService>().As<ShortLinkService>().As<IShortLinkService>().InstancePerRequest();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}