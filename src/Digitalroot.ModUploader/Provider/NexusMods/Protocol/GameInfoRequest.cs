using Digitalroot.ModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.ModUploader.Provider.NexusMods.Protocol;

internal class GameInfoRequest : AbstractAuthorizedRequest
{
  public GameInfoRequest(GameInfoRequestModel gameInfoRequestModel)
    : base(gameInfoRequestModel, $"v1/games/{gameInfoRequestModel.Game}.json", Method.Get) { }
}
