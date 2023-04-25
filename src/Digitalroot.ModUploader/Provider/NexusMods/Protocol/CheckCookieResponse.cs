using Digitalroot.ModUploader.Protocol;
using RestSharp;

namespace Digitalroot.ModUploader.Provider.NexusMods.Protocol;

// ReSharper disable once ClassNeverInstantiated.Global
internal class CheckCookieResponse : AbstractResponse
{
  /// <inheritdoc />
  public CheckCookieResponse(RestResponse response)
    : base(response) { }
}
