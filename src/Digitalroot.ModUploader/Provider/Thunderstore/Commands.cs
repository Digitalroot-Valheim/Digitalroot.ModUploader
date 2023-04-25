using Pastel;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Drawing;

namespace Digitalroot.ModUploader.Provider.Thunderstore
{
  internal static class Commands
  {
    public static Command GetRootCommand()
    {
      var cmdThunderstore = new Command("thunderstore", "thunderstore.io commands.")
                            {
                              // Note that the parameters of the handler method are matched according to the names of the options
                              Handler = CommandHandler.Create(() => { Console.Error.WriteLine("Not yet implemented.".Pastel(Color.Red)); })
                            };

      cmdThunderstore.AddAlias("ts");

      return cmdThunderstore;
    }
  }
}
