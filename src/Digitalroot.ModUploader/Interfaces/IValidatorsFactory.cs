#nullable enable
using System.CommandLine.Parsing;

namespace Digitalroot.ModUploader.Interfaces;

public interface IValidatorsFactory
{
  string? GetValidator<T>(T symbolResult)
    where T : SymbolResult;
}
