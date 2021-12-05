#nullable enable
using System.CommandLine.Parsing;

namespace Digitalroot.OdinPlusModUploader.Interfaces;

public interface IValidatorsFactory
{
  string? GetValidator<T>(T symbolResult)
    where T : SymbolResult;
}
