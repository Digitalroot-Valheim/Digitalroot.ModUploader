using Digitalroot.OdinPlusModUploader.Models;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;

internal class GameInfoResponseModel : AbstractResponseModel
{
  [JsonProperty(PropertyName = "approved_date"), JsonPropertyName("approved_date")]
  public long ApprovedDate { get; set; }

  [JsonProperty(PropertyName = "authors"), JsonPropertyName("authors")]
  public int Authors { get; set; }

  [JsonProperty(PropertyName = "domain_name"), JsonPropertyName("domain_name")]
  public string DomainName { get; set; }

  [JsonProperty(PropertyName = "downloads"), JsonPropertyName("downloads")]
  public long Downloads { get; set; }

  [JsonProperty(PropertyName = "file_count"), JsonPropertyName("file_count")]
  public long FileCount { get; set; }

  [JsonProperty(PropertyName = "file_endorsements"), JsonPropertyName("file_endorsements")]
  public long FileEndorsements { get; set; }

  [JsonProperty(PropertyName = "file_views"), JsonPropertyName("file_views")]
  public long FileViews { get; set; }

  [JsonProperty(PropertyName = "forum_url"), JsonPropertyName("forum_url")]
  public string ForumUrl { get; set; }

  [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
  public Uri ForumUri => new(NexusmodsUrl);

  [JsonProperty(PropertyName = "genre"), JsonPropertyName("genre")]
  public string Genre { get; set; }

  [JsonProperty(PropertyName = "id"), JsonPropertyName("id")]
  public int Id { get; set; }

  [JsonProperty(PropertyName = "mods"), JsonPropertyName("mods")]
  public int Mods { get; set; }

  [JsonProperty(PropertyName = "name"), JsonPropertyName("name")]
  public string Name { get; set; }

  [JsonProperty(PropertyName = "nexusmods_url"), JsonPropertyName("nexusmods_url")]
  public string NexusmodsUrl { get; set; }

  [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
  public Uri NexusmodsUri => new(NexusmodsUrl);

  [JsonProperty(PropertyName = "categories"), JsonPropertyName("categories")]
  public GameInfoCategoryResponseModel[] Categories { get; set; }
}
