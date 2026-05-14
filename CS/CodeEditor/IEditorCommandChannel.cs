using CodeEditor.Models;

namespace CodeEditor {
    public interface IEditorCommandChannel {
        bool IsReady { get; set; }
        void Send(EditorCommandType type, object? payload = null);
    }
}
