using System;

namespace Digitalroot.ModUploader.Utils
{
  internal static class FileSizeFormatter
  {
    // Load all suffixes in an array  
    private static readonly string[] Suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };

    public static string FormatSize(ulong? bytes) => bytes.HasValue ? Format(Convert.ToDecimal(bytes.Value)) : null;

    public static string FormatSize(ulong bytes) => Format(Convert.ToDecimal(bytes));

    public static string FormatSize(long? bytes) => bytes.HasValue ? Format(Convert.ToDecimal(bytes.Value)) : null;

    public static string FormatSize(long bytes) => Format(Convert.ToDecimal(bytes));

    private static string Format(decimal number)
    {
      var counter = 0;
      while (Math.Round(number / 1024) >= 1)
      {
        number /= 1024;
        counter++;
      }

      return $"{number:n1}{Suffixes[counter]}";
    }
  }
}
