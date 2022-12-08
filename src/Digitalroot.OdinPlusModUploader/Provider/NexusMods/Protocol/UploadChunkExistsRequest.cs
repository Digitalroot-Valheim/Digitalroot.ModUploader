using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

internal class UploadChunkExistsRequest : AbstractAuthorizedRequest
{
  public UploadChunkExistsRequest(UploadChunkExistsRequestModel uploadChunkExistsRequestModel)
    : base(uploadChunkExistsRequestModel, "uploads/chunk", Method.Get)
  {
    this.AddQueryParameter("resumableChunkNumber", uploadChunkExistsRequestModel.ResumableChunkNumber.ToString());
    this.AddQueryParameter("resumableChunkSize", uploadChunkExistsRequestModel.ResumableChunkSize.ToString());
    this.AddQueryParameter("resumableCurrentChunkSize", uploadChunkExistsRequestModel.ResumableCurrentChunkSize.ToString());
    this.AddQueryParameter("resumableTotalSize", uploadChunkExistsRequestModel.ResumableTotalSize.ToString());
    this.AddQueryParameter("resumableType", uploadChunkExistsRequestModel.ResumableType, true);
    this.AddQueryParameter("resumableIdentifier", uploadChunkExistsRequestModel.ResumableIdentifier, true);
    this.AddQueryParameter("resumableFilename", uploadChunkExistsRequestModel.ResumableFilename, true);
    this.AddQueryParameter("resumableRelativePath", uploadChunkExistsRequestModel.ResumableRelativePath, true);
    this.AddQueryParameter("resumableTotalChunks", uploadChunkExistsRequestModel.ResumableTotalChunks.ToString());
  }
}
