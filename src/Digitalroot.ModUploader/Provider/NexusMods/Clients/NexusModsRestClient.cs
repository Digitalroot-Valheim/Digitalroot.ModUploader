using Digitalroot.ModUploader.Clients;
using Digitalroot.ModUploader.Models;
using Digitalroot.ModUploader.Protocol;
using Digitalroot.ModUploader.Provider.NexusMods.Configuration;
using Digitalroot.ModUploader.Provider.NexusMods.Models;
using Digitalroot.ModUploader.Provider.NexusMods.Protocol;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System.Threading.Tasks;

namespace Digitalroot.ModUploader.Provider.NexusMods.Clients
{
  internal class NexusModsRestClient : AbstractRestClient
  {
    /// <summary>
    /// RestSharp Api Client
    /// </summary>
    private readonly RestClient _modHostApiProviderClient;

    /// <summary>
    /// RestSharp Upload Client
    /// </summary>
    private readonly RestClient _modHostUploadProviderClient;

    public const ulong ChunkSize = 1048576L * 5; // 5MB
    public const ulong MaxFileSize = 1073741824L * 20; // 20GB

    /// <inheritdoc />
    public NexusModsRestClient(NexusModsHostProviderConfiguration modsHostProviderConfiguration)
      : base(modsHostProviderConfiguration)
    {
      var apiOptions = new RestClientOptions(modsHostProviderConfiguration.ServiceApiUri);
      var jsonSerializerSettings = new JsonSerializerSettings
      {
        MissingMemberHandling = MissingMemberHandling.Ignore
        , NullValueHandling = NullValueHandling.Ignore
      };

      _modHostApiProviderClient = new RestClient(apiOptions, configureSerialization: s => s.UseNewtonsoftJson(jsonSerializerSettings));

      var uploadOptions = new RestClientOptions(modsHostProviderConfiguration.ServiceUploadUri);

      _modHostUploadProviderClient = new RestClient(uploadOptions, configureSerialization: s => s.UseNewtonsoftJson(jsonSerializerSettings));
    }

    public static ErrorResponseModel GetErrorMessage(AbstractResponse response)
    {
      return GetErrorMessage(response.RawRestResponse);
    }

    public async Task<CheckCookieResponse> ExecuteAsync(CheckCookieRequest request) => await GetAsync<CheckCookieResponse>(ModHostProviderClient, request);

    public async Task<CheckApiKeyResponse> ExecuteAsync(CheckApiKeyRequest request) => await GetAsync<CheckApiKeyResponse, UserResponseModel>(_modHostApiProviderClient, request);

    public async Task<GameInfoResponse> ExecuteAsync(GameInfoRequest request) => await GetAsync<GameInfoResponse, GameInfoResponseModel>(_modHostApiProviderClient, request);

    public async Task<UploadFileChunkResponse> ExecuteAsync(UploadFileChunkRequest chunkRequest) => await PostAsync<UploadFileChunkResponse, UploadFileChunkResponseModel>(_modHostUploadProviderClient, chunkRequest);

    public async Task<UploadChunkExistsResponse> ExecuteAsync(UploadChunkExistsRequest request) => await GetAsync<UploadChunkExistsResponse, UploadChunkExistsChunkResponseModel>(_modHostUploadProviderClient, request);

    public async Task<CheckFileStatusResponse> ExecuteAsync(CheckFileStatusRequest request) => await GetAsync<CheckFileStatusResponse, CheckFileStatusResponseModel>(_modHostUploadProviderClient, request);

    public async Task<AddFileToModResponse> ExecuteAsync(AddFileToModRequest request) => await PostAsync<AddFileToModResponse, AddFileToModResponseModel>(ModHostProviderClient, request);

    public async Task<ModFilesInfoResponse> ExecuteAsync(ModFilesInfoRequest request) => await GetAsync<ModFilesInfoResponse, ModFilesInfoResponseModel>(_modHostApiProviderClient, request);
  }
}
