using Digitalroot.OdinPlusModUploader.Models;
using Digitalroot.OdinPlusModUploader.Protocol;

namespace Digitalroot.OdinPlusModUploader.Http
{
  public sealed class Message<TRequest, TRequestModel, TResponse, TResponseModel>
    where TRequest : AbstractRequest
    where TRequestModel : AbstractRequestModel
    where TResponse : AbstractResponse
    where TResponseModel : AbstractResponseModel
  {
    public TRequest Request { get; set; }
    public TRequestModel RequestModel { get; set; }
    public TResponse Response { get; set; }
    public TResponseModel ResponseModel { get; set; }
    public ErrorResponseModel ErrorResponseModel { get; set; }
  }
}
