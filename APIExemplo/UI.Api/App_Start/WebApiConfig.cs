using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using UI.Api.Seguranca;

namespace UI.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var exigeToken = new ExigeToken { InnerHandler = new HttpControllerDispatcher(config) };

            config.Routes.MapHttpRoute(
                name: "Autenticar",
                routeTemplate: "api/login/{id}",
                defaults: new { controller = "Login" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                /*constraints: null,
                handler: exigeToken*/
            );
            config.EnableSystemDiagnosticsTracing();

            //config.MessageHandlers.Add(new ExigeHTTPS()); //Exige HTTPS nas resquisições
        }
    }
}
