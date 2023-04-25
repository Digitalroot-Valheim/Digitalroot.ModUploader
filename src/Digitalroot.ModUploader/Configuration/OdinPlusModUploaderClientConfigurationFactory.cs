using Digitalroot.ModUploader.Enums;
using Digitalroot.ModUploader.Provider.NexusMods.Configuration;
using System;

namespace Digitalroot.ModUploader.Configuration;

public static class OdinPlusModUploaderClientConfigurationFactory
{
  public static AbstractHostProviderConfiguration CreateInstance(ModHostProvider modHostProvider)
  {
    switch (modHostProvider)
    {
      case ModHostProvider.NexusMods:
        return new NexusModsHostProviderConfiguration();

      case ModHostProvider.ModVault:
      case ModHostProvider.Thunderstore:
      default:
        throw new NotImplementedException($"{nameof(modHostProvider)} : {modHostProvider}");
    }
  }
}
