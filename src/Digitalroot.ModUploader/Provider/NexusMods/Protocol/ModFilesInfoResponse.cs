using Digitalroot.ModUploader.Protocol;
using Digitalroot.ModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.ModUploader.Provider.NexusMods.Protocol;

internal class ModFilesInfoResponse : AbstractResponse<ModFilesInfoResponseModel>
{
  /// <inheritdoc />
  public ModFilesInfoResponse(RestResponse<ModFilesInfoResponseModel> response)
    : base(response) { }
}
