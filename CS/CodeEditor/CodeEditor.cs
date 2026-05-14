using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using CodeEditor.Models;
using CodeEditor.Theming;

namespace CodeEditor {
    public class CodeEditor : XtraUserControl, IEditorMessageHandler {
        bool applyDevExpressColors = true;
        EditorAutoIndent autoIndent = EditorAutoIndent.Full;
        IEditorCommandChannel commandChannel = null!;
        bool detectIndentation = true;
        string editorLanguage = "csharp";
        bool enableContextMenu = true;
        bool enableDragAndDrop = true;
        bool enableFolding = true;
        bool enableMouseWheelZoom = false;
        bool enableParameterHints = true;
        bool enableQuickSuggestions = true;
        bool enableScrollBeyondLastLine = true;
        bool enableSmoothScrolling;
        bool enableStickyScroll = true;
        bool enableSuggestOnTriggerCharacters = true;
        bool enableWordBasedSuggestions = true;
        bool insertSpaces = true;
        bool isModified = false;
        TaskCompletionSource<IReadOnlyList<string>>? languagesTcs;
        int lineNumbersMinChars = 5;
        EditorMessageDispatcher messageDispatcher = null!;
        bool readOnly = false;
        readonly Dictionary<string, LanguageDescriptor> registeredLanguages = new();
        int scrollBeyondLastColumn = 5;
        bool showGlyphMargin = false;
        bool showLineNumbers = true;
        bool showMinimap = false;
        int tabSize = 4;
        string text = string.Empty;
        string themeName = "vs";
        bool updatingFromEditor;
        WebView2? webView;
        EditorWordWrap wordWrap = EditorWordWrap.Off;

        public CodeEditor() {
            webView = new WebView2 {
                Dock = DockStyle.Fill
            };
            commandChannel = new WebView2CommandChannel(webView);
            messageDispatcher = new EditorMessageDispatcher(this);
            Controls.Add(webView);
            LookAndFeel.StyleChanged += LookAndFeel_StyleChanged;
            Rules = new List<MonacoThemeRule>();
        }

        static string? ConvertFontStyle(MonacoFontStyle style) {
            if(style == MonacoFontStyle.None)
                return null;

            StringBuilder sb = new StringBuilder(32);

            if((style & MonacoFontStyle.Bold) != 0)
                sb.Append("bold ");

            if((style & MonacoFontStyle.Italic) != 0)
                sb.Append("italic ");

            if((style & MonacoFontStyle.Underline) != 0)
                sb.Append("underline ");

            if(sb.Length == 0)
                return null;

            sb.Length--;
            return sb.ToString();
        }

        // Theming helpers
        static Dictionary<string, object>? ConvertRule(MonacoThemeRule r) {
            Dictionary<string, object> rule = new Dictionary<string, object> {
                ["token"] = r.Token
            };

            if(r.Foreground is Color fg)
                rule["foreground"] = ToHex(fg, false);

            if(r.Background is Color bg)
                rule["background"] = ToHex(bg, false);

            string? fontStyle = ConvertFontStyle(r.FontStyle ?? MonacoFontStyle.None);
            if(!string.IsNullOrEmpty(fontStyle))
                rule["fontStyle"] = fontStyle;

            return rule.Count > 1 ? rule : null;
        }

        void CoreWebView2_ContextMenuRequested(object? sender, CoreWebView2ContextMenuRequestedEventArgs e) {
            e.Handled = true;
        }

        void HandleEditorReady() {
            if(commandChannel.IsReady)
                return;

            commandChannel.IsReady = true;
            ApplyCurrentState();
            ApplyCurrentTheme();
            EventHandler? handler = Events[nameof(EditorInitialized)] as EventHandler;
            handler?.Invoke(this, EventArgs.Empty);
        }

        void HandleTextChanged(string newText) {
            updatingFromEditor = true;
            try {
                text = newText;
                EventHandler? handler = Events[nameof(TextChanged)] as EventHandler;
                handler?.Invoke(this, EventArgs.Empty);
            }
            finally {
                updatingFromEditor = false;
            }
        }

