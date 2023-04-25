using Digitalroot.ModUploader.Protocol;
using Digitalroot.ModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.ModUploader.Provider.NexusMods.Protocol;

// ReSharper disable once ClassNeverInstantiated.Global
internal class UploadFileChunkResponse : AbstractResponse<UploadFileChunkResponseModel>
{
  /// <inheritdoc />
  public UploadFileChunkResponse(RestResponse<UploadFileChunkResponseModel> response)
    : base(response) { }
}
