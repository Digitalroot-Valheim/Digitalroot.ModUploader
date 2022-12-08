using Digitalroot.OdinPlusModUploader.Protocol;
using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

// ReSharper disable once ClassNeverInstantiated.Global
internal class AddFileToModResponse : AbstractResponse<AddFileToModResponseModel>
{
  // ReSharper disable once MemberCanBePrivate.Global
  internal readonly bool IsFileAdded;

  /// <inheritdoc />
  public AddFileToModResponse(RestResponse<AddFileToModResponseModel> response)
    : base(response)
  {
    IsFileAdded = response?.Data != null 
                  && !response.Data.Message.HasValue() 
                  && response.Data.Redirect.HasValue();
  }
}
