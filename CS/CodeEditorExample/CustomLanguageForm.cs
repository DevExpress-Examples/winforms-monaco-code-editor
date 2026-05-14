using DevExpress.XtraEditors;
using System;
using System.Text.RegularExpressions;

namespace CodeEditorExample {
    public partial class CustomLanguageForm : XtraForm {
        static readonly Regex LanguageIdRegex = new("^[a-zA-Z0-9\\-_]+$", RegexOptions.Compiled);

        public string LanguageId {
            get => txtLanguageId?.Text ?? string.Empty;
            set { if(txtLanguageId != null) txtLanguageId.Text = value; }
        }

        public string? Monarch {
            get => monarchEditor?.Text;
            set { if(monarchEditor != null) monarchEditor.Text = value ?? string.Empty; }
        }

        public string? Configuration {
            get => configEditor?.Text;
            set { if(configEditor != null) configEditor.Text = value ?? string.Empty; }
        }

        public CustomLanguageForm() {
            InitializeComponent();
        }

        void TxtLanguageId_EditValueChanged(object? sender, EventArgs e) {
            ValidateLanguageId();
        }

        void ValidateLanguageId() {
            string id = txtLanguageId.Text;
            bool valid = !string.IsNullOrWhiteSpace(id)
                && LanguageIdRegex.IsMatch(id)
                && id.Length <= 50;
            btnSave.Enabled = valid;
        }
    }
}
