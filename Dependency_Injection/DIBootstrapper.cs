using System.Web.Http;

namespace NetworkManagerApi.Dependency_Injection
{
    public class DIBootstrapper
    {

        public static void Run()
        {
            //Configure AutoFac  
            AutofacWebApiConfig.Initialize(GlobalConfiguration.Configuration);
        }

    }
}