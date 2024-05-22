using System;

namespace Digitalroot.ModUploader.Provider.NexusMods.Models;

internal class UploadChunkExistsRequestModel : UploadFileMetaDataRequestModel
{
  /// <inheritdoc />
  // ReSharper disable once MemberCanBePrivate.Global
  internal UploadChunkExistsRequestModel(string nexusmodsSession
                                         , string resumableFilename
                                         , ulong resumableTotalSize
                                         , uint resumableChunkNumber
                                         , uint resumableCurrentChunkSize
                                         , uint resumableTotalChunks)
    : base(nexusmodsSession
           , resumableFilename
           , resumableTotalSize
           , resumableChunkNumber
           , resumableCurrentChunkSize
           , resumableTotalChunks) { }

  internal UploadChunkExistsRequestModel(string nexusmodsSession
                                         , string resumableFilename
                                         , long resumableTotalSize
                                         , uint resumableChunkNumber
                                         , uint resumableCurrentChunkSize
                                         , uint resumableTotalChunks)
    : this(nexusmodsSession
           , resumableFilename
           , Convert.ToUInt64(resumableTotalSize)
           , resumableChunkNumber
           , resumableCurrentChunkSize
           , resumableTotalChunks) { }
}
