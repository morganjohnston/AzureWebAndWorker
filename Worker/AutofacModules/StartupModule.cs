using Autofac;

namespace Worker.AutofacModules
{
    public class StartupModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Startup>();
        }
    }
}