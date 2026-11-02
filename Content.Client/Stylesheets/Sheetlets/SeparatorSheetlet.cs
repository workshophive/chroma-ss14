using Content.Client.UserInterface.Controls;
using Content.Client.Stylesheets.SheetletConfigs;
using Content.Client.Stylesheets.StylesheetDefinitions;
using Robust.Client.UserInterface;
using static Content.Client.Stylesheets.StylesheetHelpers;

namespace Content.Client.Stylesheets.Sheetlets;

[Sheetlet(typeof(CommonStylesheetDefinition))]
public sealed class SeparatorSheetlet<T> : ISheetlet<T>
    where T : IPaletteConfig
{
    public StyleRule[] GetRules(StylesheetDefinition sheet, T config)
    {
        return
        [
            E<Separator>().Class(StyleClass.LowDivider)
                .Prop(Separator.StylePropertyColor, config.SecondaryPalette.TextDark),
            E<Separator>().Class(StyleClass.HighDivider)
                .Prop(Separator.StylePropertyColor, config.HighlightPalette.Base),
            E<Separator>().Class(StyleClass.Positive)
                .Prop(Separator.StylePropertyColor, config.PositivePalette.Text),
            E<Separator>().Class(StyleClass.Highlight)
                .Prop(Separator.StylePropertyColor, config.HighlightPalette.Text),
            E<Separator>().Class(StyleClass.Negative)
                .Prop(Separator.StylePropertyColor, config.NegativePalette.Text),
        ];
    }
}
