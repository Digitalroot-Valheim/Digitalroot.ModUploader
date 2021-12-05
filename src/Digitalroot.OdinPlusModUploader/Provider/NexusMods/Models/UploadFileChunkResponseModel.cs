using Digitalroot.OdinPlusModUploader.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;

public class UploadFileChunkResponseModel : AbstractResponseModel
{
  [JsonProperty(PropertyName = "filename"), JsonPropertyName("filename")]
  public string UploadFileHash { get; set; }

  [JsonProperty(PropertyName = "uuid"), JsonPropertyName("uuid")]
  public string Uuid { get; set; }

  [JsonProperty(PropertyName = "status"), JsonPropertyName("status")]
  public bool Status { get; set; }
}
