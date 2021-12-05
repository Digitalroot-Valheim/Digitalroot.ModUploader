using Pastel;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Drawing;

namespace Digitalroot.OdinPlusModUploader.Provider.ModVault
{
  internal static class Commands
  {
    public static Command GetRootCommand()
    {
      var cmdModVault = new Command("modvault", "modvault.xyz commands.")
                        {
                          // Note that the parameters of the handler method are matched according to the names of the options
                          Handler = CommandHandler.Create(() => { Console.Error.WriteLine("Not yet implemented.".Pastel(Color.Red)); })
                        };

      cmdModVault.AddAlias("mv");

      return cmdModVault;
    }
  }
}
