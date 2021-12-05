using Digitalroot.OdinPlusModUploader.Protocol;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

// ReSharper disable once ClassNeverInstantiated.Global
internal class UploadFileChunkResponse : AbstractResponse<UploadFileChunkResponseModel>
{
  /// <inheritdoc />
  public UploadFileChunkResponse(IRestResponse<UploadFileChunkResponseModel> response)
    : base(response) { }
}
