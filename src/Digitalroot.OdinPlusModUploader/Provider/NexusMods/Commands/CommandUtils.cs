using Digitalroot.OdinPlusModUploader.Enums;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Clients;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Commands;

internal static class CommandUtils
{
  internal static readonly NexusModsRestClient RestClient = OdinPlusModUploaderClientFactory.CreateInstance<NexusModsRestClient>(ModHostProvider.NexusMods);
}
