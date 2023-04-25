using Digitalroot.ModUploader.Clients;
using Digitalroot.ModUploader.Enums;
using Digitalroot.ModUploader.Provider.NexusMods.Clients;
using Digitalroot.ModUploader.Provider.NexusMods.Configuration;
using System;

namespace Digitalroot.ModUploader;

public static class ModUploaderClientFactory
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
