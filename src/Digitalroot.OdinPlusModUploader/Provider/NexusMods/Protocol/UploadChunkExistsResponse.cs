using Digitalroot.OdinPlusModUploader.Protocol;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

// ReSharper disable once ClassNeverInstantiated.Global
internal class UploadChunkExistsResponse : AbstractResponse<UploadChunkExistsChunkResponseModel>
{
  // ReSharper disable once MemberCanBePrivate.Global
  internal readonly bool Exists;
  internal UploadChunkExistsRequestModel RequestModel { get; set; }

  /// <inheritdoc />
  public UploadChunkExistsResponse(RestResponse<UploadChunkExistsChunkResponseModel> response)
    : base(response)
  {
    Exists = IsSuccessful && !string.IsNullOrEmpty(Content);
  }
}
