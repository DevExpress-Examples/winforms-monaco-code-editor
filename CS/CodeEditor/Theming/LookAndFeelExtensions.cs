using DevExpress.LookAndFeel;
using DevExpress.Skins;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.Utils.Frames;

namespace CodeEditor.Theming {
    public static class LookAndFeelExtensions {
        public static MonacoThemeBase? SkinBaseOverride { get; set; }

        public static MonacoTheme CreateMonacoTheme(this UserLookAndFeel lookAndFeel, IList<MonacoThemeRule> rules, bool useSkinColors) {
            MonacoTheme result = new MonacoTheme {
                Name = $"{lookAndFeel.ActiveSkinName.ToLowerInvariant().Replace(" ", "-")}",
                Base = GetSkinBase(lookAndFeel),
                Colors = useSkinColors ? CreateMonacoColors(lookAndFeel) : null,
                Rules = useSkinColors ? CreateDevExpressTokenRules(lookAndFeel, rules) : rules,
                Inherit = !useSkinColors
            };
            return result;
        }

        static MonacoThemeBase GetSkinBase(UserLookAndFeel lookAndFeel) {
            if(SkinBaseOverride.HasValue)
                return SkinBaseOverride.Value;
            if(lookAndFeel.ActiveSkinName == SkinStyle.HighContrast || lookAndFeel.ActiveSkinName == SkinStyle.HighContrastClassic) {
                return FrameHelper.IsDarkSkin(lookAndFeel) ? MonacoThemeBase.HighContrast : MonacoThemeBase.HighContrastLight;
            }
            return FrameHelper.IsDarkSkin(lookAndFeel) ? MonacoThemeBase.Dark : MonacoThemeBase.Light;
        }

        static Dictionary<string, Color> CreateMonacoColors(UserLookAndFeel lookAndFeel) {
            var result = new Dictionary<string, Color>();
            Skin skin = CommonSkins.GetSkin(lookAndFeel);
            if(skin == null)
                return result;

            Skin reportSkin = ReportsSkins.GetSkin(lookAndFeel);
            SkinElement reportElem = reportSkin[ReportsSkins.SkinScriptControl];

            // === Base colors ===
            result[MonacoColorKeys.EditorBackground] = skin.Colors.GetColor("Window");
            result[MonacoColorKeys.EditorForeground] = skin.Colors.GetColor("WindowText");

            // === Line numbers ===
            result[MonacoColorKeys.LineNumberForeground] = reportElem.Properties.GetColor("LineNumbersColor");
            result[MonacoColorKeys.LineNumberActiveForeground] = skin.Colors.GetColor("HighlightAlternate");

            // === Cursor ===
            result[MonacoColorKeys.CursorForeground] = skin.Colors.GetColor("WindowText");

            // === Selection ===
            SkinElement selectionElem = skin[CommonSkins.SkinSelection];
            int opacity = selectionElem.Properties.GetInteger(CommonSkins.SkinSelectionOpacity, 40);
            Color color = Color.FromArgb(opacity, selectionElem.Color.GetBackColor());
            result[MonacoColorKeys.SelectionBackground] = color;
            result[MonacoColorKeys.InactiveSelectionBackground] = color;

            // === Current line ===
            result[MonacoColorKeys.LineHighlightBackground] = skin.Colors.GetColor("Control");

            // === Gutter ===
            result[MonacoColorKeys.GutterBackground] = skin.Colors.GetColor("Window");

            // === Indent guides ===
            result[MonacoColorKeys.IndentGuideBackground] = skin.Colors.GetColor("DisabledText");
            result[MonacoColorKeys.IndentGuideActiveBackground] = reportElem.Properties.GetColor("LineNumbersColor");

            // === Scrollbar ===
            result[MonacoColorKeys.ScrollbarShadow] = ColorTranslator.FromHtml("#a0a0a0");
            result[MonacoColorKeys.ScrollbarSliderBackground] = skin.Colors.GetColor("DisabledText");
            result[MonacoColorKeys.ScrollbarSliderHoverBackground] = skin.Colors.GetColor("WindowText");
            result[MonacoColorKeys.ScrollbarSliderActiveBackground] = skin.Colors.GetColor("WindowText");

            // === Brackets ===
            Color bracketColor = reportElem.Properties.GetColor("BracketHighlightColor");
            int bracketOpacity = reportElem.Properties.GetInteger("BracketHighlightColorAlpha", 40);
            result[MonacoColorKeys.BracketMatchBackground] = Color.FromArgb(bracketOpacity, bracketColor);
            result[MonacoColorKeys.BracketMatchBorder] = Color.FromArgb(bracketOpacity, bracketColor);
            result[MonacoColorKeys.BracketMatchHighlightForeground1] = skin.Colors.GetColor("Question");
            result[MonacoColorKeys.BracketMatchHighlightForeground2] = skin.Colors.GetColor("Information");
            result[MonacoColorKeys.BracketMatchHighlightForeground3] = skin.Colors.GetColor("Critical");

            // === Find ===
            result[MonacoColorKeys.FindMatchBackground] = skin.Colors.GetColor("Warning");
            result[MonacoColorKeys.FindMatchHighlightBackground] = skin.Colors.GetColor("Warning");

            // === Hover ===
            result[MonacoColorKeys.HoverWidgetBackground] = skin.Colors.GetColor("Window");
            result[MonacoColorKeys.HoverWidgetBorder] = skin.Colors.GetColor("DisabledText");

            // === Suggest ===
            result[MonacoColorKeys.SuggestWidgetBackground] = skin.Colors.GetColor("Window");
            result[MonacoColorKeys.SuggestWidgetSelectedBackground] = skin.Colors.GetColor("Highlight");
            result[MonacoColorKeys.SuggestWidgetSelectedForeground] = skin.Colors.GetColor("HighlightText");
            result[MonacoColorKeys.SuggestWidgetFocusHighlightForeground] = skin.Colors.GetColor("HighlightText");

            return result;
        }

