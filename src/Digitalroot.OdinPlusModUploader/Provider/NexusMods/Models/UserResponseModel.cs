using Digitalroot.OdinPlusModUploader.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;

internal class UserResponseModel : AbstractResponseModel
{
  [JsonProperty(PropertyName = "key"), JsonPropertyName("key")]
  public string ApiKey { get; set; }

  [JsonProperty(PropertyName = "user_id"), JsonPropertyName("user_id")]
  public int UserId { get; set; }

  [JsonProperty(PropertyName = "name"), JsonPropertyName("name")]
  public string UserName { get; set; }

  [JsonProperty(PropertyName = "email"), JsonPropertyName("email")]
  public string EmailAddress { get; set; }

  [JsonProperty(PropertyName = "profile_url"), JsonPropertyName("profile_url")]
  public string ProfileUrl { get; set; }

  [JsonProperty(PropertyName = "is_premium"), JsonPropertyName("is_premium")]
  public bool IsPremium { get; set; }

  [JsonProperty(PropertyName = "is_supporter"), JsonPropertyName("is_supporter")]
  public bool IsSupporter { get; set; }

  [JsonProperty(PropertyName = "is_premium?"), JsonPropertyName("is_premium?")]
  public bool IsPremiumQ { get; set; }

  [JsonProperty(PropertyName = "is_supporter?"), JsonPropertyName("is_supporter?")]
  public bool IsSupporterQ { get; set; }
}
