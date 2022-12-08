using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

internal class CheckFileStatusRequest : AbstractAuthorizedRequest
{
  public CheckFileStatusRequest(CheckFileStatusRequestModel checkFileStatusRequestModel)
    : base(checkFileStatusRequestModel, $"uploads/check_status", Method.Get)
  {
    this.AddQueryParameter("id", checkFileStatusRequestModel.Uuid);
  }
}
