using Autofac;
using Web.Infrastructure;

namespace Web.AutofacModules
{
    public class BusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BusSender>().AsImplementedInterfaces();
        }
    }
}