using System;
using Autofac;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Worker.Infrastructure;

namespace Worker
{
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            IoC.Startup();
            var container = IoC.Container;
            var startup = container.Resolve<Startup>();
            startup.Start();
            var config = new JobHostConfiguration
            {
                JobActivator = new AutofacWebJobActivator(container),
            };
            config.UseServiceBus(new ServiceBusConfiguration
            {
                MessageOptions = new OnMessageOptions
                {
                    AutoRenewTimeout = TimeSpan.FromHours(1), // Auto renew messages for up to 1 hour.
                },
            });
            var host = new JobHost(config);
            host.RunAndBlock();
        }
    }
}
