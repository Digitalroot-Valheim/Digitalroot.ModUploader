using Digitalroot.OdinPlusModUploader.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;

internal class AddFileToModResponseModel : AbstractResponseModel
{
  [JsonProperty(PropertyName = "message"), JsonPropertyName("message")]
  public string Message { get; set; } // Empty on success

  [JsonProperty(PropertyName = "status"), JsonPropertyName("status")]
  public bool Status { get; set; } // True on success

  [JsonProperty(PropertyName = "redirect"), JsonPropertyName("redirect")]
  public string Redirect { get; set; } // not empty on success
}
