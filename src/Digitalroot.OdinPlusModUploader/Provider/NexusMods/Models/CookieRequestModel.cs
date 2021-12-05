using Digitalroot.OdinPlusModUploader.Models;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;

internal class CookieRequestModel : AbstractRequestModel
{
  internal string Cookie { get; }

  internal CookieRequestModel(string cookie)
  {
    Cookie = cookie;
  }
}
