﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Owin.Types;

namespace HelloWorldOwinTypes
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    // Note the Web.Config owin:HandleAllRequests setting that is used to direct all requests to your OWIN application.
    // Alternatively you can specify routes in the global.asax file.
    public class Startup
    {
        // Invoked once at startup to configure your application.
        public void Configuration(IAppBuilder builder)
        {
            builder.UseHandler(new StartupExtensions.OwinHandlerAsync(Invoke));
        }

        // Invoked once per request.
        public Task Invoke(OwinRequest request, OwinResponse response)
        {
            string responseText = "Hello World";
            byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);

            // TODO: Should Content-Length be a strong property?
            response.SetHeader("Content-Length", responseBytes.Length.ToString(CultureInfo.InvariantCulture));
            response.SetHeader("Content-Type", "text/plain");

            // TODO: OwinResponse needs a WriteAsync method.
            Stream responseStream = response.Body;
            return Task.Factory.FromAsync(responseStream.BeginWrite, responseStream.EndWrite, responseBytes, 0, responseBytes.Length, null);
            // 4.5: return responseStream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }
    }
}