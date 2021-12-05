using Digitalroot.OdinPlusModUploader.Clients;
using Digitalroot.OdinPlusModUploader.Enums;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Clients;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Configuration;
using System;

namespace Digitalroot.OdinPlusModUploader;

public static class OdinPlusModUploaderClientFactory
{
  public static T CreateInstance<T>(ModHostProvider provider)
    where T : AbstractRestClient
  {
    return provider switch
    {
      ModHostProvider.NexusMods => new NexusModsRestClient(new NexusModsHostProviderConfiguration()) as T
      , ModHostProvider.ModVault => throw new ArgumentOutOfRangeException(nameof(provider), provider, null)
      , ModHostProvider.Thunderstore => throw new ArgumentOutOfRangeException(nameof(provider), provider, null)
      , _ => throw new ArgumentOutOfRangeException(nameof(provider), provider, null)
    };
  }
}
