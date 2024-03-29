using Digitalroot.ModUploader.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Digitalroot.ModUploader.Provider.NexusMods.Models;

internal class ModFilesInfoResponseModel : AbstractResponseModel
{
  [JsonProperty(PropertyName = "files"), JsonPropertyName("files")]
  public ModFileInfoResponseModel[] Files { get; set; }

  [JsonProperty(PropertyName = "file_updates"), JsonPropertyName("file_updates")]
  public ModFileUpdateInfoResponseModel[] FileUpdates { get; set; }
}
