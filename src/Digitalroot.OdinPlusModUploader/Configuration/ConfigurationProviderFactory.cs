using Digitalroot.OdinPlusModUploader.Enums;
using Digitalroot.OdinPlusModUploader.Interfaces;
using System;

namespace Digitalroot.OdinPlusModUploader.Configuration;

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
