using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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
        private readonly string _baseAddress;
        private IDisposable _webApp;

        public RestServer()
        {
            _baseAddress = string.Format(CultureInfo.InvariantCulture, "http://{0}:{1}/", "localhost", "6688");
        }

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
            _webApp = WebApp.Start(_baseAddress, Startup);
        }

        public virtual void Stop()
        {
            _webApp?.Dispose();
        }
    }
}