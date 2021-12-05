using Digitalroot.OdinPlusModUploader.Enums;
using Pastel;
using System;
using System.Reflection;

namespace Digitalroot.OdinPlusModUploader.Utils
{
  internal static class Credits
  {
    internal static void PrintCredits()
    {
      Console.WriteLine();
      Console.WriteLine($"Original author {"Alistair Chapman".Pastel(ColorOptions.WarningColor)} aka {"agc93".Pastel(ColorOptions.WarningColor)} ({"https://github.com/agc93".Pastel(ColorOptions.SuccessColor)}).".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"View original @ {"https://github.com/agc93/nexus-uploader".Pastel(ColorOptions.SuccessColor)}, MIT License.".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"Contributors:".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"\t{"focustense".Pastel(ColorOptions.WarningColor)} ({"https://github.com/focustense".Pastel(ColorOptions.SuccessColor)}).".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"\t{"Timothy Baldridge".Pastel(ColorOptions.WarningColor)} aka {"halgari".Pastel(ColorOptions.WarningColor)} ({"https://github.com/halgari".Pastel(ColorOptions.SuccessColor)}).".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"\t{"Digitalroot".Pastel(ColorOptions.WarningColor)} ({"https://github.com/Digitalroot".Pastel(ColorOptions.SuccessColor)}).".Pastel(ColorOptions.InfoColor));
      Console.WriteLine();
      Console.WriteLine($"OdinPlusModUploader created by {"Digitalroot".Pastel(ColorOptions.WarningColor)} of the {"Odin Plus Team.".Pastel(ColorOptions.WarningColor)}".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"Released under the {"MIT License".Pastel(ColorOptions.WarningColor)}.".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"Support Me @ {"https://www.buymeacoffee.com/digitalroot".Pastel(ColorOptions.SuccessColor)}.".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"Join us on Discord: {"https://discord.gg/mbkPcvu9ax".Pastel(ColorOptions.SuccessColor)}.".Pastel(ColorOptions.InfoColor));
    }

    internal static void PrintWelcome()
    {
      var appVersion = Assembly.GetExecutingAssembly().GetName().Version;
      Console.WriteLine($"Odin Plus Mod Uploader ({appVersion?.Major}.{appVersion?.Minor}.{appVersion?.Build})".Pastel(ColorOptions.InfoColor));
      Console.WriteLine($"By: {"Digitalroot".Pastel(ColorOptions.WarningColor)}".Pastel(ColorOptions.InfoColor));
      Console.WriteLine();
    }

    internal static void PrintBanner()
    {
      Console.WriteLine(@"   ___     _ _      ___ _           _____               ".Pastel(ColorOptions.StatusColor));
      Console.WriteLine(@"  / _ \ __| (_)_ _ | _ \ |_  _ ___ |_   _|__ __ _ _ __  ".Pastel(ColorOptions.StatusColor));
      Console.WriteLine(@" | (_) / _` | | ' \|  _/ | || (_-<   | |/ -_) _` | '  \ ".Pastel(ColorOptions.StatusColor));
      Console.WriteLine(@"  \___/\__,_|_|_||_|_| |_|\_,_/__/   |_|\___\__,_|_|_|_|".Pastel(ColorOptions.StatusColor));
      Console.WriteLine();
    }
  }
}
