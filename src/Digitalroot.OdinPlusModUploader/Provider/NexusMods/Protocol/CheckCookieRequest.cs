using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

internal class CheckCookieRequest : AbstractAuthorizedRequest
{
  public CheckCookieRequest(CookieRequestModel cookieRequestModel)
    : base(cookieRequestModel, "Core/Libs/Common/Widgets/MyModerationHistoryTab", Method.GET) { }
}
