using Newtonsoft.Json;

namespace Digitalroot.OdinPlusModUploader.Models;

public abstract class AbstractRequestModel
{
  public string ToJson() => JsonConvert.SerializeObject(this);
  public static T FromJson<T>(string json) where T : AbstractRequestModel => JsonConvert.DeserializeObject<T>(json);
}
