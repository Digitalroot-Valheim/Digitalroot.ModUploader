using Digitalroot.OdinPlusModUploader.Protocol;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

// ReSharper disable once ClassNeverInstantiated.Global
internal class CheckFileStatusResponse : AbstractResponse<CheckFileStatusResponseModel>
{
  /// <inheritdoc />
  public CheckFileStatusResponse(IRestResponse<CheckFileStatusResponseModel> response)
    : base(response) { }
}
