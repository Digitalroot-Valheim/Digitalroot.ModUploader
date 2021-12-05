using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

internal class CheckApiKeyRequest : AbstractAuthorizedRequest
{
  public CheckApiKeyRequest(ApiKeyRequestModel apiKeyRequestModel)
    : base(apiKeyRequestModel, "v1/users/validate.json", Method.GET) { }
}
