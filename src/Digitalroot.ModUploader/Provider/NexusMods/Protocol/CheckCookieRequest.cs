using Digitalroot.ModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.ModUploader.Provider.NexusMods.Protocol;

internal class CheckCookieRequest : AbstractAuthorizedRequest
{
  public CheckCookieRequest(CookieRequestModel cookieRequestModel)
    : base(cookieRequestModel, "Core/Libs/Common/Widgets/MyModerationHistoryTab", Method.Get) { }
}
