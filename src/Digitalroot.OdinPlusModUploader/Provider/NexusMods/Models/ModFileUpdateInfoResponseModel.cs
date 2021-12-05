using Digitalroot.OdinPlusModUploader.Models;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;

internal class ModFileUpdateInfoResponseModel : AbstractResponseModel
{
  [JsonProperty(PropertyName = "old_file_id"), JsonPropertyName("old_file_id")]
  public ulong OldFileId { get; set; }

  [JsonProperty(PropertyName = "new_file_id"), JsonPropertyName("new_file_id")]
  public ulong NewFileId { get; set; }

  [JsonProperty(PropertyName = "old_file_name"), JsonPropertyName("old_file_name")]
  public string OldFileName { get; set; }

  [JsonProperty(PropertyName = "new_file_name"), JsonPropertyName("new_file_name")]
  public string NewFileName { get; set; }

  [JsonProperty(PropertyName = "uploaded_timestamp"), JsonPropertyName("uploaded_timestamp")]
  public ulong UploadedTimestamp { get; set; }

  [JsonProperty(PropertyName = "uploaded_time"), JsonPropertyName("uploaded_time")]
  public DateTime? UploadedTime { get; set; }
}
