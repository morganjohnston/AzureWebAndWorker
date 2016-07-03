using System;
using System.Collections.Generic;
using Serilog;
using Serilog.Events;
using Shared;
using Shared.Configuration;

namespace Worker
{
    public class Startup
    {
        private readonly SeqServerUri seqServerUri;
        private readonly AppEnvironment appEnvironment;

        public Startup(SeqServerUri seqServerUri, AppEnvironment appEnvironment)
        {
            this.seqServerUri = seqServerUri;
            this.appEnvironment = appEnvironment;
        }

        public void Start()
        {
            ConfigureLogging();
        }

        private void ConfigureLogging()
        {
            var devEnvironments = new List<EnvironmentName> { EnvironmentName.Local, EnvironmentName.CI };
            var isDevelopmentEnvironment = devEnvironments.Contains(appEnvironment);
            var minimumLoggingLevel = isDevelopmentEnvironment ? LogEventLevel.Debug : LogEventLevel.Information;

            var logger = new LoggerConfiguration()
                .MinimumLevel.Is(minimumLoggingLevel)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Source", "Worker")
                .Enrich.WithProperty("EnvironmentName", appEnvironment)
                .Enrich.WithProperty("ServiceAccount", Environment.UserName)
                .Enrich.WithProperty("ApplicationVersion", GetType().Assembly.GetName().Version)
                .WriteTo.ColoredConsole()
                //.WriteTo.Seq(seqServerUri.ToString())
                .CreateLogger();

            Log.Logger = logger;
        }
    }
}