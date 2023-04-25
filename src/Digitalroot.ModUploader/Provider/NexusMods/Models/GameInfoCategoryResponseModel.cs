using Digitalroot.ModUploader.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Digitalroot.ModUploader.Provider.NexusMods.Models;

public class GameInfoCategoryResponseModel : AbstractResponseModel
{
  [JsonProperty(PropertyName = "category_id"), JsonPropertyName("category_id")]
  public int CategoryId { get; set; }

  [JsonProperty(PropertyName = "name"), JsonPropertyName("name")]
  public string Name { get; set; }

  [JsonProperty(PropertyName = "parent_category"), JsonPropertyName("parent_category")]
  public bool IsParentCategory { get; set; }
}
