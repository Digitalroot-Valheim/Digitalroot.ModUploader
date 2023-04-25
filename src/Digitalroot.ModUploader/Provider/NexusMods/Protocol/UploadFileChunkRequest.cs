using Digitalroot.ModUploader.Provider.NexusMods.Clients;
using Digitalroot.ModUploader.Provider.NexusMods.Models;
using RestSharp;
using System;
using System.IO;

namespace Digitalroot.ModUploader.Provider.NexusMods.Protocol;

internal class UploadFileChunkRequest : AbstractAuthorizedRequest
{
  public UploadFileChunkRequest(UploadFileChunkRequestModel uploadFileChunkRequestModel)
    : base(uploadFileChunkRequestModel, "uploads/chunk", Method.Post)
  {
    this.AddParameter("resumableChunkNumber", uploadFileChunkRequestModel.ResumableChunkNumber);
    this.AddParameter("resumableChunkSize", uploadFileChunkRequestModel.ResumableChunkSize);
    this.AddParameter("resumableCurrentChunkSize", uploadFileChunkRequestModel.ResumableCurrentChunkSize);
    this.AddParameter("resumableTotalSize", uploadFileChunkRequestModel.ResumableTotalSize);
    this.AddParameter("resumableType", uploadFileChunkRequestModel.ResumableType);
    this.AddParameter("resumableIdentifier", uploadFileChunkRequestModel.ResumableIdentifier);
    this.AddParameter("resumableFilename", uploadFileChunkRequestModel.ResumableFilename);
    this.AddParameter("resumableRelativePath", uploadFileChunkRequestModel.ResumableRelativePath);
    this.AddParameter("resumableTotalChunks", uploadFileChunkRequestModel.ResumableTotalChunks);
    // this.AddFileBytes("file", uploadFileChunkRequestModel.Buffer, "blob");
    this.AddFile("file", uploadFileChunkRequestModel.Buffer, "blob");
  }

  private static int GetChunkCount(FileInfo archiveFile)
  {
    var count = Convert.ToUInt64(archiveFile.Length) / NexusModsRestClient.ChunkSize;
    if (Convert.ToUInt64(archiveFile.Length) % NexusModsRestClient.ChunkSize != 0) count++;
    return count == 0 ? 1 : Convert.ToInt32(count);
  }
}
