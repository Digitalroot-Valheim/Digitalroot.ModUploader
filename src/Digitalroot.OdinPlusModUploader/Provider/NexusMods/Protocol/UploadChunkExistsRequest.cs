using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

internal class UploadChunkExistsRequest : AbstractAuthorizedRequest
{
  public UploadChunkExistsRequest(UploadChunkExistsRequestModel uploadChunkExistsRequestModel)
    : base(uploadChunkExistsRequestModel, "uploads/chunk", Method.GET)
  {
    AddQueryParameter("resumableChunkNumber", uploadChunkExistsRequestModel.ResumableChunkNumber.ToString());
    AddQueryParameter("resumableChunkSize", uploadChunkExistsRequestModel.ResumableChunkSize.ToString());
    AddQueryParameter("resumableCurrentChunkSize", uploadChunkExistsRequestModel.ResumableCurrentChunkSize.ToString());
    AddQueryParameter("resumableTotalSize", uploadChunkExistsRequestModel.ResumableTotalSize.ToString());
    AddQueryParameter("resumableType", uploadChunkExistsRequestModel.ResumableType, true);
    AddQueryParameter("resumableIdentifier", uploadChunkExistsRequestModel.ResumableIdentifier, true);
    AddQueryParameter("resumableFilename", uploadChunkExistsRequestModel.ResumableFilename, true);
    AddQueryParameter("resumableRelativePath", uploadChunkExistsRequestModel.ResumableRelativePath, true);
    AddQueryParameter("resumableTotalChunks", uploadChunkExistsRequestModel.ResumableTotalChunks.ToString());
  }
}
