using System.Reflection;
using Autofac;
using ConfigInjector.Configuration;
using Shared.Configuration;
using Module = Autofac.Module;

namespace Worker.AutofacModules
{
    public class ConfigurationSettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConfigurationConfigurator.RegisterConfigurationSettings()
                .FromAssemblies(new[]{
                    Assembly.GetAssembly(typeof(AppEnvironment)),
                    ThisAssembly
                })
                .RegisterWithContainer(configSetting => builder.RegisterInstance(configSetting)
                    .AsSelf()
                    .SingleInstance())
                .AllowConfigurationEntriesThatDoNotHaveSettingsClasses(true)
                .ExcludeSettingKeys("ServiceBusConnectionString")
                .DoYourThing();
        }
    }
}