#nullable enable
using Digitalroot.ModUploader.Common;
using Digitalroot.ModUploader.Interfaces;
using System;
using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.IO;

namespace Digitalroot.ModUploader.Validators
{
  public abstract class AbstractValidatorsFactory<TValidatorsFactory> : Singleton<TValidatorsFactory>, IArgumentValidatorsFactory, ICommandValidatorsFactory, IOptionValidatorsFactory, IValidatorsFactory
    where TValidatorsFactory : AbstractValidatorsFactory<TValidatorsFactory>, new()
  {
    private readonly Dictionary<Tuple<Type, string, string>, Action?> _typeFunctionMap = new();

    #region Argument

    public virtual string? GetValidator(ArgumentResult argumentResult) => GetValidator<ArgumentResult>(argumentResult);

    protected virtual void OnUInt64Validation(ArgumentResult argumentResult) { }

    protected virtual void OnInt64Validation(ArgumentResult argumentResult) { }

    protected virtual void OnUInt32Validation(ArgumentResult argumentResult) { }

    protected virtual void OnInt32Validation(ArgumentResult argumentResult) { }

    protected virtual void OnStringValidation(ArgumentResult argumentResult) { }

    protected virtual void OnDirectoryInfoValidation(ArgumentResult argumentResult) { }

    protected virtual void OnFileInfoValidation(ArgumentResult argumentResult)
    {
      if (argumentResult.Tokens.Count != 1)
      {
        AddErrorMessage(argumentResult, $"File value is missing");
      }

      if (string.IsNullOrEmpty(argumentResult.Tokens[0].Value))
      {
        AddErrorMessage(argumentResult, $"File value is missing");
      }
    }

    #endregion

    #region Option

    public virtual string? GetValidator(OptionResult optionResult) => GetValidator<OptionResult>(optionResult);

    protected virtual void OnUInt64Validation(OptionResult optionResult) { }

    protected virtual void OnInt64Validation(OptionResult optionResult) { }

    protected virtual void OnUInt32Validation(OptionResult optionResult) { }

    protected virtual void OnInt32Validation(OptionResult optionResult) { }

    protected virtual void OnStringValidation(OptionResult optionResult) { }

    protected virtual void OnDirectoryInfoValidation(OptionResult optionResult) { }

    protected virtual void OnFileInfoValidation(OptionResult optionResult) { }

    #endregion

    #region Command

    public virtual string? GetValidator(CommandResult commandResult) => GetValidator<CommandResult>(commandResult);

    protected virtual void OnStringValidation(CommandResult commandResult) { }

    #endregion

    #region Helpers

    private protected virtual void AddErrorMessage(SymbolResult symbolResult, string msg)
    {
      if (symbolResult.ErrorMessage != null && !string.IsNullOrEmpty(symbolResult.ErrorMessage)) symbolResult.ErrorMessage = "\n";
      symbolResult.ErrorMessage += msg;
    }

    public virtual string? GetValidator<T>(T symbolResult)
      where T : SymbolResult
    {
      SeedFunctionMap(symbolResult);
      var action = ResolveTypeToFunction(symbolResult);
      action?.Invoke();
      return symbolResult.ErrorMessage;
    }

    private protected virtual Action? ResolveTypeToFunction(SymbolResult symbolResult)
    {
      Tuple<Type, string, string>? key;
      switch (symbolResult)
      {
        case ArgumentResult argumentResult:
          key = new Tuple<Type, string, string>(argumentResult.Argument.ValueType, nameof(ArgumentResult), argumentResult.Argument.Name);
          break;

        case OptionResult optionResult:
          key = new Tuple<Type, string, string>(optionResult.Option.ValueType, nameof(OptionResult), optionResult.Option.Name);
          break;

        case CommandResult commandResult:
          key = new Tuple<Type, string, string>(typeof(string), nameof(CommandResult), commandResult.Command.Name);
          break;

        default:
          throw new ArgumentOutOfRangeException($"ResolveTypeToFunction: Unable to resolve: {symbolResult.GetType().Name}");
      }

      return _typeFunctionMap.ContainsKey(key) ? _typeFunctionMap[key] : null;
    }

    private protected virtual void SeedFunctionMap<T>(T symbolResult)
      where T : SymbolResult
    {
      switch (symbolResult)
      {
        case ArgumentResult argumentResult:
          _typeFunctionMap.TryAdd(new Tuple<Type, string, string>(typeof(int), nameof(ArgumentResult), argumentResult.Argument.Name), () => OnInt32Validation(argumentResult));
          _typeFunctionMap.TryAdd(new Tuple<Type, string, string>(typeof(long), nameof(ArgumentResult), argumentResult.Argument.Name), () => OnInt64Validation(argumentResult));
          _typeFunctionMap.TryAdd(new Tuple<Type, string, string>(typeof(uint), nameof(ArgumentResult), argumentResult.Argument.Name), () => OnUInt32Validation(argumentResult));
          _typeFunctionMap.TryAdd(new Tuple<Type, string, string>(typeof(ulong), nameof(ArgumentResult), argumentResult.Argument.Name), () => OnUInt64Validation(argumentResult));
          _typeFunctionMap.TryAdd(new Tuple<Type, string, string>(typeof(string), nameof(ArgumentResult), argumentResult.Argument.Name), () => OnStringValidation(argumentResult));
          _typeFunctionMap.TryAdd(new Tuple<Type, string, string>(typeof(FileInfo), nameof(ArgumentResult), argumentResult.Argument.Name), () => OnFileInfoValidation(argumentResult));
          _typeFunctionMap.TryAdd(new Tuple<Type, string, string>(typeof(DirectoryInfo), nameof(ArgumentResult), argumentResult.Argument.Name), () => OnDirectoryInfoValidation(argumentResult));
          break;

        case OptionResult optionResult:
          _typeFunctionMap.TryAdd(new Tuple<Type, string, string>(typeof(int), nameof(OptionResult), optionResult.Option.Name), () => OnInt32Validation(optionResult));
          _typeFunctionMap.TryAdd(new Tuple<Type, string, string>(typeof(long), nameof(OptionResult), optionResult.Option.Name), () => OnInt64Validation(optionResult));
          _typeFunctionMap.TryAdd(new Tuple<Type, string, string>(typeof(uint), nameof(OptionResult), optionResult.Option.Name), () => OnUInt32Validation(optionResult));
          _typeFunctionMap.TryAdd(new Tuple<Type, string, string>(typeof(ulong), nameof(OptionResult), optionResult.Option.Name), () => OnUInt64Validation(optionResult));
          _typeFunctionMap.TryAdd(new Tuple<Type, string, string>(typeof(string), nameof(OptionResult), optionResult.Option.Name), () => OnStringValidation(optionResult));
          _typeFunctionMap.TryAdd(new Tuple<Type, string, string>(typeof(FileInfo), nameof(OptionResult), optionResult.Option.Name), () => OnFileInfoValidation(optionResult));
          _typeFunctionMap.TryAdd(new Tuple<Type, string, string>(typeof(DirectoryInfo), nameof(OptionResult), optionResult.Option.Name), () => OnDirectoryInfoValidation(optionResult));
          break;

        case CommandResult commandResult:
          _typeFunctionMap.TryAdd(new Tuple<Type, string, string>(typeof(string), nameof(CommandResult), commandResult.Command.Name), () => OnStringValidation(commandResult));
          break;
      }
    }

    #endregion
  }
}
