using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Enums;
using System;
using System.CommandLine;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Commands;

internal static class CommandFactory
{
  internal static ICommand GetCommand(CommandName command)
  {
    return command switch
    {
      CommandName.Root => RootCommand.GetRootCommand()
      , CommandName.Check => CheckCommand.GetCheckCommand()
      , CommandName.Upload => UploadCommand.GetUploadCommand()
      , _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
    };
  }
}
