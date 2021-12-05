using Digitalroot.OdinPlusModUploader.Protocol;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

// ReSharper disable once ClassNeverInstantiated.Global
internal class CheckApiKeyResponse : AbstractResponse<UserResponseModel>
{
  internal bool IsApiKeyValid => IsSuccessful;

  /// <inheritdoc />
  public CheckApiKeyResponse(IRestResponse<UserResponseModel> response)
    : base(response) { }
}
