using Digitalroot.ModUploader.Protocol;
using Digitalroot.ModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.ModUploader.Provider.NexusMods.Protocol;

// ReSharper disable once ClassNeverInstantiated.Global
internal class GameInfoResponse : AbstractResponse<GameInfoResponseModel>
{
  /// <inheritdoc />
  public GameInfoResponse(RestResponse<GameInfoResponseModel> response)
    : base(response) { }
}
