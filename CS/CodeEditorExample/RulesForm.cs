using DevExpress.XtraEditors;
using CodeEditor;
using CodeEditor.Theming;
using CodeEditorExample.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace CodeEditorExample {
    public partial class RulesForm : XtraForm {
        BindingList<RuleItem> ruleItems = new BindingList<RuleItem>();

        public RulesForm() {
            InitializeComponent();
            gridControl.DataSource = ruleItems;
            repositoryItemCheckedComboBoxEdit1.SetFlags(typeof(MonacoFontStyle));
        }

        public void SetRules(IReadOnlyList<MonacoThemeRule> rules) {
            ruleItems.Clear();
            foreach(MonacoThemeRule r in rules) {
                ruleItems.Add(new RuleItem {
                    Token = r.Token,
                    Foreground = r.Foreground,
                    Background = r.Background,
                    FontStyle = r.FontStyle
                });
            }
        }

        public List<MonacoThemeRule> GetRules() {
            return ruleItems.Select(r => new MonacoThemeRule {
                Token = r.Token,
                Foreground = r.Foreground,
                Background = r.Background,
                FontStyle = r.FontStyle
            }).ToList();
        }

        void ChkRawMode_CheckedChanged(object? sender, EventArgs e) {
            bool isRaw = chkRawMode.Checked;
            if(isRaw) {
                // Switch to raw JS mode: serialize grid rules to text
                List<MonacoThemeRule> rules = GetRules();
                rawEditor.Text = MonacoRulesParser.Serialize(rules);
                navigationFrame1.SelectedPage = rawPage;
            }
            else {
                // Switch to grid mode: parse raw JS back to rules
                if(MonacoRulesParser.TryParse(rawEditor.Text ?? string.Empty, out var parsed)) {
                    ruleItems.Clear();
                    foreach(MonacoThemeRule r in parsed) {
                        ruleItems.Add(new RuleItem {
                            Token = r.Token,
                            Foreground = r.Foreground,
                            Background = r.Background,
                            FontStyle = r.FontStyle
                        });
                    }
                }
                else {
                    XtraMessageBox.Show("Failed to parse rules. Please check the format.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    chkRawMode.Checked = true;
                    return;
                }
                navigationFrame1.SelectedPage = gridPage;
            }
        }

        void BtnSave_Click(object? sender, EventArgs e) {
            // If in raw mode, apply raw JS first
            if(chkRawMode.Checked) {
                if(!MonacoRulesParser.TryParse(rawEditor.Text ?? string.Empty, out var parsed)) {
                    XtraMessageBox.Show("Failed to parse rules. Please check the format.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ruleItems.Clear();
                foreach(MonacoThemeRule r in parsed) {
                    ruleItems.Add(new RuleItem {
                        Token = r.Token,
                        Foreground = r.Foreground,
                        Background = r.Background,
                        FontStyle = r.FontStyle
                    });
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Bindable data class for grid
        public class RuleItem {
            public string Token { get; set; } = string.Empty;
            public Color? Foreground { get; set; }
            public Color? Background { get; set; }
            public MonacoFontStyle? FontStyle { get; set; }
        }

        void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            if(e.Button.Kind == ButtonPredefines.Delete) {
                if(XtraMessageBox.Show("Do you wish to remove this token?", "Confirmation Dialog", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    gridView.DeleteRow(gridView.FocusedRowHandle);
                }
            }
        }

        void simpleButton1_Click(object sender, EventArgs e) {
            RuleItem item = new RuleItem {
                Token = "new-token"
            };
            ruleItems.Add(item);
            gridView.FocusedRowHandle = gridView.FindRow(item);
        }
    }
}
