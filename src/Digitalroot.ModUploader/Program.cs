using Digitalroot.ModUploader.Help;
using Digitalroot.ModUploader.Utils;
using System;
using System.CommandLine;
using System.CommandLine.Binding;
using System.CommandLine.Builder;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Digitalroot.ModUploader
{
  public static class Program
  {
    public static async Task<int> Main(string[] args)
    {
      Credits.PrintBanner();
      Credits.PrintWelcome();

      // Create a root command with some options
      var rootCommand = new RootCommand("Uploads mods to NexusMods")
      {
        // Provider.ModVault.Commands.GetRootCommand() ?? throw new InvalidOperationException()
        Provider.NexusMods.Commands.CommandFactory.GetCommand(Provider.NexusMods.Enums.CommandName.Root) as Command ?? throw new InvalidOperationException()
        // , Provider.Thunderstore.Commands.GetRootCommand() ?? throw new InvalidOperationException()
      };

      var cmdBuilder = new CommandLineBuilder(rootCommand);
      cmdBuilder.AddMiddleware(async (context, next) =>
                               {
                                 // Trace Output of Command passed. Warning this outputs -k and -c values.
                                 Trace.WriteLine(context.ParseResult);
                                 await next(context);
                               });

      cmdBuilder.UseDefaults();
      cmdBuilder.UseHelpBuilder(GetHelpBuilder);
      var parser = cmdBuilder.Build();

      if (args.Length == 0) // No commandline arguments given, output help text
      {
        var helpBuilder = GetHelpBuilder(Console.WindowWidth);
        helpBuilder.Write(rootCommand, Console.Out);
        return 0;
      }

      // AddHelpSubCommandsRecursively(rootCommand);

      // Parse the incoming args and invoke the handler
      var results = await parser.InvokeAsync(args);
      if (results == 0)
      {
        Credits.PrintCredits();
      }

      #if DEBUG
      Console.ReadKey();
      #endif

      return results;
    }

    private static IHelpBuilder GetHelpBuilder(BindingContext arg)
    {
      #if DEBUG
      return GetUnMaskingHelpBuilder(Console.WindowWidth);
      #else
      return GetMaskingHelpBuilder(Console.WindowWidth);
      #endif
    }

    private static IHelpBuilder GetHelpBuilder(int maxWidth)
    {
      #if DEBUG
      return GetUnMaskingHelpBuilder(maxWidth);
      #else
      return GetMaskingHelpBuilder(maxWidth);
      #endif
    }

    // ReSharper disable once UnusedMember.Local
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Not implemented yet")]
    private static MaskingHelpBuilder GetMaskingHelpBuilder(int maxWidth) => new(LocalizationResources.Instance, maxWidth);

    private static HelpBuilder GetUnMaskingHelpBuilder(int maxWidth) => new(LocalizationResources.Instance, maxWidth);

    private static void AddHelpSubCommandsRecursively(Command command)
    {
      foreach (var subCommand in command.OfType<Command>())
      {
        AddHelpSubCommandsRecursively(subCommand);
      }

      var helpCommand = new Command("help")
      {
        Handler = CommandHandler.Create(() => PrintHelp(command))
      };

      command.AddCommand(helpCommand);

      static void PrintHelp(ICommand c)
      {
        var helpBuilder = GetHelpBuilder(Console.WindowWidth);
        helpBuilder.Write(c, Console.Out);
      }
    }
  }
}
