﻿using Digitalroot.ModUploader.Models;
using Digitalroot.ModUploader.Provider.NexusMods.Protocol;

namespace Digitalroot.ModUploader.Provider.NexusMods.Models;

// ReSharper disable once ClassNeverInstantiated.Global
internal class CheckCookieResponseModel : AbstractResponseModel
{
  // ReSharper disable once MemberCanBePrivate.Global
  internal readonly bool IsCookieValid;

  public CheckCookieResponseModel(CheckCookieResponse checkCookieResponse)
  {
    IsCookieValid = checkCookieResponse.IsSuccessful && !checkCookieResponse.Content.Contains("You are not allowed to access this area!") && !checkCookieResponse.Content.Contains("Error</h3>");
  }
}
