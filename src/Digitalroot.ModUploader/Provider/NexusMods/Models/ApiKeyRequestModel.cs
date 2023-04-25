using Digitalroot.ModUploader.Models;

namespace Digitalroot.ModUploader.Provider.NexusMods.Models;

internal class ApiKeyRequestModel : AbstractRequestModel
{
  internal string ApiKey { get; }

  internal ApiKeyRequestModel(string apiKey)
  {
    ApiKey = apiKey;
  }
}
