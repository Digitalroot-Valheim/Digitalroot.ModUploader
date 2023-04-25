using Digitalroot.ModUploader.Enums;
using Digitalroot.ModUploader.Interfaces;
using System;

namespace Digitalroot.ModUploader.Configuration;

internal static class ConfigurationProviderFactory
{
  internal static IConfigs CreateInstance(ConfigurationProvider provider)
  {
    return provider switch
           {
             ConfigurationProvider.File => new FileConfigurationProvider()
             , ConfigurationProvider.Environment => new EnvironmentConfigurationProvider()
             , _ => throw new ArgumentOutOfRangeException(nameof(provider), provider, null)
           };
  }
}
