using RestSharp;
using System;

namespace Digitalroot.ModUploader.Models;

public class ErrorResponseModel : AbstractResponseModel
{
  private readonly RestResponse _restResponse;

  public string ErrorMessage => _restResponse.ErrorMessage;
  public Exception ErrorException => _restResponse.ErrorException;
  public bool IsSet => !string.IsNullOrEmpty(ErrorMessage) || ErrorException != null;

  public ErrorResponseModel(RestResponse restResponse)
  {
    _restResponse = restResponse;
  }
}
