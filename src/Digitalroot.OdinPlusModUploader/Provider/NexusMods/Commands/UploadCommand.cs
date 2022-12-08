using Digitalroot.OdinPlusModUploader.Commands;
using Digitalroot.OdinPlusModUploader.Enums;
using Digitalroot.OdinPlusModUploader.Http;
using Digitalroot.OdinPlusModUploader.Models;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Clients;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Enums;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Validators;
using Digitalroot.OdinPlusModUploader.Utils;
using Pastel;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming

#pragma warning disable CS1998

// ReSharper disable UseObjectOrCollectionInitializer
namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Commands;

internal static class UploadCommand
{
  private static readonly AutoResetEvent _autoEvent = new(false);
  private static bool _isErrored = false;

  internal static ICommand GetUploadCommand()
  {
    var command = new Command("upload", $"Upload a file of {FileSizeFormatter.FormatSize(NexusModsRestClient.MaxFileSize).Pastel(ColorOptions.WarningColor)} or less to nexusmods.com")
    {
      CommandHelper.GetArgument<uint?>("mod-id", "Nexus mod id.", null, ValidatorsFactory.Instance) as Argument ?? throw new InvalidOperationException()
      , CommandHelper.GetArgument<FileInfo>("archive-file", "File to upload.", null, ValidatorsFactory.Instance) as Argument ?? throw new InvalidOperationException()
      , CommandHelper.GetOption<string>(new[] { "--file-name", "-f" }, "Name for the file on Nexus Mods", null, true, optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
      , CommandHelper.GetOption<string>(new[] { "--version", "-v" }, "Version for your uploaded file.", null, true, optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
      , CommandHelper.GetOption(new[] { "--category", "-t" }, "Mod file category", CategoryName.Main, true, optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
      , CommandHelper.GetOption<string>(new[] { "--description", "-d" }, "Description", null, optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
      , CommandHelper.GetOption(new[] { "--game", "-g" }, "Game mod is for.", "valheim", optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
      , CommandHelper.GetOption(new[] { "--disable-main-file-update", "-dmfu" }, "Skips replacing an existing file in the " + "'Main'".Pastel(ColorOptions.EmColor) + " category with the new one.", false, optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
      , CommandHelper.GetOption(new[] { "--disable-download-with-manager", "-ddwm" }, "Removes the " + "'Download With Manager'".Pastel(ColorOptions.EmColor) + " button.", false, optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
      , CommandHelper.GetOption(new[] { "--disable-version-update", "-dvu" }, "Skips updating mod's main version to match this file's version.", false, optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
      , CommandHelper.GetOption(new[] { "--disable-main-vortex", "-dmv" }, "Skips setting file as the main Vortex file.", false, optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
      , CommandHelper.GetOption(new[] { "--disable-requirements-pop-up", "-drpu" }, "Skips informing downloaders of this mod's requirements before they attempt to download this file", false, optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
      , CommandHelper.GetOption(new[] { "--key", "-k" }, "Api Key, ENV: " + "NEXUSMOD_API_KEY".Pastel(ColorOptions.EmColor), CommandUtils.RestClient.GetDefaultConfigValue("NEXUSMOD_API_KEY"), optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
      , CommandHelper.GetOption(new[] { "--cookie_nexusid", "-cnxid" }, "Session Cookie, ENV: " + "NEXUSMOD_COOKIE_NEXUSID".Pastel(ColorOptions.EmColor), CommandUtils.RestClient.GetDefaultConfigValue("NEXUSMOD_COOKIE_NEXUSID"), optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
      , CommandHelper.GetOption(new[] { "--cookie_sid_develop", "-csid" }, "Session Cookie, ENV: " + "NEXUSMOD_COOKIE_SID_DEVELOP".Pastel(ColorOptions.EmColor), CommandUtils.RestClient.GetDefaultConfigValue("NEXUSMOD_COOKIE_SID_DEVELOP"), optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
    };

    command.Handler = GetCommandHandler();
    command.AddValidator(ValidatorsFactory.Instance.GetValidator);
    return command;
  }

  private static ICommandHandler GetCommandHandler()
  {
    return CommandHandler.Create<
      uint           // modId
      , FileInfo     // archiveFile
      , string       // fileName
      , string       // version
      , CategoryName // category
      , string       // description
      , string       // game
      , bool         // disableMainFileUpdate
      , bool         // disableDownloadWithManager
      , bool         // disableVersionUpdate
      , bool         // disableMainVortex
      , bool         // enableRequirementsPopUp
      , string       // key
      , string       // cookie_nexusid
      , string       // cookie_sid_develop
    >(
      async (modId                        // uint? - Arg
             , archiveFile                // FileInfo - Arg
             , fileName                   // string
             , version                    // string
             , category                   // CategoryName
             , description                // string
             , game                       // string
             , disableMainFileUpdate      // bool
             , disableDownloadWithManager // bool
             , disableVersionUpdate       // bool
             , disableMainVortex          // bool
             , disableRequirementsPopUp   // bool
             , key                        // string
             , cookie_nexusid             // string
             , cookie_sid_develop         // string
      ) =>
      {
        // Get Game Info
        var gameInfoMessage = await GameInfo(key, game);
        Console.WriteLine($"Preparing to upload '{archiveFile.Name}' ({FileSizeFormatter.FormatSize(archiveFile.Length)}) as version {version} to Nexus Mods upload Api.".Pastel(ColorOptions.InfoColor));

        // Replace Existing file
        int oldFileId = -1;
        if (!disableMainFileUpdate)
        {
          if (category == CategoryName.Main)
          {
            // Get mods current files
            var modFileInfoMessage = await ModFilesInfo(key, game, modId);

            if (!modFileInfoMessage.Response.IsSuccessful) return;
            var lastUpload = modFileInfoMessage.ResponseModel.Files.FirstOrDefault(fileInfo => fileInfo.CategoryId == Convert.ToUInt32(CategoryName.Main));
            PrintLastUploadedFileInfo(lastUpload);
            if (lastUpload is { FileId: > 0 }) oldFileId = lastUpload.FileId;
          }
          else
          {
            Console.WriteLine($"Category ({category}) is not 'Main' skipping replacement of an existing file in the 'Main' category with the new one. ".Pastel(ColorOptions.WarningColor));
          }
        }

        // Check file size and get chunkCount
        var totalChunks = GetChunkCount(archiveFile);

        // Upload all chunks but the last one. 
        var r = Parallel.For(1, totalChunks, new ParallelOptions { MaxDegreeOfParallelism = 2 }, RunUploadWorkFlow);
        RunUploadWorkFlow(totalChunks); // Upload the last file chunk
        _autoEvent.WaitOne();

        if (_isErrored)
        {
          Environment.Exit(1);
        }

        #region Run Workflow

        async void RunUploadWorkFlow(int i)
        {
          try
          {
            var uploadFileChunk = await GetUploadWorkflow(i
                                                          , totalChunks
                                                          , archiveFile
                                                          , cookie_nexusid
                                                          , cookie_sid_develop
                                                          , modId
                                                          , fileName
                                                          , version
                                                          , game
                                                          , disableDownloadWithManager
                                                          , disableVersionUpdate
                                                          , disableMainVortex
                                                          , category
                                                          , description
                                                          , gameInfoMessage
                                                          , disableRequirementsPopUp
                                                          , disableMainFileUpdate
                                                          , oldFileId
                                                          , !disableMainFileUpdate
                                                          , !disableMainFileUpdate
                                                         );
            if (uploadFileChunk == null) return;
            // if (uploadFileChunk.RequestModel.ResumableChunkNumber != totalChunks) return;

            // Report on the results
            if (!uploadFileChunk.ResponseModel.Uuid.HasValue())
            {
              Console.WriteLine("File uploaded to Nexus Mods, but Id is null".Pastel(ColorOptions.ErrorColor));
              Console.WriteLine();
              Console.WriteLine("Response Details:".Pastel(ColorOptions.ErrorColor));
              Console.WriteLine($"{nameof(uploadFileChunk.ResponseModel.UploadFileHash)}: {uploadFileChunk.ResponseModel.UploadFileHash}".Pastel(ColorOptions.ErrorColor));
              Console.WriteLine($"{nameof(uploadFileChunk.ResponseModel.Status)}: {uploadFileChunk.ResponseModel.Status}".Pastel(ColorOptions.ErrorColor));
              Console.WriteLine($"{nameof(uploadFileChunk.ResponseModel.Uuid)}: {uploadFileChunk.ResponseModel.Uuid}".Pastel(ColorOptions.ErrorColor));
            }
            else
            {
              Console.WriteLine($"File successfully uploaded to Nexus Mods with Id '{uploadFileChunk.ResponseModel.Uuid}'".Pastel(ColorOptions.SuccessColor));
            }

            // Check for file assemble
            Message<
              CheckFileStatusRequest,
              CheckFileStatusRequestModel,
              CheckFileStatusResponse,
              CheckFileStatusResponseModel
            > checkFileStatus = await CheckFileStatus(cookie_nexusid, cookie_sid_develop, uploadFileChunk.ResponseModel);

            // Attach file to Mod.
            if (checkFileStatus.Response.IsSuccessful && checkFileStatus.ResponseModel.FileChunksReassembled)
            {
              Console.WriteLine($"File '{checkFileStatus.RequestModel.Uuid}' confirmed as assembled.".Pastel(ColorOptions.SuccessColor));

              // ReSharper disable once UnusedVariable
              Message<
                AddFileToModRequest,
                AddFileToModRequestModel,
                AddFileToModResponse,
                AddFileToModResponseModel
              > addFileToMod = await AddFileToMod(cookie_nexusid
                                                  , cookie_sid_develop
                                                  , modId
                                                  , version
                                                  , disableVersionUpdate
                                                  , disableMainVortex
                                                  , fileName
                                                  , archiveFile
                                                  , category
                                                  , description
                                                  , disableDownloadWithManager
                                                  , disableRequirementsPopUp
                                                  , gameInfoMessage.ResponseModel.Id
                                                  , checkFileStatus.RequestModel.Uuid
                                                  , checkFileStatus.RequestModel.FileHash
                                                  , disableMainFileUpdate
                                                  , oldFileId
                                                  , !disableMainFileUpdate
                                                  , !disableMainFileUpdate
                                                 );

              if (addFileToMod.Response.IsSuccessful)
              {
                Trace.WriteLine("Workflow complete.".Pastel(ColorOptions.StatusColor));
                _autoEvent.Set();
                return;
              }

              throw new Exception("Workflow Failed");
            }
          }
          catch (Exception e)
          {
            Console.WriteLine(e.Message.Pastel(ColorOptions.ErrorColor));
            Console.WriteLine(e.StackTrace.Pastel(ColorOptions.ErrorColor));
            _isErrored = true;
            _autoEvent.Set();
          }
        }

        #endregion
      });
  }

  #region Workflow

  private static async Task<Message<
    UploadFileChunkRequest,
    UploadFileChunkRequestModel,
    UploadFileChunkResponse,
    UploadFileChunkResponseModel
  >> GetUploadWorkflow(int i
                       , int totalChunks
                       , FileInfo archiveFile
                       , string cookieNexusId
                       , string cookiesid_develop
                       , uint modId
                       , string fileName
                       , string version
                       , string game
                       , bool disableDownloadWithManager
                       , bool disableVersionUpdate
                       , bool disableMainVortex
                       , CategoryName categoryName
                       , string description
                       , Message<GameInfoRequest, GameInfoRequestModel, GameInfoResponse, GameInfoResponseModel> gameInfoMessage
                       , bool disableRequirementsPopUp
                       , bool disableMainFileUpdate
                       , int oldFileId
                       , bool newExisting
                       , bool removeOldVersion)
  {
    // Check if chunk already uploaded
    Message<
      UploadChunkExistsRequest,
      UploadChunkExistsRequestModel,
      UploadChunkExistsResponse,
      UploadChunkExistsChunkResponseModel
    > checkUploadChunkExists = await CheckUploadChunkExists(archiveFile
                                                            , cookieNexusId
                                                            , cookiesid_develop
                                                            , Convert.ToUInt32(i)
                                                            , i != totalChunks
                                                                ? Convert.ToUInt32(NexusModsRestClient.ChunkSize)
                                                                : Convert.ToUInt32(Convert.ToUInt64(archiveFile.Length) % NexusModsRestClient.ChunkSize)
                                                            , Convert.ToUInt32(totalChunks)
                                                           );

    if (!checkUploadChunkExists.Response.IsSuccessful) return null;

    // Report Results
    Console.WriteLine(checkUploadChunkExists.Response.Exists
                        ? $"Chunk {checkUploadChunkExists.RequestModel.ResumableChunkNumber} of {checkUploadChunkExists.RequestModel.ResumableTotalChunks} already exist".Pastel(ColorOptions.SuccessColor)
                        : $"Chunk {checkUploadChunkExists.RequestModel.ResumableChunkNumber} of {checkUploadChunkExists.RequestModel.ResumableTotalChunks} does not already exist.".Pastel(ColorOptions.WarningColor));

    // Upload a chunk of the file
    if (checkUploadChunkExists.ResponseModel?.Status ?? false)
    {
      if (i != totalChunks)
      {
        return null; // Chunk already uploaded
      }

      return new Message<UploadFileChunkRequest, UploadFileChunkRequestModel, UploadFileChunkResponse, UploadFileChunkResponseModel>
      {
        ResponseModel = checkUploadChunkExists.ResponseModel
      };
    }

    // Open File
    await using var str = new FileStream(archiveFile.FullName,
                                         FileMode.Open,
                                         FileAccess.Read,
                                         FileShare.Read,
                                         Convert.ToInt32(NexusModsRestClient.ChunkSize),
                                         true);

    var bufferSize = checkUploadChunkExists.RequestModel.ResumableChunkNumber != totalChunks
                       ? Convert.ToUInt32(NexusModsRestClient.ChunkSize)
                       : Convert.ToUInt32(Convert.ToUInt64(archiveFile.Length) % NexusModsRestClient.ChunkSize);

    var buffer = new byte[bufferSize];
    var fileOffset = Convert.ToInt64(NexusModsRestClient.ChunkSize) * (checkUploadChunkExists.RequestModel.ResumableChunkNumber - 1);
    str.Seek(fileOffset, SeekOrigin.Begin);
    var byteCount = str.Read(buffer);

    str.Close();

    if (byteCount == 0) return null; // No bytes to upload

    Message<
      UploadFileChunkRequest,
      UploadFileChunkRequestModel,
      UploadFileChunkResponse,
      UploadFileChunkResponseModel
    > uploadFileChunk = await UploadFileChunk(modId
                                              , archiveFile
                                              , cookieNexusId
                                              , cookiesid_develop
                                              , fileName
                                              , version
                                              , game
                                              , disableDownloadWithManager
                                              , disableVersionUpdate
                                              , disableMainVortex
                                              , checkUploadChunkExists.RequestModel
                                              , buffer);

    return !uploadFileChunk.Response.IsSuccessful ? null : uploadFileChunk;
  }

  #endregion

  #region Tasks

  /// <summary>
  /// Add an assembled file to a mod.
  /// </summary>
  /// <param name="cookieNexusId"></param>
  /// <param name="modId"></param>
  /// <param name="version"></param>
  /// <param name="disableVersionUpdate"></param>
  /// <param name="disableMainVortex"></param>
  /// <param name="fileName"></param>
  /// <param name="archiveFile"></param>
  /// <param name="categoryName"></param>
  /// <param name="description"></param>
  /// <param name="disableDownloadWithManager"></param>
  /// <param name="disableRequirementsPopUp"></param>
  /// <param name="gameId"></param>
  /// <param name="fileUuid"></param>
  /// <param name="uploadedFileHash"></param>
  /// <param name="disableMainFileUpdate"></param>
  /// <param name="oldFileId"></param>
  /// <param name="newExisting"></param>
  /// <param name="removeOldVersion"></param>
  /// <returns></returns>
  [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Consistent Pattern")]
  private static async Task<Message<
    AddFileToModRequest
    , AddFileToModRequestModel
    , AddFileToModResponse
    , AddFileToModResponseModel
  >> AddFileToMod(string cookieNexusId
                  , string cookiesid_develop
                  , uint modId
                  , string version
                  , bool disableVersionUpdate
                  , bool disableMainVortex
                  , string fileName
                  , FileInfo archiveFile
                  , CategoryName categoryName
                  , string description
                  , bool disableDownloadWithManager
                  , bool disableRequirementsPopUp
                  , int gameId
                  , string fileUuid
                  , string uploadedFileHash
                  , bool disableMainFileUpdate
                  , int oldFileId
                  , bool newExisting
                  , bool removeOldVersion)
  {
    Console.WriteLine($"Adding uploaded file to mod {modId}".Pastel(ColorOptions.InfoColor));

    var message = new Message<AddFileToModRequest, AddFileToModRequestModel, AddFileToModResponse, AddFileToModResponseModel>();
    message.RequestModel = new AddFileToModRequestModel(cookieNexusId
                                                        , cookiesid_develop
                                                        , gameId
                                                        , fileName
                                                        , version
                                                        , !disableVersionUpdate
                                                        , categoryName
                                                        , description
                                                        , !disableMainVortex
                                                        , fileUuid
                                                        , archiveFile.Length
                                                        , modId
                                                        , modId
                                                        , "add"
                                                        , uploadedFileHash
                                                        , archiveFile.Name
                                                        , !disableRequirementsPopUp
                                                        , disableMainFileUpdate ? null : oldFileId == -1 ? null : Convert.ToUInt32(oldFileId)
                                                        , disableDownloadWithManager
                                                        , newExisting
                                                        , removeOldVersion
                                                       );

    message.Request = new AddFileToModRequest(message.RequestModel);
    Console.WriteLine($"Using file options: {message.RequestModel}".Pastel(ColorOptions.StatusColor));

    message.Response = CommandUtils.RestClient.ExecuteAsync(message.Request).Result;
    message.ResponseModel = message.Response.Data;

    if (message.Response.IsSuccessful && message.Response.IsFileAdded)
    {
      Console.WriteLine($"{message.RequestModel.OriginalFile} successfully uploaded and added to mod {message.RequestModel.ModID}!".Pastel(ColorOptions.SuccessColor));
      Console.WriteLine();
      Console.WriteLine($"Now go ask {"@Pickysaurus".Pastel(ColorOptions.WarningColor)} when a real API will be available! ;)".Pastel(ColorOptions.InfoColor));
    }
    else
    {
      message.ErrorResponseModel = NexusModsRestClient.GetErrorMessage(message.Response);
      if (message.ErrorResponseModel.IsSet)
      {
        Console.WriteLine($"Error: {message.ErrorResponseModel.ErrorMessage}".Pastel(ColorOptions.ErrorColor));
        if (message.ErrorResponseModel.ErrorException != null)
        {
          Console.WriteLine($"Exception: {message.ErrorResponseModel.ErrorException}");
        }
        throw new Exception(message.ErrorResponseModel.ErrorMessage, message.ErrorResponseModel.ErrorException);
      }
      else
      {
        var msg = AbstractResponseModel.FromJson<MessageResponseModel>(message.Response.Content.Trim());
        Console.WriteLine($"Error: {message.Response?.StatusDescription}: {msg?.Message}.".Pastel(ColorOptions.ErrorColor));
        throw new Exception(msg?.Message);
      }
    }

    return message;
  }

  /// <summary>
  /// Check if chunks have already been assembled.
  /// </summary>
  /// <param name="cookieNexusId"></param>
  /// <param name="cookiesid_develop"></param>
  /// <param name="uploadFileChunkResponseModel"></param>
  /// <returns></returns>
  [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Consistent Pattern")]
  private static async Task<Message<
    CheckFileStatusRequest, CheckFileStatusRequestModel,
    CheckFileStatusResponse, CheckFileStatusResponseModel
  >> CheckFileStatus(string cookieNexusId, string cookiesid_develop, UploadFileChunkResponseModel uploadFileChunkResponseModel)
  {
    Console.WriteLine($"Validating file upload for '{uploadFileChunkResponseModel.Uuid}'".Pastel(ColorOptions.InfoColor));

    var message = new Message<CheckFileStatusRequest, CheckFileStatusRequestModel, CheckFileStatusResponse, CheckFileStatusResponseModel>();
    message.RequestModel = new CheckFileStatusRequestModel(cookieNexusId, cookiesid_develop, uploadFileChunkResponseModel.Uuid, uploadFileChunkResponseModel.UploadFileHash);
    message.Request = new CheckFileStatusRequest(message.RequestModel);

    var attempt = 1;
    const int attempts = 8; // (2^8)/60 = 4.26 mins

    do
    {
      var delay = TimeSpan.FromSeconds(Math.Pow(2, attempt));
      Console.WriteLine($"Waiting {delay.Minutes}m {delay.Seconds}s, Attempt {attempt++} of {attempts}".Pastel(ColorOptions.StatusColor));
      await Task.Delay(delay);
      message.Response = await CommandUtils.RestClient.ExecuteAsync(message.Request);
      message.ResponseModel = message.Response.Data;

      if (message.Response.IsSuccessful) continue;
      Console.WriteLine($"Error validating file assembled for '{uploadFileChunkResponseModel.Uuid}'".Pastel(ColorOptions.ErrorColor));
    } while (!message.ResponseModel.FileChunksReassembled && attempt <= attempts);

    return message;
  }

  /// <summary>
  /// Check if a chunk has already been uploaded.
  /// </summary>
  /// <param name="archiveFile"></param>
  /// <param name="cookieNexusId"></param>
  /// <param name="resumableChunkNumber"></param>
  /// <param name="resumableCurrentChunkSize"></param>
  /// <param name="resumableTotalChunks"></param>
  /// <returns></returns>
  [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Consistent Pattern")]
  private static async Task<Message<
    UploadChunkExistsRequest, UploadChunkExistsRequestModel,
    UploadChunkExistsResponse, UploadChunkExistsChunkResponseModel
  >> CheckUploadChunkExists(FileInfo archiveFile
                            , string cookieNexusId
                            , string cookiesid_develop
                            , uint resumableChunkNumber
                            , uint resumableCurrentChunkSize
                            , uint resumableTotalChunks)
  {
    var message = new Message<
      UploadChunkExistsRequest, UploadChunkExistsRequestModel,
      UploadChunkExistsResponse, UploadChunkExistsChunkResponseModel
    >();

    message.RequestModel = new UploadChunkExistsRequestModel(cookieNexusId
                                                             , cookiesid_develop
                                                             , archiveFile.Name
                                                             , archiveFile.Length
                                                             , resumableChunkNumber
                                                             , resumableCurrentChunkSize
                                                             , resumableTotalChunks
                                                            );

    message.Request = new UploadChunkExistsRequest(message.RequestModel);

    var r = RandomUtil.GetNext();
    var cnt = r + (Convert.ToInt32(resumableChunkNumber) % 4 == 0 ? 1 : 300);
    Trace.WriteLine($"{resumableChunkNumber} : r={r} - {cnt}ms");
    Task.Delay(cnt).Wait();
    message.Response = await CommandUtils.RestClient.ExecuteAsync(message.Request);
    message.ResponseModel = message.Response.Data;
    return message;
  }

  /// <summary>
  /// Gets game meta data.
  /// </summary>
  /// <param name="key"></param>
  /// <param name="game"></param>
  /// <returns></returns>
  [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Consistent Pattern")]
  private static async Task<Message<
    GameInfoRequest, GameInfoRequestModel,
    GameInfoResponse, GameInfoResponseModel
  >> GameInfo(string key, string game)
  {
    Console.WriteLine($"Attempting to retrieve game details for '{game}'".Pastel(ColorOptions.InfoColor));
    var message = new Message<GameInfoRequest, GameInfoRequestModel, GameInfoResponse, GameInfoResponseModel>();
    message.RequestModel = new GameInfoRequestModel(key, game);
    message.Request = new GameInfoRequest(message.RequestModel);
    message.Response = await CommandUtils.RestClient.ExecuteAsync(message.Request);
    message.ResponseModel = message.Response.Data;

    if (message.Response.IsSuccessful)
    {
      Console.WriteLine($"Game details loaded: {message.ResponseModel.Name}/{message.ResponseModel.Id}".Pastel(ColorOptions.SuccessColor));
    }
    else
    {
      message.ErrorResponseModel = NexusModsRestClient.GetErrorMessage(message.Response);
      if (message.ErrorResponseModel.IsSet)
      {
        Console.WriteLine($"Error: {message.ErrorResponseModel.ErrorMessage}".Pastel(ColorOptions.ErrorColor));
        if (message.ErrorResponseModel.ErrorException != null)
        {
          Console.WriteLine($"Exception: {message.ErrorResponseModel.ErrorException}");
        }
      }
      else
      {
        var msg = AbstractResponseModel.FromJson<MessageResponseModel>(message.Response.Content);
        Console.WriteLine($"{message.Response.StatusDescription}: {msg.Message}.".Pastel(ColorOptions.ErrorColor));
      }
    }

    return message;
  }

  [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Not an error")]
  private static async Task<Message<
    ModFilesInfoRequest, ModFilesInfoRequestModel,
    ModFilesInfoResponse, ModFilesInfoResponseModel
  >> ModFilesInfo(string key, string game, uint modId)
  {
    Console.WriteLine($"Attempting to retrieve mod file details for mod '{modId}' of '{game}'".Pastel(ColorOptions.InfoColor));
    var message = new Message<ModFilesInfoRequest, ModFilesInfoRequestModel, ModFilesInfoResponse, ModFilesInfoResponseModel>();
    message.RequestModel = new ModFilesInfoRequestModel(key, game, modId);
    message.Request = new ModFilesInfoRequest(message.RequestModel);
    message.Response = await CommandUtils.RestClient.ExecuteAsync(message.Request);
    message.ResponseModel = message.Response.Data;

    if (message.Response.IsSuccessful)
    {
      Console.WriteLine($"Mod file details loaded: {message.RequestModel.ModId}".Pastel(ColorOptions.SuccessColor));
    }
    else
    {
      message.ErrorResponseModel = NexusModsRestClient.GetErrorMessage(message.Response);
      if (message.ErrorResponseModel.IsSet)
      {
        Console.WriteLine($"Error: {message.ErrorResponseModel.ErrorMessage}".Pastel(ColorOptions.ErrorColor));
        if (message.ErrorResponseModel.ErrorException != null)
        {
          Console.WriteLine($"Exception: {message.ErrorResponseModel.ErrorException}");
        }
      }
      else
      {
        var msg = AbstractResponseModel.FromJson<MessageResponseModel>(message.Response.Content);
        Console.WriteLine($"{message.Response.StatusDescription}: {msg.Message}.".Pastel(ColorOptions.ErrorColor));
      }
    }

    return message;
  }

  /// <summary>
  /// Upload a file chunk.
  /// </summary>
  /// <param name="modId"></param>
  /// <param name="archiveFile"></param>
  /// <param name="cookieNexusId"></param>
  /// <param name="fileName"></param>
  /// <param name="version"></param>
  /// <param name="game"></param>
  /// <param name="disableDownloadWithManager"></param>
  /// <param name="disableVersionUpdate"></param>
  /// <param name="disableMainVortex"></param>
  /// <param name="uploadFileMetaDataRequestModel"></param>
  /// <param name="buffer"></param>
  /// <returns></returns>
  [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Consistent Pattern")]
  private static async Task<Message<
    UploadFileChunkRequest, UploadFileChunkRequestModel,
    UploadFileChunkResponse, UploadFileChunkResponseModel
  >> UploadFileChunk(uint modId
                     , FileInfo archiveFile
                     , string cookieNexusId
                     , string cookiesid_develop
                     , string fileName
                     , string version
                     , string game
                     , bool disableDownloadWithManager
                     , bool disableVersionUpdate
                     , bool disableMainVortex
                     , UploadFileMetaDataRequestModel uploadFileMetaDataRequestModel
                     , byte[] buffer)
  {
    var message = new Message<UploadFileChunkRequest, UploadFileChunkRequestModel, UploadFileChunkResponse, UploadFileChunkResponseModel>();
    message.RequestModel = new UploadFileChunkRequestModel(cookieNexusId
                                                           , cookiesid_develop
                                                           , modId
                                                           , archiveFile
                                                           , fileName
                                                           , version
                                                           , game
                                                           , disableDownloadWithManager
                                                           , !disableVersionUpdate
                                                           , !disableMainVortex
                                                           , uploadFileMetaDataRequestModel.ResumableChunkNumber
                                                           , uploadFileMetaDataRequestModel.ResumableCurrentChunkSize
                                                           , uploadFileMetaDataRequestModel.ResumableTotalChunks
                                                           , buffer
                                                          );

    message.Request = new UploadFileChunkRequest(message.RequestModel);
    message.Response = await CommandUtils.RestClient.ExecuteAsync(message.Request);
    message.ResponseModel = message.Response.Data;

    if (message.Response.IsSuccessful)
    {
      Console.WriteLine($"Chunk {uploadFileMetaDataRequestModel.ResumableChunkNumber} of {uploadFileMetaDataRequestModel.ResumableTotalChunks} uploaded.".Pastel(ColorOptions.SuccessColor));
    }
    else
    {
      message.ErrorResponseModel = NexusModsRestClient.GetErrorMessage(message.Response);
      if (message.ErrorResponseModel.IsSet)
      {
        Console.WriteLine($"Error: {message.ErrorResponseModel.ErrorMessage}".Pastel(ColorOptions.ErrorColor));
        if (message.ErrorResponseModel.ErrorException != null)
        {
          Console.WriteLine($"Exception: {message.ErrorResponseModel.ErrorException}".Pastel(ColorOptions.ErrorColor));
        }
      }
      else
      {
        var msg = AbstractResponseModel.FromJson<MessageResponseModel>(message.Response.Content);
        Console.WriteLine($"{msg}".Pastel(ColorOptions.ErrorColor));
      }
    }

    return message;
  }

  #endregion

  #region Helper Methods

  /// <summary>
  /// Get total number of chunks a file will be split into.
  /// </summary>
  /// <param name="archiveFile"></param>
  /// <returns></returns>
  private static int GetChunkCount(FileInfo archiveFile)
  {
    var count = Convert.ToUInt64(archiveFile.Length) / NexusModsRestClient.ChunkSize;
    if (Convert.ToUInt64(archiveFile.Length) % NexusModsRestClient.ChunkSize != 0) count++;
    return count == 0 ? 1 : Convert.ToInt32(count);
  }

  private static void PrintLastUploadedFileInfo(ModFileInfoResponseModel lastUpload)
  {
    if (lastUpload == null)
    {
      Console.WriteLine($"Unable to locate file to replace.".Pastel(ColorOptions.ErrorColor));
      return;
    }

    Console.WriteLine($"Last file uploaded info".Pastel(ColorOptions.WarningColor));
    Console.WriteLine($"{lastUpload.Name}, ({lastUpload.ModVersion})".Pastel(ColorOptions.WarningColor));
    Console.WriteLine($"{lastUpload.FileName}, ({lastUpload.FileId})".Pastel(ColorOptions.WarningColor));
  }

  #endregion
}

internal class TaskInfo
{
  internal readonly Task Task;
  internal readonly int ChunkNumber;
  internal readonly string TaskName;

  public TaskInfo(Task task, int chunkNumber, string taskName)
  {
    Task = task;
    ChunkNumber = chunkNumber;
    TaskName = taskName;
  }

  #region Overrides of Object

  /// <inheritdoc />
  public override string ToString()
  {
    return $"{ChunkNumber}, {TaskName}, {Task.Status}";
  }

  #endregion
}
