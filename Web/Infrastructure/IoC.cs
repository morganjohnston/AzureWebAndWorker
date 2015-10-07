using System;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using Web.AutofacModules;

namespace Web.Infrastructure
{
    public static class IoC
    {
        private static IContainer container;

        public static void Startup()
        {
            CreateContainer(ContainerBuildOptions.None);
        }

        public static void Shutdown()
        {
            try
            {
                container?.Dispose();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        internal static IContainer CreateContainer(ContainerBuildOptions options)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyModules(typeof(BusModule).Assembly);
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();
            container = builder.Build(options);

            return container;
        }

        public static IContainer Container => container;
    }
}