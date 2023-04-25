using Digitalroot.ModUploader.Protocol;
using Digitalroot.ModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.ModUploader.Provider.NexusMods.Protocol;

// ReSharper disable once ClassNeverInstantiated.Global
internal class CheckFileStatusResponse : AbstractResponse<CheckFileStatusResponseModel>
{
  /// <inheritdoc />
  public CheckFileStatusResponse(RestResponse<CheckFileStatusResponseModel> response)
    : base(response) { }
}
