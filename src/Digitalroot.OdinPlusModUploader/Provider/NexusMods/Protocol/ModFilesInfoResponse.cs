using Digitalroot.OdinPlusModUploader.Protocol;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

internal class ModFilesInfoResponse : AbstractResponse<ModFilesInfoResponseModel>
{
  /// <inheritdoc />
  public ModFilesInfoResponse(RestResponse<ModFilesInfoResponseModel> response)
    : base(response) { }
}
