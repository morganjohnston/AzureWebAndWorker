using Autofac;
using Microsoft.Azure.WebJobs.Host;

namespace Worker.Infrastructure
{
    public class AutofacWebJobActivator : IJobActivator
    {
        private readonly IContainer container;

        public AutofacWebJobActivator(IContainer container)
        {
            this.container = container;
        }
        public T CreateInstance<T>()
        {
            return container.Resolve<T>();
        }
    }
}