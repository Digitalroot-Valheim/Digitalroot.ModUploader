using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

internal class GameInfoRequest : AbstractAuthorizedRequest
{
  public GameInfoRequest(GameInfoRequestModel gameInfoRequestModel)
    : base(gameInfoRequestModel, $"v1/games/{gameInfoRequestModel.Game}.json", Method.GET) { }
}
