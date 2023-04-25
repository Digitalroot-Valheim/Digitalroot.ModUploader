#nullable enable
using Digitalroot.ModUploader.Provider.NexusMods.Clients;
using Digitalroot.ModUploader.Provider.NexusMods.Commands;
using Digitalroot.ModUploader.Utils;
using Digitalroot.ModUploader.Validators;
using System;
using System.CommandLine.Parsing;
using System.IO;

namespace Digitalroot.ModUploader.Provider.NexusMods.Validators
{
  internal class ValidatorsFactory : AbstractValidatorsFactory<ValidatorsFactory>
  {
    #region Argument

    /// <inheritdoc />
    protected override void OnFileInfoValidation(ArgumentResult argumentResult)
    {
      base.OnFileInfoValidation(argumentResult);
      var fileInfo = argumentResult.GetValueOrDefault<FileInfo>();
      if (fileInfo == null) AddErrorMessage(argumentResult, $"Invalid file. fileInfo == null");
      if (string.IsNullOrEmpty(fileInfo?.Name)) AddErrorMessage(argumentResult, $"File name ({fileInfo?.Name}) is missing or invalid.");
      if (!(fileInfo?.Exists ?? false)) AddErrorMessage(argumentResult, $"File ({fileInfo?.Name}) not found at {fileInfo?.DirectoryName}.");
      if (Convert.ToUInt64(fileInfo?.Length) > NexusModsRestClient.MaxFileSize)
      {
        AddErrorMessage(argumentResult
                        , $"File '{fileInfo?.Name}' size of {FileSizeFormatter.FormatSize(fileInfo?.Length)} " +
                          $"is larger then {FileSizeFormatter.FormatSize(NexusModsRestClient.MaxFileSize)}. " +
                          $"\nLarger file sizes may be supported in a future release.");
      }
    }

    #endregion

    #region Command

    #endregion

    #region Option

    /// <inheritdoc />
    protected override void OnStringValidation(OptionResult optionResult)
    {
      if (optionResult == null) throw new ArgumentNullException(nameof(optionResult));

      switch (optionResult.Symbol.Name)
      {
        case "key":
          if ((optionResult.Tokens.Count != 1
               || string.IsNullOrEmpty(optionResult.Tokens[0].Value))
              && string.IsNullOrEmpty(CommandUtils.RestClient.GetDefaultConfigValue("NEXUSMOD_API_KEY")))
          {
            AddErrorMessage(optionResult, $"Error: --{optionResult.Symbol.Name} value is missing.");
          }

          break;

        case "cookie":
          if ((optionResult.Tokens.Count != 1
               || string.IsNullOrEmpty(optionResult.Tokens[0].Value))
              && string.IsNullOrEmpty(CommandUtils.RestClient.GetDefaultConfigValue("NEXUSMOD_COOKIE")))
          {
            AddErrorMessage(optionResult, $"Error: --{optionResult.Symbol.Name} value is missing.");
          }

          break;
      }
    }

    #endregion
  }
}
