﻿namespace Digitalroot.ModUploader.Provider.NexusMods
{
  internal static class Extensions
  {
    internal static bool HasValue(this string s)
    {
      return !string.IsNullOrWhiteSpace(s);
    }
  }
}
