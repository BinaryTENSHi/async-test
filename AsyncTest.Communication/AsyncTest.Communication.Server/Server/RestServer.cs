using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Web.Http;
using AsyncTest.Shared;
using Microsoft.Owin.Hosting;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

namespace AsyncTest.Communication.Server.Server
{
    public class RestServer : IRestServer
    {
        private const string BaseAddress = "http://+:6688";
        private IDisposable _webApp;

        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
        public virtual void Startup(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            using (HttpConfiguration config = new HttpConfiguration())
            {
                // Web API routes
                config.MapHttpAttributeRoutes();

                // Configure Formatters
                config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

                config.EnsureInitialized();

                appBuilder.UseNinjectMiddleware(() => MainKernel.Instance.Kernel);
                appBuilder.UseNinjectWebApi(config);
            }
        }

        public virtual void Start()
        {
            _webApp = WebApp.Start(BaseAddress, Startup);
        }

        public virtual void Stop()
        {
            _webApp?.Dispose();
        }
    }
}