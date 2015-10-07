using System;
using Autofac;
using Autofac.Builder;

namespace Worker.Infrastructure
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
            builder.RegisterAssemblyModules(typeof(Startup).Assembly);
            container = builder.Build(options);

            return container;
        }

        public static IContainer Container => container;
    }
}