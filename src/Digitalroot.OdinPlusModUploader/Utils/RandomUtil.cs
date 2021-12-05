using System;

namespace Digitalroot.OdinPlusModUploader.Utils
{
  internal static class RandomUtil
  {
    private static readonly Random _random = new();

    public static int GetNext() => _random.Next(100, 150);
  }
}
