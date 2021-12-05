using Digitalroot.OdinPlusModUploader.Models;
using RestSharp;
using System.Net;

namespace Digitalroot.OdinPlusModUploader.Protocol;

public abstract class AbstractResponse
{
  private readonly IRestResponse _restResponse;

  internal string Content => _restResponse.Content;
  internal bool IsSuccessful => _restResponse.IsSuccessful;
  internal System.Exception ErrorException => _restResponse.ErrorException;
  internal string ErrorMessage => _restResponse.ErrorMessage;
  internal string StatusDescription => _restResponse.StatusDescription;
  internal HttpStatusCode StatusCode => _restResponse.StatusCode;

  [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
  internal IRestResponse RawRestResponse => _restResponse;

  private protected AbstractResponse(IRestResponse response)
  {
    _restResponse = response;
  }
}

internal abstract class AbstractResponse<T> : AbstractResponse
  where T : AbstractResponseModel
{
  internal T Data { get; }

  /// <inheritdoc />
  protected AbstractResponse(IRestResponse<T> response)
    : base(response)
  {
    Data = response.Data;
  }
}
