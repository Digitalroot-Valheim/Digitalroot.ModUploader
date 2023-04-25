using Digitalroot.ModUploader.Clients;
using Digitalroot.ModUploader.Models;
using Digitalroot.ModUploader.Protocol;
using Digitalroot.ModUploader.Provider.NexusMods.Configuration;
using Digitalroot.ModUploader.Provider.NexusMods.Models;
using Digitalroot.ModUploader.Provider.NexusMods.Protocol;
using RestSharp;
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
      _modHostApiProviderClient = new RestClient(modsHostProviderConfiguration.ServiceApiUri);
      SetNewtonsoftJsonSerializerAsHandler(_modHostApiProviderClient);

      _modHostUploadProviderClient = new RestClient(modsHostProviderConfiguration.ServiceUploadUri);
      SetNewtonsoftJsonSerializerAsHandler(_modHostUploadProviderClient);
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
