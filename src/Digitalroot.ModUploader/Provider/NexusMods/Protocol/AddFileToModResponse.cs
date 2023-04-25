using Digitalroot.ModUploader.Protocol;
using Digitalroot.ModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.ModUploader.Provider.NexusMods.Protocol;

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
