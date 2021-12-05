using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Clients;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;
using System;
using System.IO;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

internal class UploadFileChunkRequest : AbstractAuthorizedRequest
{
  public UploadFileChunkRequest(UploadFileChunkRequestModel uploadFileChunkRequestModel)
    : base(uploadFileChunkRequestModel, "uploads/chunk", Method.POST)
  {
    AddParameter("resumableChunkNumber", uploadFileChunkRequestModel.ResumableChunkNumber);
    AddParameter("resumableChunkSize", uploadFileChunkRequestModel.ResumableChunkSize);
    AddParameter("resumableCurrentChunkSize", uploadFileChunkRequestModel.ResumableCurrentChunkSize);
    AddParameter("resumableTotalSize", uploadFileChunkRequestModel.ResumableTotalSize);
    AddParameter("resumableType", uploadFileChunkRequestModel.ResumableType);
    AddParameter("resumableIdentifier", uploadFileChunkRequestModel.ResumableIdentifier);
    AddParameter("resumableFilename", uploadFileChunkRequestModel.ResumableFilename);
    AddParameter("resumableRelativePath", uploadFileChunkRequestModel.ResumableRelativePath);
    AddParameter("resumableTotalChunks", uploadFileChunkRequestModel.ResumableTotalChunks);
    AddFileBytes("file", uploadFileChunkRequestModel.Buffer, "blob");
  }

  private static int GetChunkCount(FileInfo archiveFile)
  {
    var count = Convert.ToUInt64(archiveFile.Length) / NexusModsRestClient.ChunkSize;
    if (Convert.ToUInt64(archiveFile.Length) % NexusModsRestClient.ChunkSize != 0) count++;
    return count == 0 ? 1 : Convert.ToInt32(count);
  }
}