        async Task InitializeAsync() {
            WebView2? wv = webView;
            if(wv == null)
                return;

            await wv.EnsureCoreWebView2Async();

            if(webView != wv)
                return;

            // Unsubscribing followed by subscribing is needed to avoid registering multiple handlers in case CodeEditor is reinitialized.
            wv.CoreWebView2.WebMessageReceived -= messageDispatcher.HandleWebMessageReceived;
            wv.CoreWebView2.WebMessageReceived += messageDispatcher.HandleWebMessageReceived;
            wv.CoreWebView2.ContextMenuRequested -= CoreWebView2_ContextMenuRequested;
            wv.CoreWebView2.ContextMenuRequested += CoreWebView2_ContextMenuRequested;

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Monaco", "index.html");

            if(!File.Exists(path))
                throw new FileNotFoundException(path);

            wv.Source = new Uri(path);
        }

        void LookAndFeel_StyleChanged(object? sender, EventArgs e) {
            ApplyCurrentTheme();
        }

        static string MapBase(MonacoThemeBase value) {
            return value switch {
                MonacoThemeBase.Light => "vs",
                MonacoThemeBase.Dark => "vs-dark",
                MonacoThemeBase.HighContrast => "hc-black",
                MonacoThemeBase.HighContrastLight => "hc-light",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        static string ToHex(Color c, bool addHashTag = true) {
            return $"{(addHashTag ? "#" : string.Empty)}{c.R:X2}{c.G:X2}{c.B:X2}{c.A:X2}".ToLower();
        }

        public void ApplyCurrentTheme() {
            MonacoTheme monacoTheme = LookAndFeel.CreateMonacoTheme(Rules, ApplyDevExpressColors);
            RegisterTheme(monacoTheme);
            ThemeName = monacoTheme.Name;
        }

        public async Task<IReadOnlyList<string>> GetAvailableLanguagesAsync(CancellationToken cancellationToken = default) {
            // RunContinuationsAsynchronously prevents the completion callback from running
            // inline on the thread that calls TrySetResult, avoiding potential deadlocks.
            TaskCompletionSource<IReadOnlyList<string>> tcs = new TaskCompletionSource<IReadOnlyList<string>>(TaskCreationOptions.RunContinuationsAsynchronously);
            languagesTcs = tcs;

            // Auto-cancel if no response arrives within 5 seconds.
            using CancellationTokenSource timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

            // When either token fires, attempt to cancel the pending TCS.
            using(linkedCts.Token.Register(() => tcs.TrySetCanceled(linkedCts.Token))) {
                try {
                    commandChannel.Send(EditorCommandType.GetLanguages);
                    return await tcs.Task.ConfigureAwait(false);
                }
                finally {
                    // Atomically clear languagesTcs only if it still points to our instance,
                    // so a concurrent call's TCS is not accidentally discarded.
                    Interlocked.CompareExchange(ref languagesTcs, null, tcs);
                }
            }
        }

        public void MarkAsSaved() {
            commandChannel.Send(EditorCommandType.MarkAsSaved);
        }

        // Public methods
        public void RegisterTheme(MonacoTheme theme) {
            if(theme == null)
                throw new ArgumentNullException(nameof(theme));

            var payload = new {
                name = theme.Name,
                @base = MapBase(theme.Base),
                inherit = theme.Inherit,
                colors = theme.Colors?.ToDictionary(kvp => kvp.Key, kvp => ToHex(kvp.Value)) ?? new Dictionary<string, string>(),
                rules = (theme.Rules ?? Enumerable.Empty<MonacoThemeRule>())
                    .Select(ConvertRule)
                    .Where(r => r != null)
                    .ToList()
            };

            commandChannel.Send(EditorCommandType.RegisterTheme, payload);
        }

        void IEditorMessageHandler.OnEditorReady() {
            HandleEditorReady();
        }

        void IEditorMessageHandler.OnIsModifiedChanged(bool isModified) {
            IsModified = isModified;
        }

        void IEditorMessageHandler.OnLanguagesReceived(IReadOnlyList<string> languages) {
            languagesTcs?.TrySetResult(languages);
        }

        // IEditorMessageHandler implementation
        void IEditorMessageHandler.OnTextChanged(string text) {
            HandleTextChanged(text);
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            _ = InitializeAsync();
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {
                WebView2? wv = webView;
                if(wv != null) {
                    if(wv.CoreWebView2 != null) {
                        wv.CoreWebView2.WebMessageReceived -= messageDispatcher.HandleWebMessageReceived;
                        wv.CoreWebView2.ContextMenuRequested -= CoreWebView2_ContextMenuRequested;
                    }
                    wv.Dispose();
                    webView = null;
                }
                commandChannel.IsReady = false;
                LookAndFeel.StyleChanged -= LookAndFeel_StyleChanged;
            }
            base.Dispose(disposing);
        }

        // Events
        public event EventHandler EditorInitialized {
            add { Events.AddHandler(nameof(EditorInitialized), value); }
            remove { Events.RemoveHandler(nameof(EditorInitialized), value); }
        }
        public event EventHandler IsModifiedChanged {
            add { Events.AddHandler(nameof(IsModifiedChanged), value); }
            remove { Events.RemoveHandler(nameof(IsModifiedChanged), value); }
        }

        // Properties
        [DefaultValue("")]
        [DXCategory(CategoryName.Appearance)]
        [AllowNull]
        public override string Text {
            get => text;
            set {
                if(text == value)
                    return;
                text = value ?? string.Empty;
                if(!updatingFromEditor)
                    commandChannel.Send(EditorCommandType.SetText, text);
                EventHandler? handler = Events[nameof(TextChanged)] as EventHandler;
                handler?.Invoke(this, EventArgs.Empty);
            }
        }

        [DefaultValue(false)]
        [DXCategory(CategoryName.Behavior)]
        public bool ReadOnly {
            get => readOnly;
            set {
                if(readOnly == value)
                    return;
                readOnly = value;
                commandChannel.Send(EditorCommandType.SetReadOnly, value);
            }
        }

        [DefaultValue(false)]
        [DXCategory(CategoryName.Data)]
        public bool IsModified {
            get => isModified;
            private set {
                if(isModified == value)
                    return;
                isModified = value;
                EventHandler? handler = Events[nameof(IsModifiedChanged)] as EventHandler;
                handler?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<MonacoThemeRule> Rules { get; }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Appearance)]
        public bool ApplyDevExpressColors {
            get => applyDevExpressColors;
            set {
                if(applyDevExpressColors == value) return;
                applyDevExpressColors = value;
                ApplyCurrentTheme();
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Appearance)]
        public bool ShowLineNumbers {
            get => showLineNumbers;
            set {
                if(showLineNumbers == value) return;
                showLineNumbers = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.LineNumbers, value);
            }
        }

        [DefaultValue(false)]
        [DXCategory(CategoryName.Appearance)]
        public bool ShowMinimap {
            get => showMinimap;
            set {
                if(showMinimap == value) return;
                showMinimap = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.Minimap, value);
            }
        }

