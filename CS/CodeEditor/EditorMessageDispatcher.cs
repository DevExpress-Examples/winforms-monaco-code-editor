using Microsoft.Web.WebView2.Core;
using CodeEditor.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace CodeEditor {
    /// <summary>
    /// Parses incoming WebView2 messages and dispatches them to an <see cref="IEditorMessageHandler"/>.
    /// </summary>
    public sealed class EditorMessageDispatcher {
        readonly IEditorMessageHandler handler;

        public EditorMessageDispatcher(IEditorMessageHandler handler) {
            this.handler = handler;
        }

        public void HandleWebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e) {
            if(sender is not CoreWebView2)
                return;

            EditorMessage? message;

            try {
                message = JsonSerializer.Deserialize<EditorMessage>(e.WebMessageAsJson, JsonSerializerOptions.Web);
            }
            catch { return; }

            if(message?.Type == null)
                return;

            switch(message.Type) {
                case EditorMessageType.TextChanged:
                    handler.OnTextChanged(message.Payload.GetString() ?? string.Empty);
                    break;
                case EditorMessageType.EditorReady:
                    handler.OnEditorReady();
                    break;
                case EditorMessageType.IsDirtyChanged:
                    handler.OnIsModifiedChanged(message.Payload.GetBoolean());
                    break;
                case EditorMessageType.Languages:
                    List<string> langs = message.Payload
                        .EnumerateArray()
                        .Select(x => x.GetString()!)
                        .ToList();
                    handler.OnLanguagesReceived(langs);
                    break;
                default:
                    break;
            }
        }
    }
}
