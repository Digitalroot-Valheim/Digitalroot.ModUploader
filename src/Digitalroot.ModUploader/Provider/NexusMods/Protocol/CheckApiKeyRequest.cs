using Digitalroot.ModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.ModUploader.Provider.NexusMods.Protocol;

internal class CheckApiKeyRequest : AbstractAuthorizedRequest
{
  public CheckApiKeyRequest(ApiKeyRequestModel apiKeyRequestModel)
    : base(apiKeyRequestModel, "v1/users/validate.json", Method.Get) { }
}
