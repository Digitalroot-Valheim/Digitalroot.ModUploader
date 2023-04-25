using Digitalroot.ModUploader.Models;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace Digitalroot.ModUploader.Provider.NexusMods.Models;

internal class ModFileInfoResponseModel : AbstractResponseModel
{
  [JsonProperty(PropertyName = "id"), JsonPropertyName("id")]
  public int[] Ids { get; set; }

  [JsonProperty(PropertyName = "uid"), JsonPropertyName("uid")]
  public ulong Uid { get; set; }

  [JsonProperty(PropertyName = "file_id"), JsonPropertyName("file_id")]
  public int FileId { get; set; }

  [JsonProperty(PropertyName = "name"), JsonPropertyName("name")]
  public string Name { get; set; }

  [JsonProperty(PropertyName = "category_id"), JsonPropertyName("category_id")]
  public int CategoryId { get; set; }

  [JsonProperty(PropertyName = "category_name"), JsonPropertyName("category_name")]
  public string CategoryName { get; set; }

  [JsonProperty(PropertyName = "is_primary"), JsonPropertyName("is_primary")]
  public bool IsPrimary { get; set; }

  [JsonProperty(PropertyName = "size"), JsonPropertyName("size")]
  public int Size { get; set; }

  [JsonProperty(PropertyName = "file_name"), JsonPropertyName("file_name")]
  public string FileName { get; set; }

  [JsonProperty(PropertyName = "uploaded_timestamp"), JsonPropertyName("uploaded_timestamp")]
  public ulong UploadedTimestamp { get; set; }

  [JsonProperty(PropertyName = "uploaded_time"), JsonPropertyName("uploaded_time")]
  public DateTime? UploadedTime { get; set; }

  [JsonProperty(PropertyName = "mod_version"), JsonPropertyName("mod_version")]
  public string ModVersion { get; set; }

  [JsonProperty(PropertyName = "external_virus_scan_url"), JsonPropertyName("external_virus_scan_url")]
  public string ExternalVirusScanUrl { get; set; }

  [JsonProperty(PropertyName = "description"), JsonPropertyName("description")]
  public string Description { get; set; }

  [JsonProperty(PropertyName = "size_kb"), JsonPropertyName("size_kb")]
  public ulong SizeInKiloBytes { get; set; }

  [JsonProperty(PropertyName = "size_in_bytes"), JsonPropertyName("size_in_bytes")]
  public ulong SizeInBytes { get; set; }

  [JsonProperty(PropertyName = "changelog_html"), JsonPropertyName("changelog_html")]
  public string ChangeLogHtml { get; set; }

  [JsonProperty(PropertyName = "content_preview_link"), JsonPropertyName("content_preview_link")]
  public string ContentPreviewLink { get; set; }
}
