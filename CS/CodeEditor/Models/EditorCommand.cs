using System.Text.Json.Serialization;

namespace CodeEditor.Models {
    public enum EditorCommandType {
        SetText,
        SetReadOnly,
        MarkAsSaved,
        GetLanguages,
        SetLanguage,
        UpdateOption,
        RegisterTheme,
        SetTheme,
        RegisterLanguage
    }

    public sealed class EditorCommand {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EditorCommandType Type { get; set; }
        public object? Payload { get; set; }
    }
}
