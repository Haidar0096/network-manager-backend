using Autofac;
using Autofac.Integration.WebApi;
using NetworkManagerApi.Common.Logger;
using NetworkManagerApi.Repositories;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace NetworkManagerApi.Dependency_Injection
{
    public class AutofacWebApiConfig
    {

        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            //Register Web API controllers.  
            builder.RegisterApiControllers(assembly);

            // Register Service classes
            builder.RegisterAssemblyTypes(assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .InstancePerDependency();


            // Register Repository classes
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            // Register MSSQLDeviceManagerRepository explicitly as IDeviceManagerRepository
            builder.RegisterType<MSSQLDeviceRepository>()
                   .As<IDeviceRepository>()
                   .InstancePerDependency();

            // Register FileLogger
            builder.RegisterType<FileLogger>()
                .As<ILogger>()
                .SingleInstance();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }

    }
}