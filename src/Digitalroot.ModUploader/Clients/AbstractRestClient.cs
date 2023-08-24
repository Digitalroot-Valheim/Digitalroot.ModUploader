using Digitalroot.ModUploader.Configuration;
using Digitalroot.ModUploader.Models;
using Digitalroot.ModUploader.Protocol;
using Digitalroot.ModUploader.Provider.NexusMods;
using Digitalroot.ModUploader.Provider.NexusMods.Models;
using Digitalroot.ModUploader.Provider.NexusMods.Protocol;
using Digitalroot.ModUploader.Serialization;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Digitalroot.ModUploader.Clients
{
  public class AbstractRestClient
  {
    /// <summary>
    /// RestSharp Clients
    /// </summary>
    private protected readonly RestClient ModHostProviderClient;

    /// <summary>
    /// Address Validation Client Configuration
    /// </summary>
    private readonly AbstractHostProviderConfiguration _configuration;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="modsHostProviderConfiguration"></param>
    private protected AbstractRestClient(AbstractHostProviderConfiguration modsHostProviderConfiguration)
    {
      _configuration = modsHostProviderConfiguration;
      ModHostProviderClient = new RestClient(_configuration.ServiceUri);
      SetNewtonsoftJsonSerializerAsHandler(ModHostProviderClient);
    }

    #region Helper Methods

    internal string GetDefaultConfigValue(string value) => _configuration.GetDefaultConfigValue(value);

    /// <summary>
    /// Configures the client to use NewtonsoftJsonSerializer so that
    /// The JsonProperty Attribute can be used.
    /// e.g. [JsonProperty(PropertyName = "country")]
    /// </summary>
    /// <param name="restClient"></param>
    private protected static void SetNewtonsoftJsonSerializerAsHandler(RestClient restClient)
    {
      restClient.UseNewtonsoftJson(new JsonSerializerSettings
      {
        MissingMemberHandling = MissingMemberHandling.Ignore
        , NullValueHandling = NullValueHandling.Ignore
      });
    }

    internal static ErrorResponseModel GetErrorMessage(RestResponse response)
    {
      return new ErrorResponseModel(response);
    }

    [Conditional("TRACE")]
    private static void TraceRequest(RestClient restClient, RestRequest request)
    {
      Trace.WriteLine($"** Request ** - {request.Method}: {restClient.Options.BaseUrl}{request.Resource}");
      Trace.WriteLine("[Payload:]");
      Trace.WriteLine(NewtonsoftJsonSerializer.Default.Serialize(request.Parameters));
      Trace.WriteLine(string.Empty);
    }

    [Conditional("TRACE")]
    private static void TraceResponse(RestResponse response)
    {
      Trace.WriteLine($"** Response ** - IsSuccessful: {response.IsSuccessful}, StatusCode: {response.StatusCode}");
      Trace.WriteLine("[Headers:]");
      Trace.WriteLine(NewtonsoftJsonSerializer.Default.Serialize(response.Headers));
      Trace.WriteLine(string.Empty);

      switch (response.ContentType)
      {
        case "application/json; charset=utf-8":
          // Print nothing. JSON data is printed via TraceResponse<TRestResponseModel>(RestResponse response, TRestResponseModel data)
          break;

        case "text/html; charset=utf-8":
          Trace.WriteLine("[Content]:");
          Trace.WriteLine(response.Content);
          break;

        default:
          Trace.WriteLine("[Content]:");
          Trace.WriteLine(NewtonsoftJsonSerializer.Default.Serialize(response.Content));
          break;
      }

      if (response.ErrorMessage.HasValue())
      {
        Trace.WriteLine(string.Empty);
        Trace.WriteLine("[Error]:");
        Trace.WriteLine(response.ErrorMessage);
      }

      if (response.ErrorException == null) return;

      Trace.WriteLine(string.Empty);
      Trace.WriteLine("[ErrorException]:");
      Trace.WriteLine(response.ErrorException.Message);
      Trace.WriteLine(string.Empty);
      Trace.WriteLine("[StackTrace]");
      Trace.WriteLine(response.ErrorException.StackTrace);
    }

    [Conditional("TRACE")]
    private static void TraceResponse<TRestResponseModel>(RestResponse response, TRestResponseModel data)
    {
      TraceResponse(response);
      if (data == null) return;
      Trace.WriteLine(string.Empty);
      Trace.WriteLine("[Content]:");
      Trace.WriteLine(NewtonsoftJsonSerializer.Default.Serialize(data));
      Trace.WriteLine(string.Empty);
    }

    #endregion

    #region Obsolete

    [Obsolete("Use Async Version")]
    protected static TRestResponse Get<TRestResponse>(RestClient restClient, RestRequest request)
    {
      var response = restClient.Get(request);
      return (TRestResponse)Activator.CreateInstance(typeof(TRestResponse), response);
    }

    [Obsolete("Use Async Version")]
    protected static TRestResponse Get<TRestResponse, TRestResponseModel>(RestClient restClient, RestRequest request)
      where TRestResponse : AbstractResponse
    {
      var response = restClient.Get<TRestResponseModel>(request);
      return (TRestResponse)Activator.CreateInstance(typeof(TRestResponse), response);
    }

    [Obsolete("This might be restored")]
    protected static async Task<TRestResponse> PostAsync<TRestResponse>(RestClient restClient, RestRequest request)
    {
      var response = await restClient.ExecutePostAsync(request);
      return (TRestResponse)Activator.CreateInstance(typeof(TRestResponse), response);
    }

    #endregion

    #region GetAsync

    //AbstractAuthorizedRequest

    protected static async Task<TRestResponse> GetAsync<TRestResponse>(RestClient restClient, RestRequest request)
      where TRestResponse : AbstractResponse
    {
      TraceRequest(restClient, request);

      if (request is AbstractAuthorizedRequest { Model: CookieRequestModel model })
      {
        restClient.AddCookie("sid_develop", model.CookieSidDevelop, "/", ".nexusmods.com");  
      }

      var response = await restClient.ExecuteGetAsync(request);
      TraceResponse(response);
      return (TRestResponse)Activator.CreateInstance(typeof(TRestResponse), response);
    }

    protected static async Task<TRestResponse> GetAsync<TRestResponse, TRestResponseModel>(RestClient restClient, RestRequest request)
      where TRestResponse : AbstractResponse
    {
      TraceRequest(restClient, request);

      if (request is AbstractAuthorizedRequest { Model: CookieRequestModel model })
      {
        restClient.AddCookie("sid_develop", model.CookieSidDevelop, "/", ".nexusmods.com");  
      }

      var response = await restClient.ExecuteGetAsync<TRestResponseModel>(request);
      TraceResponse(response, response.Data);

      return (TRestResponse)Activator.CreateInstance(typeof(TRestResponse), response);
    }

    #endregion

    #region PostAsync

    protected static async Task<TRestResponse> PostAsync<TRestResponse, TRestResponseModel>(RestClient restClient, RestRequest request)
      where TRestResponse : AbstractResponse
    {
      TraceRequest(restClient, request);

      if (request is AbstractAuthorizedRequest { Model: CookieRequestModel model })
      {
        restClient.AddCookie("sid_develop", model.CookieSidDevelop, "/", ".nexusmods.com");  
      }

      var response = await restClient.ExecutePostAsync<TRestResponseModel>(request);
      TraceResponse(response, response.Data);
      return (TRestResponse)Activator.CreateInstance(typeof(TRestResponse), response);
    }

    #endregion
  }
}
