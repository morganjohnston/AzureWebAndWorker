﻿using System.Reflection;
using Autofac;
using ConfigInjector.Configuration;
using Module = Autofac.Module;
using ServiceBusConnectionString = Web.Configuration.ServiceBusConnectionString;

namespace Web.AutofacModules
{
    public class ConfigurationSettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConfigurationConfigurator.RegisterConfigurationSettings()
                .FromAssemblies(Assembly.GetAssembly(typeof(ServiceBusConnectionString)), ThisAssembly)
                .RegisterWithContainer(configSetting => builder.RegisterInstance(configSetting)
                    .AsSelf()
                    .SingleInstance())
                .AllowConfigurationEntriesThatDoNotHaveSettingsClasses(true)
                .DoYourThing();
        }
    }
}