        [DefaultValue(false)]
        [DXCategory(CategoryName.Appearance)]
        public bool ShowGlyphMargin {
            get => showGlyphMargin;
            set {
                if(showGlyphMargin == value) return;
                showGlyphMargin = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.GlyphMargin, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Appearance)]
        public bool EnableFolding {
            get => enableFolding;
            set {
                if(enableFolding == value) return;
                enableFolding = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.Folding, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableContextMenu {
            get => enableContextMenu;
            set {
                if(enableContextMenu == value) return;
                enableContextMenu = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.ContextMenu, value);
            }
        }

        [DefaultValue(false)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableSmoothScrolling {
            get => enableSmoothScrolling;
            set {
                if(enableSmoothScrolling == value) return;
                enableSmoothScrolling = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.SmoothScrolling, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableScrollBeyondLastLine {
            get => enableScrollBeyondLastLine;
            set {
                if(enableScrollBeyondLastLine == value) return;
                enableScrollBeyondLastLine = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.ScrollBeyondLastLine, value);
            }
        }

        [DefaultValue(5)]
        [DXCategory(CategoryName.Behavior)]
        public int ScrollBeyondLastColumn {
            get => scrollBeyondLastColumn;
            set {
                if(scrollBeyondLastColumn == value) return;
                scrollBeyondLastColumn = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.ScrollBeyondLastColumn, value);
            }
        }

        [DefaultValue(5)]
        [DXCategory(CategoryName.Appearance)]
        public int LineNumbersMinChars {
            get => lineNumbersMinChars;
            set {
                if(lineNumbersMinChars == value) return;
                lineNumbersMinChars = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.LineNumbersMinChars, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableDragAndDrop {
            get => enableDragAndDrop;
            set {
                if(enableDragAndDrop == value) return;
                enableDragAndDrop = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.DragAndDrop, value);
            }
        }

        [DefaultValue(false)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableMouseWheelZoom {
            get => enableMouseWheelZoom;
            set {
                if(enableMouseWheelZoom == value) return;
                enableMouseWheelZoom = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.MouseWheelZoom, value);
            }
        }

        [DefaultValue(EditorWordWrap.Off)]
        [DXCategory(CategoryName.Appearance)]
        public EditorWordWrap WordWrap {
            get => wordWrap;
            set {
                if(wordWrap == value) return;
                this.wordWrap = value;
                SetWordWrap(value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Appearance)]
        public bool EnableStickyScroll {
            get => enableStickyScroll;
            set {
                if(enableStickyScroll == value) return;
                enableStickyScroll = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.StickyScroll, new { enabled = value });
            }
        }

        [DefaultValue(4)]
        [DXCategory(CategoryName.Behavior)]
        public int TabSize {
            get => tabSize;
            set {
                if(value < 1 || value > 64)
                    throw new ArgumentOutOfRangeException(nameof(value));
                if(tabSize == value) return;
                tabSize = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.TabSize, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool DetectIndentation {
            get => detectIndentation;
            set {
                if(detectIndentation == value) return;
                detectIndentation = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.DetectIndentation, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool InsertSpaces {
            get => insertSpaces;
            set {
                if(insertSpaces == value) return;
                insertSpaces = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.InsertSpaces, value);
            }
        }

        [DefaultValue(EditorAutoIndent.Full)]
        [DXCategory(CategoryName.Behavior)]
        public EditorAutoIndent AutoIndent {
            get => autoIndent;
            set {
                if(autoIndent == value) return;
                autoIndent = value;
                SetAutoIndent(value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableQuickSuggestions {
            get => enableQuickSuggestions;
            set {
                if(enableQuickSuggestions == value) return;
                enableQuickSuggestions = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableQuickSuggestions, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableWordBasedSuggestions {
            get => enableWordBasedSuggestions;
            set {
                if(enableWordBasedSuggestions == value) return;
                enableWordBasedSuggestions = value;
                string suggestionMode = value ? "currentDocument" : "off";
                EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableWordBasedSuggestions, suggestionMode);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableSuggestOnTriggerCharacters {
            get => enableSuggestOnTriggerCharacters;
            set {
                if(enableSuggestOnTriggerCharacters == value) return;
                enableSuggestOnTriggerCharacters = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableSuggestOnTriggerCharacters, value);
            }
        }

        [DefaultValue(true)]
        [DXCategory(CategoryName.Behavior)]
        public bool EnableParameterHints {
            get => enableParameterHints;
            set {
                if(enableParameterHints == value) return;
                enableParameterHints = value;
                EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableParameterHints, new { enabled = value });
            }
        }

        [DefaultValue("vs")]
        [DXCategory(CategoryName.Appearance)]
        public string ThemeName {
            get => themeName;
            set {
                if(themeName == value) return;
                themeName = value;
                SetTheme(value);
            }
        }

        [DefaultValue("csharp")]
        [DXCategory(CategoryName.Behavior)]
        public string EditorLanguage {
            get => editorLanguage;
            set {
                if(editorLanguage == value) return;
                editorLanguage = value;
                SetEditorLanguage(value);
            }
        }

        // Property helpers
        void SetWordWrap(EditorWordWrap wrap) {
            string monacoValue = wrap switch {
                EditorWordWrap.Off => "off",
                EditorWordWrap.On => "on",
                _ => throw new ArgumentOutOfRangeException(nameof(wrap))
            };
            EditorOptionHelper.SendOption(commandChannel, EditorOption.WordWrap, monacoValue);
        }

        void SetAutoIndent(EditorAutoIndent indent) {
            string monacoValue = indent switch {
                EditorAutoIndent.None => "none",
                EditorAutoIndent.Keep => "keep",
                EditorAutoIndent.Brackets => "brackets",
                EditorAutoIndent.Advanced => "advanced",
                EditorAutoIndent.Full => "full",
                _ => throw new ArgumentOutOfRangeException(nameof(indent))
            };
            EditorOptionHelper.SendOption(commandChannel, EditorOption.AutoIndent, monacoValue);
            // a workaround for Monaco resetting TabSize when AutoIndent is changed - we need to reapply it after changing AutoIndent.
            EditorOptionHelper.SendOption(commandChannel, EditorOption.TabSize, TabSize);
        }

        void SetTheme(string name) {
            if(string.IsNullOrWhiteSpace(name))
                return;
            commandChannel.Send(EditorCommandType.SetTheme, name);
        }

        void SetEditorLanguage(string language) {
            if(string.IsNullOrWhiteSpace(language))
                return;
            commandChannel.Send(EditorCommandType.SetLanguage, language);
        }

        void RestoreRegisteredLanguages() {
            foreach(LanguageDescriptor language in registeredLanguages.Values) {
                RegisterLanguage(language);
            }
        }

        public void RegisterLanguage(LanguageDescriptor language) {
            if(language == null)
                throw new ArgumentNullException(nameof(language));

            var payload = new {
                id = language.Id,
                monarch = language.Monarch,
                configuration = language.Configuration
            };
            commandChannel.Send(EditorCommandType.RegisterLanguage, payload);
            registeredLanguages[language.Id] = language;
        }

        void ApplyCurrentState() {
            RestoreRegisteredLanguages();

            SetEditorLanguage(EditorLanguage);
            SetWordWrap(WordWrap);
            SetTheme(ThemeName);
            SetAutoIndent(AutoIndent);
            commandChannel.Send(EditorCommandType.SetReadOnly, ReadOnly);
            commandChannel.Send(EditorCommandType.SetText, Text);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.LineNumbers, ShowLineNumbers);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.Minimap, ShowMinimap);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.GlyphMargin, ShowGlyphMargin);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.Folding, EnableFolding);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.ContextMenu, EnableContextMenu);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.SmoothScrolling, EnableSmoothScrolling);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.ScrollBeyondLastLine, EnableScrollBeyondLastLine);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.ScrollBeyondLastColumn, ScrollBeyondLastColumn);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.LineNumbersMinChars, LineNumbersMinChars);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.DragAndDrop, EnableDragAndDrop);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.MouseWheelZoom, EnableMouseWheelZoom);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.StickyScroll, new { enabled = EnableStickyScroll });
            EditorOptionHelper.SendOption(commandChannel, EditorOption.TabSize, TabSize);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.InsertSpaces, InsertSpaces);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.DetectIndentation, DetectIndentation);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableQuickSuggestions, EnableQuickSuggestions);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableWordBasedSuggestions, EnableWordBasedSuggestions ? "currentDocument" : "off");
            EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableSuggestOnTriggerCharacters, EnableSuggestOnTriggerCharacters);
            EditorOptionHelper.SendOption(commandChannel, EditorOption.EnableParameterHints, new { enabled = EnableParameterHints });
        }
    }
}
