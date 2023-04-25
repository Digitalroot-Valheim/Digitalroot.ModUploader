using Digitalroot.ModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.ModUploader.Provider.NexusMods.Protocol;

internal class ModFilesInfoRequest : AbstractAuthorizedRequest
{
  public ModFilesInfoRequest(ModFilesInfoRequestModel modFilesInfoRequestModel)
    : base(modFilesInfoRequestModel, $"v1/games/{modFilesInfoRequestModel.Game}/mods/{modFilesInfoRequestModel.ModId}/files.json", Method.Get)
  {
  }
}
