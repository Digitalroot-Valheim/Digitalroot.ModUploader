#nullable enable
using System.CommandLine.Parsing;

namespace Digitalroot.ModUploader.Interfaces;

public interface ICommandValidatorsFactory
{
  string? GetValidator(CommandResult commandResult);
}
