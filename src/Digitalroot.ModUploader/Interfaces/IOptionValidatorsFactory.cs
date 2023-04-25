#nullable enable
using System.CommandLine.Parsing;

namespace Digitalroot.ModUploader.Interfaces;

public interface IOptionValidatorsFactory
{
  string? GetValidator(OptionResult optionResult);
}
