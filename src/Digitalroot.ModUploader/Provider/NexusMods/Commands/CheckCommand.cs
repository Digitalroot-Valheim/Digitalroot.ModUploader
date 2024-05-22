using Digitalroot.ModUploader.Commands;
using Digitalroot.ModUploader.Enums;
using Digitalroot.ModUploader.Http;
using Digitalroot.ModUploader.Provider.NexusMods.Models;
using Digitalroot.ModUploader.Provider.NexusMods.Protocol;
using Digitalroot.ModUploader.Provider.NexusMods.Validators;
using Pastel;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

// ReSharper disable UseObjectOrCollectionInitializer

namespace Digitalroot.ModUploader.Provider.NexusMods.Commands;

internal static class CheckCommand
{
  internal static ICommand GetCheckCommand()
  {
    var command = new Command("check", "Check that an API Key and Cookie are valid.")
    {
      CommandHelper.GetOption(new[] { "--key", "-k" }, "Api Key, ENV: " + "NEXUSMOD_API_KEY".Pastel(ColorOptions.EmColor), CommandUtils.RestClient.GetDefaultConfigValue("NEXUSMOD_API_KEY"), optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
      , CommandHelper.GetOption(new[] { "--cookie_nexusmods_session", "-cnms" }, "Session Cookie, ENV: " + "COOKIE_NEXUSMOD_SESSION".Pastel(ColorOptions.EmColor), CommandUtils.RestClient.GetDefaultConfigValue("COOKIE_NEXUSMOD_SESSION"), optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
    };

    command.Handler = GetCommandHandler();
    command.AddValidator(ValidatorsFactory.Instance.GetValidator);

    return command;
  }

  private static ICommandHandler GetCommandHandler()
  {
    return CommandHandler.Create<string, string>(async (key, cnms) =>
                                                 {
                                                   var checkApiKeyMessage = await CheckApiKey(key);
                                                   Console.WriteLine(checkApiKeyMessage.Response.IsApiKeyValid ? "API key successfully validated!".Pastel(ColorOptions.SuccessColor) : "API key validation failed!".Pastel(ColorOptions.WarningColor));

                                                   var checkCookieMessage = await CheckCookie(cnms);
                                                   Console.WriteLine(checkCookieMessage.ResponseModel.IsCookieValid ? "Cookies successfully validated!".Pastel(ColorOptions.SuccessColor) : "Cookie validation failed!".Pastel(ColorOptions.WarningColor));

                                                   if (!(checkApiKeyMessage.Response.IsApiKeyValid && checkCookieMessage.ResponseModel.IsCookieValid))
                                                   {
                                                     Console.WriteLine("Validation failed! Failing on error".Pastel(ColorOptions.ErrorColor));
                                                     Environment.Exit(1);
                                                   }
                                                 });
  }

  [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Consistent Pattern")]
  private static async Task<Message<
    CheckApiKeyRequest, ApiKeyRequestModel,
    CheckApiKeyResponse, UserResponseModel
  >> CheckApiKey(string key)
  {
    var message = new Message<CheckApiKeyRequest, ApiKeyRequestModel, CheckApiKeyResponse, UserResponseModel>();
    message.RequestModel = new ApiKeyRequestModel(key);
    message.Request = new CheckApiKeyRequest(message.RequestModel);
    message.Response = await CommandUtils.RestClient.ExecuteAsync(message.Request);
    message.ResponseModel = message.Response.Data;
    return message;
  }

  [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Consistent Pattern")]
  private static async Task<Message<
    CheckCookieRequest, CookieRequestModel,
    CheckCookieResponse, CheckCookieResponseModel
  >> CheckCookie(string nexusmodsSession)
  {
    var message = new Message<CheckCookieRequest, CookieRequestModel, CheckCookieResponse, CheckCookieResponseModel>();
    message.RequestModel = new CookieRequestModel(nexusmodsSession);
    message.Request = new CheckCookieRequest(message.RequestModel);
    message.Response = await CommandUtils.RestClient.ExecuteAsync(message.Request);
    message.ResponseModel = new CheckCookieResponseModel(message.Response);
    return message;
  }
}
