using Autofac;
using Worker.Handlers;

namespace Worker.AutofacModules
{
    public class HandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof (WidgetAddedHandler).Assembly)
                .Where(x => x.IsInNamespaceOf<WidgetAddedHandler>())
                .Where(x => x.Name.EndsWith("Handler"))
                .InstancePerDependency();
        }
    }
}