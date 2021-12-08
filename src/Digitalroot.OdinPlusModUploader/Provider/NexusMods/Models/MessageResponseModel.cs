using Digitalroot.OdinPlusModUploader.Models;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;

internal class MessageResponseModel : AbstractResponseModel
{
  [JsonProperty(PropertyName = "message"), JsonPropertyName("message")]
  public string Message { get; set; }

  [JsonProperty(PropertyName = "status"), JsonPropertyName("status"), Newtonsoft.Json.JsonConverter(typeof(StatusConverter))]
  public int Status { get; set; }

  [JsonProperty(PropertyName = "error"), JsonPropertyName("error")]
  public string Error { get; set; }

  [JsonProperty(PropertyName = "redirect"), JsonPropertyName("redirect")]
  public string Redirect { get; set; }

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

public class StatusConverter : Newtonsoft.Json.JsonConverter
{
  public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  {
    //if (value is bool)
    //{
      
    //}
    writer.WriteValue(((bool)value) ? 1 : 0);
  }

  public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
  {
    if (reader.ValueType == typeof(bool))
    {
      return Convert.ToInt32(reader.Value);
    }
    return reader.Value;
  }

  public override bool CanConvert(Type objectType)
  {
    return objectType == typeof(bool);
  }
}