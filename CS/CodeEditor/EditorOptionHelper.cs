using CodeEditor.Models;
using System;

namespace CodeEditor {
    public static class EditorOptionHelper {
        public static string ToMonacoOption(EditorOption option) {
            return option switch {
                EditorOption.LineNumbers => "lineNumbers",
                EditorOption.Minimap => "minimap",
                EditorOption.GlyphMargin => "glyphMargin",
                EditorOption.Folding => "folding",
                EditorOption.ScrollBeyondLastLine => "scrollBeyondLastLine",
                EditorOption.ScrollBeyondLastColumn => "scrollBeyondLastColumn",
                EditorOption.ContextMenu => "contextmenu",
                EditorOption.SmoothScrolling => "smoothScrolling",
                EditorOption.DragAndDrop => "dragAndDrop",
                EditorOption.MouseWheelZoom => "mouseWheelZoom",
                EditorOption.LineNumbersMinChars => "lineNumbersMinChars",
                EditorOption.WordWrap => "wordWrap",
                EditorOption.StickyScroll => "stickyScroll",
                EditorOption.TabSize => "tabSize",
                EditorOption.InsertSpaces => "insertSpaces",
                EditorOption.DetectIndentation => "detectIndentation",
                EditorOption.AutoIndent => "autoIndent",
                EditorOption.EnableQuickSuggestions => "quickSuggestions",
                EditorOption.EnableWordBasedSuggestions => "wordBasedSuggestions",
                EditorOption.EnableSuggestOnTriggerCharacters => "suggestOnTriggerCharacters",
                EditorOption.EnableParameterHints => "parameterHints",
                _ => throw new ArgumentOutOfRangeException(nameof(option))
            };
        }

        public static void SendOption(IEditorCommandChannel channel, EditorOption option, object? value) {
            string monacoOption = ToMonacoOption(option);

            object? monacoValue = option switch {
                EditorOption.LineNumbers when value is bool show => show ? "on" : "off",
                EditorOption.Minimap when value is bool enabled => new { enabled },
                _ => value
            };

            channel.Send(EditorCommandType.UpdateOption, new {
                option = monacoOption,
                value = monacoValue
            });
        }
    }
}
