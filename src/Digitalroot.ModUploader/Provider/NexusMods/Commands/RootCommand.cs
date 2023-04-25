using Digitalroot.ModUploader.Provider.NexusMods.Enums;
using Digitalroot.ModUploader.Provider.NexusMods.Validators;
using System;
using System.CommandLine;

namespace Digitalroot.ModUploader.Provider.NexusMods.Commands
{
  internal static class RootCommand
  {
    internal static ICommand GetRootCommand()
    {
      var cmdNexusMods = new Command("nexusmods", "nexusmods.com commands.");
      cmdNexusMods.AddAlias("nx");
      cmdNexusMods.AddCommand(CommandFactory.GetCommand(CommandName.Check) as Command ?? throw new InvalidOperationException());
      cmdNexusMods.AddCommand(CommandFactory.GetCommand(CommandName.Upload) as Command ?? throw new InvalidOperationException());
      cmdNexusMods.AddValidator(ValidatorsFactory.Instance.GetValidator);
      return cmdNexusMods;
    }
  }
}
