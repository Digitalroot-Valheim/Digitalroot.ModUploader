using Digitalroot.ModUploader.Models;
using Digitalroot.ModUploader.Protocol;

namespace Digitalroot.ModUploader.Http
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
