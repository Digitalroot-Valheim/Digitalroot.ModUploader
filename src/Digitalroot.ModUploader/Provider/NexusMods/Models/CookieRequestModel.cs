using Digitalroot.ModUploader.Models;

namespace Digitalroot.ModUploader.Provider.NexusMods.Models;

internal class CookieRequestModel : AbstractRequestModel
{
  internal string NexusmodsSession { get; }

  internal CookieRequestModel(string nexusmodsSession)
  {
    NexusmodsSession = nexusmodsSession;
  }
}
