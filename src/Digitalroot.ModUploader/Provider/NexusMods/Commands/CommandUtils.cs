using Digitalroot.ModUploader.Enums;
using Digitalroot.ModUploader.Provider.NexusMods.Clients;

namespace Digitalroot.ModUploader.Provider.NexusMods.Commands;

internal static class CommandUtils
{
  internal static readonly NexusModsRestClient RestClient = ModUploaderClientFactory.CreateInstance<NexusModsRestClient>(ModHostProvider.NexusMods);
}
