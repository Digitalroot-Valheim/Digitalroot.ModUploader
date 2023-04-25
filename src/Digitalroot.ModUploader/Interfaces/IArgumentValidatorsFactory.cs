#nullable enable
using System.CommandLine.Parsing;

namespace Digitalroot.ModUploader.Interfaces;

public interface IArgumentValidatorsFactory
{
  string? GetValidator(ArgumentResult argumentResult);
}
