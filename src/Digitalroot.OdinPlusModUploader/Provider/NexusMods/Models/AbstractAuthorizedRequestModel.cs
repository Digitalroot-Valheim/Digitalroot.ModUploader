using Digitalroot.OdinPlusModUploader.Models;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;

internal abstract class AbstractAuthorizedRequestModel : AbstractRequestModel
{
  internal string ApiKey { get; }
  internal string CookieNexusId { get; }
  internal string CookieSidDevelop { get; }

  internal AbstractAuthorizedRequestModel(string apiKey, string cookieNexusId, string cookieSidDevelop)
  {
    CookieNexusId = cookieNexusId;
    CookieSidDevelop = cookieSidDevelop;
    ApiKey = apiKey;
  }
}
