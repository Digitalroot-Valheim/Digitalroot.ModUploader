using Digitalroot.ModUploader.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Digitalroot.ModUploader.Provider.NexusMods.Models;

internal class CheckFileStatusResponseModel : AbstractResponseModel
{
  [JsonProperty(PropertyName = "file_chunks_reassembled"), JsonPropertyName("file_chunks_reassembled")]
  public bool FileChunksReassembled { get; set; }

  [JsonProperty(PropertyName = "s3_upload_complete"), JsonPropertyName("s3_upload_complete")]
  public bool S3UploadComplete { get; set; }

  [JsonProperty(PropertyName = "virus_total_status"), JsonPropertyName("virus_total_status")]
  public int VirusTotalStatus { get; set; }
}
