using Digitalroot.OdinPlusModUploader.Models;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;

internal abstract class AbstractAuthorizedRequestModel : AbstractRequestModel
{
  internal string ApiKey { get; }
  internal string Cookie { get; }

  internal AbstractAuthorizedRequestModel(string apiKey, string cookie)
  {
    Cookie = cookie;
    ApiKey = apiKey;
  }
}
