using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Parsing;
using System.IO;
using System.Linq;

namespace Digitalroot.OdinPlusModUploader.Help
{
  internal class MaskingHelpBuilder : HelpBuilder
  {
    /// <inheritdoc />
    public MaskingHelpBuilder(LocalizationResources localizationResources, int maxWidth = Int32.MaxValue)
      : base(localizationResources, maxWidth) { }

    #region Overrides of HelpBuilder

    /// <inheritdoc />
    protected override void WriteOptions(ICommand command, TextWriter writer, ParseResult parseResult)
    {
      // base.WriteOptions(command, writer, parseResult);

      var options = GetOptionRows(command, parseResult).ToArray();

      if (options.Length > 0)
      {
        WriteHeading(LocalizationResources.HelpOptionsTitle(), null, writer);
        RenderAsColumns(writer, options);
        writer.WriteLine();
      }

    }

    /// <summary>
    /// Gets help rows for the specified command's options.
    /// </summary>
    /// <param name="command">The command to get argument help items for.</param>
    /// <param name="parseResult">A parse result providing context for help formatting.</param>
    private new IEnumerable<TwoColumnHelpRow> GetOptionRows(ICommand command, ParseResult parseResult)
    {
      foreach (var option in command.Options.Where(x => !x.IsHidden))
      {
        var twoColumnRow = GetTwoColumnRow(option, parseResult);
        
        if ((twoColumnRow.FirstColumnText.Contains("<key>") || twoColumnRow.FirstColumnText.Contains("<cookie>")) && !twoColumnRow.SecondColumnText.Equals(option.Description))
        {
          yield return new TwoColumnHelpRow(twoColumnRow.FirstColumnText, $"{option.Description} [default: ******* ]");
        }
        else
        {
          yield return twoColumnRow;
        }
      }
    }

    #endregion
  }
}
