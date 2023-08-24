using Digitalroot.ModUploader.Provider.NexusMods.Clients;
using System;

namespace Digitalroot.ModUploader.Provider.NexusMods.Models;

internal class UploadFileMetaDataRequestModel : CookieRequestModel
{
  public readonly uint ResumableChunkNumber;
  public readonly uint ResumableChunkSize;
  public readonly uint ResumableCurrentChunkSize;
  public readonly ulong ResumableTotalSize;
  public readonly string ResumableType;
  public readonly string ResumableIdentifier;
  public readonly string ResumableFilename;
  public readonly string ResumableRelativePath;
  public readonly uint ResumableTotalChunks;

  /// <inheritdoc />
  internal UploadFileMetaDataRequestModel(string cookiesid_develop
                                          , string resumableFilename
                                          , ulong resumableTotalSize
                                          , uint resumableChunkNumber
                                          , uint resumableCurrentChunkSize
                                          , uint resumableTotalChunks)
    : base(cookiesid_develop)
  {
    ResumableFilename = resumableFilename;
    ResumableTotalSize = resumableTotalSize;
    ResumableChunkNumber = resumableChunkNumber;
    ResumableChunkSize = Convert.ToUInt32(NexusModsRestClient.ChunkSize);
    ResumableCurrentChunkSize = resumableCurrentChunkSize;
    ResumableType = string.Empty;
    ResumableIdentifier = $"{ResumableTotalSize}-{resumableFilename.Replace(".", string.Empty)}";
    ResumableRelativePath = resumableFilename;
    ResumableTotalChunks = resumableTotalChunks;
  }
}
