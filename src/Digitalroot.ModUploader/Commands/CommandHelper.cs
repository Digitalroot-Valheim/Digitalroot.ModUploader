using Digitalroot.ModUploader.Interfaces;
using Digitalroot.ModUploader.Validators;
using System.CommandLine;

namespace Digitalroot.ModUploader.Commands
{
  internal static class CommandHelper
  {
    #region Argument

    internal static IArgument GetArgument<T>(string name
                                             , string description
                                             , T defaultValue
                                             , IArgumentValidatorsFactory argumentValidatorsFactory = null)
    {
      var argument = new Argument(name)
      {
        Description = description
        , ValueType = typeof(T)
      };

      if (defaultValue != null) argument.SetDefaultValue(defaultValue);
      if (argumentValidatorsFactory == null) argument.AddValidator(DefaultValidatorsFactory.Instance.GetValidator); // Use the default
      if (argumentValidatorsFactory != null) argument.AddValidator(argumentValidatorsFactory.GetValidator);

      return argument;
    }

    #endregion

    #region Options

    internal static IOption GetOption<T>(string[] alias
                                         , string description
                                         , T defaultValue
                                         , bool isRequired = false
                                         , bool isHidden = false
                                         , IArgumentArity argumentArity = null
                                         , IOptionValidatorsFactory optionValidatorsFactory = null)
    {
      var option = new Option<T>(alias, description)
      {
        Arity = argumentArity ?? (isRequired ? ArgumentArity.ExactlyOne : ArgumentArity.ZeroOrOne)
        , IsRequired = isRequired
        , IsHidden = isHidden
      };

      if (defaultValue != null) option.SetDefaultValue(defaultValue);

      if (optionValidatorsFactory == null) option.AddValidator(DefaultValidatorsFactory.Instance.GetValidator); // Use the default
      if (optionValidatorsFactory != null) option.AddValidator(optionValidatorsFactory.GetValidator);
      return option;
    }

    #endregion
  }
}
