namespace Digitalroot.ModUploader.Provider.NexusMods.Models;

internal class GameInfoRequestModel : ApiKeyRequestModel
{
  internal string Game { get; }

  /// <inheritdoc />
  internal GameInfoRequestModel(string apiKey, string game)
    : base(apiKey)
  {
    Game = game;
  }
}
