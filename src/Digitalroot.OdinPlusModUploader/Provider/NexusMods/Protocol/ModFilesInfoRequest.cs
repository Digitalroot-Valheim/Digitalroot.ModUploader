using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

internal class ModFilesInfoRequest : AbstractAuthorizedRequest
{
  public ModFilesInfoRequest(ModFilesInfoRequestModel modFilesInfoRequestModel)
    : base(modFilesInfoRequestModel, $"v1/games/{modFilesInfoRequestModel.Game}/mods/{modFilesInfoRequestModel.ModId}/files.json", Method.Get)
  {
  }
}
