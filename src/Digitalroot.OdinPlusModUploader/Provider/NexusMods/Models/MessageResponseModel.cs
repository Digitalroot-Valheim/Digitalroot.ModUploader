using Digitalroot.OdinPlusModUploader.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;

internal class MessageResponseModel : AbstractResponseModel
{
  [JsonProperty(PropertyName = "message"), JsonPropertyName("message")]
  public string Message { get; set; }

  [JsonProperty(PropertyName = "status"), JsonPropertyName("status")]
  public int Status { get; set; }

  [JsonProperty(PropertyName = "error"), JsonPropertyName("error")]
  public string Error { get; set; }

  #region Overrides of Object

  /// <inheritdoc />
  public override string ToString()
  {
    StringBuilder sb = new();
    if (Status != 0) sb.Append(Status).Append(": ");
    if (!string.IsNullOrEmpty(Error)) sb.Append(Error).Append(' ');
    if (!string.IsNullOrEmpty(Message)) sb.Append(Message);

    return sb.ToString();
  }

  #endregion
}