using Newtonsoft.Json;

namespace Digitalroot.OdinPlusModUploader.Models;

public abstract class AbstractResponseModel
{
  public string ToJson() => JsonConvert.SerializeObject(this);
  public static T FromJson<T>(string json) where T : AbstractResponseModel => JsonConvert.DeserializeObject<T>(json);
}