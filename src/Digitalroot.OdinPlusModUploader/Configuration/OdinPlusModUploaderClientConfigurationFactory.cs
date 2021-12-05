using Digitalroot.OdinPlusModUploader.Enums;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Configuration;
using System;

namespace Digitalroot.OdinPlusModUploader.Configuration;

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
