using System.Collections.Generic;
using System.Drawing;

namespace CodeEditor.Theming {

    public enum MonacoThemeBase {
        Light,
        Dark,
        HighContrast,
        HighContrastLight
    }

    public sealed class MonacoTheme {
        public required string Name { get; init; }
        public MonacoThemeBase Base { get; init; }
        public bool Inherit { get; init; } = true;
        public Dictionary<string, Color>? Colors { get; init; } = new();
        public IList<MonacoThemeRule>? Rules { get; init; } = new List<MonacoThemeRule>();
    }
}
