using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using System.IO;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Digitalroot.ModUploader.Serialization
{
  /// <summary>
  /// https://bytefish.de/blog/restsharp_custom_json_serializer/
  /// </summary>
  internal class NewtonsoftJsonSerializer : ISerializer, IDeserializer
  {
    private readonly JsonSerializer _serializer;

    public NewtonsoftJsonSerializer(JsonSerializer serializer)
    {
      _serializer = serializer;
    }

    public string ContentType
    {
      get => "application/json"; // Probably used for Serialization?
      set { }
    }

    public string DateFormat { get; set; }

    public string Namespace { get; set; }

    public string RootElement { get; set; }

    public string Serialize(object obj)
    {
      using var stringWriter = new StringWriter();
      using var jsonTextWriter = new JsonTextWriter(stringWriter);
      _serializer.Serialize(jsonTextWriter, obj);

      return stringWriter.ToString();
    }

    public T Deserialize<T>(RestResponse response)
    {
      var content = response.Content;

      using var stringReader = new StringReader(content);
      using var jsonTextReader = new JsonTextReader(stringReader);
      return _serializer.Deserialize<T>(jsonTextReader);
    }

    public static NewtonsoftJsonSerializer Default => new(
                                                          new JsonSerializer
                                                          {
                                                            NullValueHandling = NullValueHandling.Ignore
                                                            , MissingMemberHandling = MissingMemberHandling.Ignore
                                                            , Formatting = Formatting.Indented
                                                            , ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                          });

    public static JsonSerializerSettings DefaultSettings => new JsonSerializerSettings
                                                         {
                                                           NullValueHandling = NullValueHandling.Ignore
                                                           , MissingMemberHandling = MissingMemberHandling.Ignore
                                                           , Formatting = Formatting.Indented
                                                           , ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                         };
  }
}
