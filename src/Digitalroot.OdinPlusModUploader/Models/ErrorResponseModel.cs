using RestSharp;
using System;

namespace Digitalroot.OdinPlusModUploader.Models;

public class ErrorResponseModel : AbstractResponseModel
{
  private readonly IRestResponse _restResponse;

  public string ErrorMessage => _restResponse.ErrorMessage;
  public Exception ErrorException => _restResponse.ErrorException;
  public bool IsSet => !string.IsNullOrEmpty(ErrorMessage) || ErrorException != null;

  public ErrorResponseModel(IRestResponse restResponse)
  {
    _restResponse = restResponse;
  }
}
