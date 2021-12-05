using Digitalroot.OdinPlusModUploader.Models;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;

// ReSharper disable once ClassNeverInstantiated.Global
internal class CheckCookieResponseModel : AbstractResponseModel
{
  // ReSharper disable once MemberCanBePrivate.Global
  internal readonly bool IsCookieValid;

  public CheckCookieResponseModel(CheckCookieResponse checkCookieResponse)
  {
    IsCookieValid = checkCookieResponse.IsSuccessful && !checkCookieResponse.Content.Contains("og:") && !checkCookieResponse.Content.Contains("Error");
  }
}
