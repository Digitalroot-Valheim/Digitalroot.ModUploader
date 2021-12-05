using System;
using System.IO;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;

internal class UploadFileChunkRequestModel : UploadFileMetaDataRequestModel
{
  public uint ModId { get; }
  public FileInfo ArchiveFile { get; }
  public string FileName { get; }
  public string Version { get; }
  public string Game { get; }
  public bool RemoveDownloadWithManager { get; }
  public bool NoVersionUpdate { get; }
  public bool SetMainVortex { get; }
  public byte[] Buffer { get; }

  /// <inheritdoc />
  internal UploadFileChunkRequestModel(string cookie
                                     , uint modId
                                     , FileInfo archiveFile
                                     , string fileName
                                     , string version
                                     , string game
                                     , bool removeDownloadWithManager
                                     , bool noVersionUpdate
                                     , bool setMainVortex
                                     , uint resumableChunkNumber
                                     , uint resumableCurrentChunkSize
                                     , uint resumableTotalChunks
                                     , byte[] buffer)
    : base(cookie
           , archiveFile.Name
           , Convert.ToUInt64(archiveFile.Length)
           , resumableChunkNumber
           , resumableCurrentChunkSize
           , resumableTotalChunks)
  {
    ModId = modId;
    ArchiveFile = archiveFile;
    FileName = fileName;
    Version = version;
    Game = game;
    RemoveDownloadWithManager = removeDownloadWithManager;
    NoVersionUpdate = noVersionUpdate;
    SetMainVortex = setMainVortex;
    Buffer = buffer;
  }
}