        static IList<MonacoThemeRule> CreateDevExpressTokenRules(UserLookAndFeel lookAndFeel, IList<MonacoThemeRule>? rules) {
            List<MonacoThemeRule> result = rules != null ? new List<MonacoThemeRule>(rules) : new List<MonacoThemeRule>();

            Skin skin = CommonSkins.GetSkin(lookAndFeel);
            if(skin == null)
                return result;

            Color blue = skin.Colors.GetColor("Question");
            Color green = skin.Colors.GetColor("Information");
            Color red = skin.Colors.GetColor("Critical");
            Color black = skin.Colors.GetColor("WindowText");
            Color white = skin.Colors.GetColor("Window");

            var existingTokens = new HashSet<string>(result.Select(r => r.Token));

            void AddRuleIfNotExists(string token, Color foreground) {
                if(!existingTokens.Contains(token)) {
                    result.Add(new MonacoThemeRule { Token = token, Foreground = foreground });
                }
            }

            // Comments
            AddRuleIfNotExists("comment", green);

            // Keywords
            AddRuleIfNotExists("keyword", blue);

            // Types and Classes
            AddRuleIfNotExists("type", blue);
            AddRuleIfNotExists("class", blue);
            AddRuleIfNotExists("interface", blue);
            AddRuleIfNotExists("enum", blue);
            AddRuleIfNotExists("struct", blue);

            // Functions and Methods
            AddRuleIfNotExists("function", black);
            AddRuleIfNotExists("method", black);

            // Strings
            AddRuleIfNotExists("string", red);
            AddRuleIfNotExists("regexp", red);
            AddRuleIfNotExists("regex", red);

            // Numbers and Constants
            AddRuleIfNotExists("number", green);
            AddRuleIfNotExists("constant", green);
            AddRuleIfNotExists("constant.language", blue);

            // Variables and Identifiers
            AddRuleIfNotExists("variable", black);
            AddRuleIfNotExists("identifier", black);
            AddRuleIfNotExists("parameter", black);
            AddRuleIfNotExists("property", black);
            AddRuleIfNotExists("member", black);

            // Operators and Delimiters
            AddRuleIfNotExists("operator", black);
            AddRuleIfNotExists("delimiter", black);
            AddRuleIfNotExists("punctuation", black);

            // Namespaces
            AddRuleIfNotExists("namespace", black);

            // Attributes and Annotations
            AddRuleIfNotExists("annotation", green);
            AddRuleIfNotExists("decorator", green);
            AddRuleIfNotExists("attribute", green);

            // Preprocessor
            AddRuleIfNotExists("preprocessor", blue);
            AddRuleIfNotExists("macro", blue);

            // Tags (for XML/HTML)
            AddRuleIfNotExists("tag", blue);

            // Invalid and Errors
            AddRuleIfNotExists("invalid", red);
            AddRuleIfNotExists("error", red);

            return result;
        }
    }
}
