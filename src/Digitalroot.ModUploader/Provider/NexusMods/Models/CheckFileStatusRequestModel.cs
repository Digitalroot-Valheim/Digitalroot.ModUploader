namespace Digitalroot.ModUploader.Provider.NexusMods.Models;

internal class CheckFileStatusRequestModel : CookieRequestModel
{
  // ReSharper disable once MemberCanBePrivate.Global
  // ReSharper disable once InconsistentNaming
  // ReSharper disable once UnusedAutoPropertyAccessor.Global
  internal string Uuid { get; }
  internal string FileHash { get; }

  internal CheckFileStatusRequestModel(string cookiesid_develop, string uuid, string fileHash)
    : base(cookiesid_develop)
  {
    Uuid = uuid;
    FileHash = fileHash;
  }
}
