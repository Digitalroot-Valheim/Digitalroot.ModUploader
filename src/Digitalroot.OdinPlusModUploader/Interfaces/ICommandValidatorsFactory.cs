#nullable enable
using System.CommandLine.Parsing;

namespace Digitalroot.OdinPlusModUploader.Interfaces;

public interface ICommandValidatorsFactory
{
  string? GetValidator(CommandResult commandResult);
}
