using Autofac;
using Microsoft.Azure.WebJobs;
using Worker.Infrastructure;

namespace Worker
{
    class Program
    {
        static void Main()
        {
            IoC.Startup();
            var container = IoC.Container;
            var startup = container.Resolve<Startup>();
            startup.Start();
            var config = new JobHostConfiguration
            {
                JobActivator = new AutofacWebJobActivator(container)
            };
            var host = new JobHost(config);
            host.RunAndBlock();
        }
    }
}
