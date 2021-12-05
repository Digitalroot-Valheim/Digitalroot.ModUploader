#nullable enable
using System.CommandLine.Parsing;

namespace Digitalroot.OdinPlusModUploader.Interfaces;

public interface IArgumentValidatorsFactory
{
  string? GetValidator(ArgumentResult argumentResult);
}
