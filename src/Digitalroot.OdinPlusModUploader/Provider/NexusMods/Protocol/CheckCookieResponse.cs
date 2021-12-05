using Digitalroot.OdinPlusModUploader.Protocol;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

// ReSharper disable once ClassNeverInstantiated.Global
internal class CheckCookieResponse : AbstractResponse
{
  /// <inheritdoc />
  public CheckCookieResponse(IRestResponse response)
    : base(response) { }
}
