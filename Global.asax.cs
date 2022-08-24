using NetworkManagerApi.Dependency_Injection;
using NetworkManagerApi.Persistence;
using System.Web.Http;

namespace NetworkManagerApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Configure depndency injection
            DIBootstrapper.Run();

            // Configure the database layer
            PersistenceBootstrapper.Run();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
