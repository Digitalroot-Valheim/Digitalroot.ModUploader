namespace Digitalroot.ModUploader.Provider.NexusMods.Models;

internal class ModFilesInfoRequestModel : ApiKeyRequestModel
{
  internal string Game { get; }
  internal uint ModId { get; }

  /// <inheritdoc />
  internal ModFilesInfoRequestModel(string apiKey, string game, uint modId)
    : base(apiKey)
  {
    Game = game;
    ModId = modId;
  }
}
