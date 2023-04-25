using Digitalroot.ModUploader.Enums;
using Pastel;
using System;
using System.Reflection;

namespace Digitalroot.ModUploader.Utils
{
  internal static class Credits
  {
    internal static void PrintCredits()
    {
      Console.WriteLine();
      Console.WriteLine($"Based on work by the original author {"Alistair Chapman".Pastel(ColorOptions.WarningColor)} aka {"agc93".Pastel(ColorOptions.WarningColor)} ({"https://github.com/agc93".Pastel(ColorOptions.SuccessColor)}).".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"View original @ {"https://github.com/agc93/nexus-uploader".Pastel(ColorOptions.SuccessColor)}, MIT License.".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"Contributors:".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"\t{"focustense".Pastel(ColorOptions.WarningColor)} ({"https://github.com/focustense".Pastel(ColorOptions.SuccessColor)}).".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"\t{"Timothy Baldridge".Pastel(ColorOptions.WarningColor)} aka {"halgari".Pastel(ColorOptions.WarningColor)} ({"https://github.com/halgari".Pastel(ColorOptions.SuccessColor)}).".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"\t{"Digitalroot".Pastel(ColorOptions.WarningColor)} ({"https://github.com/Digitalroot".Pastel(ColorOptions.SuccessColor)}).".Pastel(ColorOptions.InfoColor));
      Console.WriteLine();
      Console.WriteLine($"Mod Uploader created by {"Digitalroot".Pastel(ColorOptions.WarningColor)} of the {"Valhalla Legends Team.".Pastel(ColorOptions.WarningColor)}".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"Released under the {"MIT License".Pastel(ColorOptions.WarningColor)}.".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"Support Me @ {"https://www.buymeacoffee.com/digitalroot".Pastel(ColorOptions.SuccessColor)}.".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"Join us on Discord: {"https://discord.gg/SsMW3rm67u".Pastel(ColorOptions.SuccessColor)}.".Pastel(ColorOptions.InfoColor));
    }

    internal static void PrintWelcome()
    {
      var appVersion = Assembly.GetExecutingAssembly().GetName().Version;
      Console.WriteLine($"Mod Uploader ({appVersion?.Major}.{appVersion?.Minor}.{appVersion?.Build})".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"By: {"Digitalroot".Pastel(ColorOptions.WarningColor)}".Pastel(ColorOptions.InfoColor));
      Console.WriteLine();
    }

    internal static void PrintBanner()
    {
      Console.WriteLine(@"  ___  _      _ _        _              _   ".Pastel(ColorOptions.StatusColor));
      Console.WriteLine(@" |   \(_)__ _(_) |_ __ _| |_ _ ___  ___| |_ ".Pastel(ColorOptions.StatusColor));
      Console.WriteLine(@" | |) | / _` | |  _/ _` | | '_/ _ \/ _ \  _|".Pastel(ColorOptions.StatusColor));
      Console.WriteLine(@" |___/|_\__, |_|\__\__,_|_|_| \___/\___/\__|".Pastel(ColorOptions.StatusColor));
      Console.WriteLine(@"        |___/                               ".Pastel(ColorOptions.StatusColor));
      Console.WriteLine();
    }
  }
}
