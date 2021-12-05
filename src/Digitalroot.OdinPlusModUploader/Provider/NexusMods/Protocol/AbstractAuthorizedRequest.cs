using Digitalroot.OdinPlusModUploader.Protocol;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using Digitalroot.OdinPlusModUploader.Serialization;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

internal abstract class AbstractAuthorizedRequest : AbstractRequest
{
  protected AbstractAuthorizedRequest(AbstractAuthorizedRequestModel authorizedFileRequestModel, string resource, Method method)
    : base(resource, method)
  {
    // RequestFormat = DataFormat.Json;
    JsonSerializer = NewtonsoftJsonSerializer.Default;
    AddHeader("Accept", "application/json");
    AddHeader("apikey", authorizedFileRequestModel.ApiKey);
    AddCookie("nexusid", authorizedFileRequestModel.Cookie);
  }

  protected AbstractAuthorizedRequest(CookieRequestModel saveDocumentationChunkRequestModel, string resource, Method method)
    : base(resource, method)
  {
    // RequestFormat = DataFormat.Json;
    JsonSerializer = NewtonsoftJsonSerializer.Default;
    AddHeader("Accept", "application/json");
    AddCookie("nexusid", saveDocumentationChunkRequestModel.Cookie);
  }

  protected AbstractAuthorizedRequest(ApiKeyRequestModel apiKeyRequestModel, string resource, Method method)
    : base(resource, method)
  {
    // RequestFormat = DataFormat.Json;
    JsonSerializer = NewtonsoftJsonSerializer.Default;
    AddHeader("Accept", "application/json");
    AddHeader("apikey", apiKeyRequestModel.ApiKey);
  }
}
