using System;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;

internal class UploadChunkExistsRequestModel : UploadFileMetaDataRequestModel
{
  /// <inheritdoc />
  // ReSharper disable once MemberCanBePrivate.Global
  internal UploadChunkExistsRequestModel(string cookie
                                        , string resumableFilename
                                        , ulong resumableTotalSize
                                        , uint resumableChunkNumber
                                        , uint resumableCurrentChunkSize
                                        , uint resumableTotalChunks)
    : base(cookie
           , resumableFilename
           , resumableTotalSize
           , resumableChunkNumber
           , resumableCurrentChunkSize
           , resumableTotalChunks) { }

  internal UploadChunkExistsRequestModel(string cookie
                                        , string resumableFilename
                                        , long resumableTotalSize
                                        , uint resumableChunkNumber
                                        , uint resumableCurrentChunkSize
                                        , uint resumableTotalChunks)
    : this(cookie
           , resumableFilename
           , Convert.ToUInt64(resumableTotalSize)
           , resumableChunkNumber
           , resumableCurrentChunkSize
           , resumableTotalChunks) { }
}
