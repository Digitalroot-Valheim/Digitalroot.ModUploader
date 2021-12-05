#nullable enable
using System.CommandLine.Parsing;

namespace Digitalroot.OdinPlusModUploader.Interfaces;

public interface IOptionValidatorsFactory
{
  string? GetValidator(OptionResult optionResult);
}
