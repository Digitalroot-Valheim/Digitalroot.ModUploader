using Digitalroot.OdinPlusModUploader.Models;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;

internal class ApiKeyRequestModel : AbstractRequestModel
{
  internal string ApiKey { get; }

  internal ApiKeyRequestModel(string apiKey)
  {
    ApiKey = apiKey;
  }
}
