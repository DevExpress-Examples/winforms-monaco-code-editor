using System.Collections.Generic;

namespace CodeEditor {
    /// <summary>
    /// Callback interface for handling messages received from the Monaco editor.
    /// </summary>
    public interface IEditorMessageHandler {
        void OnTextChanged(string text);
        void OnEditorReady();
        void OnIsModifiedChanged(bool isModified);
        void OnLanguagesReceived(IReadOnlyList<string> languages);
    }
}
