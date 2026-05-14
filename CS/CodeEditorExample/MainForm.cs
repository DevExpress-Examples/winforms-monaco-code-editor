using DevExpress.XtraBars;
using CodeEditorExample.Helpers;
using CodeEditor.Models;
using CodeEditor.Theming;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CodeEditorExample {
    public partial class MainForm : DevExpress.XtraEditors.XtraForm {
        List<MonacoThemeRule>? currentRules;

        public MainForm() {
            InitializeComponent();
            ConfigureUI();
            codeEditor.Text = Constants.defaultCSharpText.Replace("    ", "\t");
            currentRules = [.. codeEditor.Rules];
        }

        void ConfigureUI() {
            codeEditor.EditorInitialized += async (s, e) => {
                try {
                    await RefreshLanguages();
                    SetSelectedLanguage("csharp");
                }
                catch(Exception ex) {
                    MessageBox.Show($"An error occured: {ex.Message}");
                }
            };

            // Populate combo boxes

            cbeLanguage.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            cbeLanguage.Properties.AutoComplete = true;

            cbeWordWrap.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbeWordWrap.Properties.Items.AddRange(Enum.GetValues(typeof(EditorWordWrap)));
            cbeWordWrap.EditValue = codeEditor.WordWrap;

            cbeAutoIndent.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbeAutoIndent.Properties.Items.AddRange(Enum.GetValues(typeof(EditorAutoIndent)));
            cbeAutoIndent.EditValue = codeEditor.AutoIndent;

            // Configure SpinEdit 

            seLineNumbersMinChars.Properties.MinValue = 1;
            seLineNumbersMinChars.Properties.MaxValue = decimal.MaxValue;
            seLineNumbersMinChars.Properties.IsFloatValue = false;
            seLineNumbersMinChars.EditValue = codeEditor.LineNumbersMinChars;
            seLineNumbersMinChars.EditValueChanged += seLineNumbersMinChars_EditValueChanged;

            seScrollBeyondLastColumn.Properties.MinValue = 0;
            seScrollBeyondLastColumn.Properties.MaxValue = decimal.MaxValue;
            seScrollBeyondLastColumn.Properties.IsFloatValue = false;
            seScrollBeyondLastColumn.EditValue = codeEditor.ScrollBeyondLastColumn;
            seScrollBeyondLastColumn.EditValueChanged += seScrollBeyondLastColumn_EditValueChanged;

            seTabSize.Properties.MinValue = 1;
            seTabSize.Properties.MaxValue = 64;
            seTabSize.Properties.IsFloatValue = false;
            seTabSize.EditValue = codeEditor.TabSize;
            seTabSize.EditValueChanged += seTabSize_EditValueChanged;

            // Configure active skin combo
            cbeActiveSkin.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbeActiveSkin.Properties.Items.Add("Default");
            cbeActiveSkin.Properties.Items.AddRange(Enum.GetValues(typeof(MonacoThemeBase)));
            cbeActiveSkin.EditValue = "Default";
            cbeActiveSkin.Enabled = !codeEditor.ApplyDevExpressColors;
            cbeActiveSkin.EditValueChanged += cbeActiveSkin_EditValueChanged;

            // Sync check edits with initial editor values
            applySkinColorsCheckItem.Checked = codeEditor.ApplyDevExpressColors;
            ceReadOnly.Checked = codeEditor.ReadOnly;
            ceContextMenu.Checked = codeEditor.EnableContextMenu;
            ceDragAndDrop.Checked = codeEditor.EnableDragAndDrop;
            ceLineNumbers.Checked = codeEditor.ShowLineNumbers;
            ceMinimap.Checked = codeEditor.ShowMinimap;
            ceGlyphMargin.Checked = codeEditor.ShowGlyphMargin;
            ceFolding.Checked = codeEditor.EnableFolding;
            ceStickyScroll.Checked = codeEditor.EnableStickyScroll;
            ceSmoothScrolling.Checked = codeEditor.EnableSmoothScrolling;
            ceScrollBeyondLastLine.Checked = codeEditor.EnableScrollBeyondLastLine;
            ceMouseWheelZoom.Checked = codeEditor.EnableMouseWheelZoom;
            ceInsertSpaces.Checked = codeEditor.InsertSpaces;
            ceDetectIndentation.Checked = codeEditor.DetectIndentation;
            ceQuickSuggestions.Checked = codeEditor.EnableQuickSuggestions;
            ceWordBasedSuggestions.Checked = codeEditor.EnableWordBasedSuggestions;
            ceSuggestOnTriggerCharacters.Checked = codeEditor.EnableSuggestOnTriggerCharacters;
            ceEnableParameterHints.Checked = codeEditor.EnableParameterHints;

            saveItem.Enabled = codeEditor.IsModified;
            codeEditor.IsModifiedChanged += (s, e) => {
                saveItem.Enabled = codeEditor.IsModified;
            };
        }


        void SetSelectedLanguage(string language) {
            if(cbeLanguage.Properties.Items.Contains(language))
                cbeLanguage.EditValue = language;
        }

        async System.Threading.Tasks.Task RefreshLanguages() {
            try {
                IReadOnlyList<string> languages = await codeEditor.GetAvailableLanguagesAsync();
                cbeLanguage.Properties.Items.Clear();
                if(languages != null) {
                    foreach(string lang in languages)
                        cbeLanguage.Properties.Items.Add(lang);
                }
            }
            catch { }
        }



        void openItem_ItemClick(object sender, ItemClickEventArgs e) {
            using var dlg = new OpenFileDialog();
            if(dlg.ShowDialog(this) == DialogResult.OK) {
                codeEditor.Text = File.ReadAllText(dlg.FileName);
            }
        }

        void saveItem_ItemClick(object sender, ItemClickEventArgs e) {
            using var dlg = new SaveFileDialog();
            if(dlg.ShowDialog(this) == DialogResult.OK) {
                File.WriteAllText(dlg.FileName, codeEditor.Text);
                codeEditor.MarkAsSaved();
            }
        }

        async void customLanguageItem_ItemClick(object sender, ItemClickEventArgs e) {
            try {
                using var form = new CustomLanguageForm();
                form.LanguageId = "MyLang";
                form.Monarch = Constants.MyLangMonarch;
                form.Configuration = Constants.MyLangConfiguration;

                if(form.ShowDialog(this) == DialogResult.OK) {
                    var lang = new LanguageDescriptor {
                        Id = form.LanguageId,
                        Monarch = form.Monarch,
                        Configuration = form.Configuration
                    };

                    codeEditor.RegisterLanguage(lang);
                    await RefreshLanguages();
                    SetSelectedLanguage(form.LanguageId);
                    codeEditor.EditorLanguage = form.LanguageId;
                    codeEditor.Text = Constants.TestText;
                }
            }
            catch(Exception ex) {
                MessageBox.Show($"An error occured: {ex.Message}");
            }
        }

        void rulesItem_ItemClick(object sender, ItemClickEventArgs e) {
            using var form = new RulesForm();
            if(currentRules != null) {
                form.SetRules(currentRules);
            }

            if(form.ShowDialog(this) == DialogResult.OK) {
                currentRules = form.GetRules();
                codeEditor.Rules.Clear();
                codeEditor.Rules.AddRange(currentRules);
                codeEditor.ApplyCurrentTheme();
            }
        }

        void applySkinColorsCheckItem_CheckedChanged(object sender, ItemClickEventArgs e) {
            cbeActiveSkin.Enabled = !applySkinColorsCheckItem.Checked;
            object? theme = applySkinColorsCheckItem.Checked ? null : cbeActiveSkin.EditValue;
            SetSkinOverride(theme);
            codeEditor.ApplyDevExpressColors = applySkinColorsCheckItem.Checked;
        }

        void cbeActiveSkin_EditValueChanged(object? sender, EventArgs e) {
            SetSkinOverride(cbeActiveSkin.EditValue);
            codeEditor.ApplyCurrentTheme();
        }

        void SetSkinOverride(object? theme) {
            if(theme is MonacoThemeBase themeBase)
                LookAndFeelExtensions.SkinBaseOverride = themeBase;
            else
                LookAndFeelExtensions.SkinBaseOverride = null;
        }



        void ceReadOnly_CheckedChanged(object sender, EventArgs e) {
            codeEditor.ReadOnly = ceReadOnly.Checked;
        }

        void cbeLanguage_EditValueChanged(object sender, EventArgs e) {
            string? lang = cbeLanguage.EditValue?.ToString();
            if(!string.IsNullOrWhiteSpace(lang))
                codeEditor.EditorLanguage = lang;
        }



        void ceContextMenu_CheckedChanged(object sender, EventArgs e) {
            codeEditor.EnableContextMenu = ceContextMenu.Checked;
        }

        void ceDragAndDrop_CheckedChanged(object sender, EventArgs e) {
            codeEditor.EnableDragAndDrop = ceDragAndDrop.Checked;
        }



        void ceLineNumbers_CheckedChanged(object sender, EventArgs e) {
            codeEditor.ShowLineNumbers = ceLineNumbers.Checked;
        }

        void seLineNumbersMinChars_EditValueChanged(object? sender, EventArgs e) {
            codeEditor.LineNumbersMinChars = Convert.ToInt32(seLineNumbersMinChars.EditValue);
        }

        void ceMinimap_CheckedChanged(object sender, EventArgs e) {
            codeEditor.ShowMinimap = ceMinimap.Checked;
        }

        void ceGlyphMargin_CheckedChanged(object sender, EventArgs e) {
            codeEditor.ShowGlyphMargin = ceGlyphMargin.Checked;
        }

        void ceFolding_CheckedChanged(object sender, EventArgs e) {
            codeEditor.EnableFolding = ceFolding.Checked;
        }

        void ceStickyScroll_CheckedChanged(object sender, EventArgs e) {
            codeEditor.EnableStickyScroll = ceStickyScroll.Checked;
        }

        void cbeWordWrap_EditValueChanged(object? sender, EventArgs e) {
            if(cbeWordWrap.EditValue is EditorWordWrap ww)
                codeEditor.WordWrap = ww;
        }

        void ceSmoothScrolling_CheckedChanged(object sender, EventArgs e) {
            codeEditor.EnableSmoothScrolling = ceSmoothScrolling.Checked;
        }

        void ceScrollBeyondLastLine_CheckedChanged(object sender, EventArgs e) {
            codeEditor.EnableScrollBeyondLastLine = ceScrollBeyondLastLine.Checked;
        }

        void seScrollBeyondLastColumn_EditValueChanged(object? sender, EventArgs e) {
            codeEditor.ScrollBeyondLastColumn = Convert.ToInt32(seScrollBeyondLastColumn.EditValue);
        }

        void ceMouseWheelZoom_CheckedChanged(object sender, EventArgs e) {
            codeEditor.EnableMouseWheelZoom = ceMouseWheelZoom.Checked;
        }



        void seTabSize_EditValueChanged(object sender, EventArgs e) {
            codeEditor.TabSize = Convert.ToInt32(seTabSize.EditValue);
        }

        void ceInsertSpaces_CheckedChanged(object sender, EventArgs e) {
            codeEditor.InsertSpaces = ceInsertSpaces.Checked;
        }

        void ceDetectIndentation_CheckedChanged(object sender, EventArgs e) {
            codeEditor.DetectIndentation = ceDetectIndentation.Checked;
        }

        void cbeAutoIndent_EditValueChanged(object sender, EventArgs e) {
            if(cbeAutoIndent.EditValue is EditorAutoIndent ai)
                codeEditor.AutoIndent = ai;
        }



        void ceQuickSuggestions_CheckedChanged(object sender, EventArgs e) {
            codeEditor.EnableQuickSuggestions = ceQuickSuggestions.Checked;
        }

        void ceWordBasedSuggestions_CheckedChanged(object sender, EventArgs e) {
            codeEditor.EnableWordBasedSuggestions = ceWordBasedSuggestions.Checked;
        }

        void ceSuggestOnTriggerCharacters_CheckedChanged(object sender, EventArgs e) {
            codeEditor.EnableSuggestOnTriggerCharacters = ceSuggestOnTriggerCharacters.Checked;
        }

        void ceEnableParameterHints_CheckedChanged(object sender, EventArgs e) {
            codeEditor.EnableParameterHints = ceEnableParameterHints.Checked;
        }

    }
}
