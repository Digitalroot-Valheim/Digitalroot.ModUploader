using System;

namespace Digitalroot.ModUploader.Provider.NexusMods.Models;

internal class UploadChunkExistsRequestModel : UploadFileMetaDataRequestModel
{
  /// <inheritdoc />
  // ReSharper disable once MemberCanBePrivate.Global
  internal UploadChunkExistsRequestModel(string cookiesid_develop
                                        , string resumableFilename
                                        , ulong resumableTotalSize
                                        , uint resumableChunkNumber
                                        , uint resumableCurrentChunkSize
                                        , uint resumableTotalChunks)
    : base(cookiesid_develop
           , resumableFilename
           , resumableTotalSize
           , resumableChunkNumber
           , resumableCurrentChunkSize
           , resumableTotalChunks) { }

  internal UploadChunkExistsRequestModel(string cookieCookiesid
                                        , string resumableFilename
                                        , long resumableTotalSize
                                        , uint resumableChunkNumber
                                        , uint resumableCurrentChunkSize
                                        , uint resumableTotalChunks)
    : this(cookieCookiesid
           , resumableFilename
           , Convert.ToUInt64(resumableTotalSize)
           , resumableChunkNumber
           , resumableCurrentChunkSize
           , resumableTotalChunks) { }
}
