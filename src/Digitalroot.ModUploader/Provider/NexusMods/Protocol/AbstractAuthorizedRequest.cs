using Digitalroot.ModUploader.Models;
using Digitalroot.ModUploader.Protocol;
using Digitalroot.ModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.ModUploader.Provider.NexusMods.Protocol;

internal abstract class AbstractAuthorizedRequest : AbstractRequest
{
  private const string apikey = nameof(apikey);

  public AbstractRequestModel Model { get; private set; }

  protected AbstractAuthorizedRequest(AbstractAuthorizedRequestModel authorizedFileRequestModel, string resource, Method method)
    : base(resource, method)
  {
    Model = authorizedFileRequestModel;
    // RequestFormat = DataFormat.Json;
    // JsonSerializer = NewtonsoftJsonSerializer.Default;
    // AddHeader(Accept, appJson);
    this.AddHeader(apikey, authorizedFileRequestModel.ApiKey);
    // this.AddHeader(apikey, authorizedFileRequestModel.ApiKey);
    
    // this.AddCookie(nexusid, authorizedFileRequestModel.CookieNexusId);
    // this.AddCookie(sid_develop, authorizedFileRequestModel.CookieSidDevelop);
  }

  protected AbstractAuthorizedRequest(CookieRequestModel saveDocumentationChunkRequestModel, string resource, Method method)
    : base(resource, method)
  {
    Model = saveDocumentationChunkRequestModel;
    // RequestFormat = DataFormat.Json;
    // JsonSerializer = NewtonsoftJsonSerializer.Default;
    // AddHeader(Accept, appJson);
    // AddCookie(nexusid, saveDocumentationChunkRequestModel.CookieNexusId);
    // AddCookie(sid_develop, saveDocumentationChunkRequestModel.CookieSidDevelop);
  }

  protected AbstractAuthorizedRequest(ApiKeyRequestModel apiKeyRequestModel, string resource, Method method)
    : base(resource, method)
  {
    Model = apiKeyRequestModel;
    // RequestFormat = DataFormat.Json;
    // JsonSerializer = NewtonsoftJsonSerializer.Default;
    // AddHeader(Accept, appJson);
    this.AddHeader(apikey, apiKeyRequestModel.ApiKey);
  }
}
