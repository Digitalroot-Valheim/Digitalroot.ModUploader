using Digitalroot.OdinPlusModUploader.Protocol;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

// ReSharper disable once ClassNeverInstantiated.Global
internal class GameInfoResponse : AbstractResponse<GameInfoResponseModel>
{
  /// <inheritdoc />
  public GameInfoResponse(RestResponse<GameInfoResponseModel> response)
    : base(response) { }
}
