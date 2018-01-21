using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Routing;

namespace Cut
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Directory.CreateDirectory(HttpRuntime.AppDomainAppPath + "db");
            Directory.CreateDirectory(HttpRuntime.AppDomainAppPath + "db\\");
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
        protected void Application_End()
        {
        }
    }
}
