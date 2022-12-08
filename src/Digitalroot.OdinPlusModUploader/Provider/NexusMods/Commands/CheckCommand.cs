using Digitalroot.OdinPlusModUploader.Commands;
using Digitalroot.OdinPlusModUploader.Enums;
using Digitalroot.OdinPlusModUploader.Http;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Validators;
using Pastel;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Threading.Tasks;

// ReSharper disable UseObjectOrCollectionInitializer

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Commands;

internal static class CheckCommand
{
  internal static ICommand GetCheckCommand()
  {
    var command = new Command("check", "Check that an API Key and Cookie are valid.")
    {
      CommandHelper.GetOption(new[] { "--key", "-k" }, "Api Key, ENV: " + "NEXUSMOD_API_KEY".Pastel(ColorOptions.EmColor), CommandUtils.RestClient.GetDefaultConfigValue("NEXUSMOD_API_KEY"), optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
      , CommandHelper.GetOption(new[] { "--cookie_nexusid", "-cnxid" }, "Session Cookie, ENV: " + "NEXUSMOD_COOKIE_NEXUSID".Pastel(ColorOptions.EmColor), CommandUtils.RestClient.GetDefaultConfigValue("NEXUSMOD_COOKIE_NEXUSID"), optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
      , CommandHelper.GetOption(new[] { "--cookie_sid_develop", "-csid" }, "Session Cookie, ENV: " + "NEXUSMOD_COOKIE_SID_DEVELOP".Pastel(ColorOptions.EmColor), CommandUtils.RestClient.GetDefaultConfigValue("NEXUSMOD_COOKIE_SID_DEVELOP"), optionValidatorsFactory: ValidatorsFactory.Instance) as Option ?? throw new InvalidOperationException()
    };

    command.Handler = GetCommandHandler();
    command.AddValidator(ValidatorsFactory.Instance.GetValidator);

    return command;
  }

  private static ICommandHandler GetCommandHandler()
  {
    return CommandHandler.Create<string, string, string>(async (key, cookie_nexusid, cookie_sid_develop) =>
                                                 {
                                                   var checkApiKeyMessage = await CheckApiKey(key);
                                                   Console.WriteLine(checkApiKeyMessage.Response.IsApiKeyValid ? "API key successfully validated!".Pastel(Color.Green) : "API key validation failed!".Pastel(Color.Orange));

                                                   var checkCookieMessage = await CheckCookie(cookie_nexusid, cookie_sid_develop);
                                                   Console.WriteLine(checkCookieMessage.ResponseModel.IsCookieValid ? "Cookies successfully validated!".Pastel(Color.Green) : "Cookie validation failed!".Pastel(Color.Orange));
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
  >> CheckCookie(string cookieNexusId, string cookieSidDevelop)
  {
    var message = new Message<CheckCookieRequest, CookieRequestModel, CheckCookieResponse, CheckCookieResponseModel>();
    message.RequestModel = new CookieRequestModel(cookieNexusId, cookieSidDevelop);
    message.Request = new CheckCookieRequest(message.RequestModel);
    message.Response = await CommandUtils.RestClient.ExecuteAsync(message.Request);
    message.ResponseModel = new CheckCookieResponseModel(message.Response);
    return message;
  }
}
