using Digitalroot.ModUploader.Models;
using System;

namespace Digitalroot.ModUploader.Provider.NexusMods.Models;

internal class CookieRequestModel : AbstractRequestModel
{
  internal string CookieSidDevelop { get; }

  internal CookieRequestModel(string cookieSidDevelop)
  {
    CookieSidDevelop = cookieSidDevelop;
  }

  [Obsolete("Fix Me", true)]
  internal CookieRequestModel(string cookieNexusId, string cookieSidDevelop)
  {
  }
}
