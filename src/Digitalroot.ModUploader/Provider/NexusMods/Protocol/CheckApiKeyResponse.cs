using Digitalroot.ModUploader.Protocol;
using Digitalroot.ModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.ModUploader.Provider.NexusMods.Protocol;

// ReSharper disable once ClassNeverInstantiated.Global
internal class CheckApiKeyResponse : AbstractResponse<UserResponseModel>
{
  internal bool IsApiKeyValid => IsSuccessful;

  /// <inheritdoc />
  public CheckApiKeyResponse(RestResponse<UserResponseModel> response)
    : base(response) { }
}
