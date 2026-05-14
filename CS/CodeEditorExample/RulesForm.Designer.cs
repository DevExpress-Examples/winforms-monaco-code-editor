using CodeEditor.Theming;

namespace CodeEditorExample {
    partial class RulesForm {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing) {
                rawEditor?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            chkRawMode = new DevExpress.XtraEditors.CheckEdit();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            navigationFrame1 = new DevExpress.XtraBars.Navigation.NavigationFrame();
            gridPage = new DevExpress.XtraBars.Navigation.NavigationPage();
            gridControl = new DevExpress.XtraGrid.GridControl();
            gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckedComboBoxEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            rawPage = new DevExpress.XtraBars.Navigation.NavigationPage();
            rawEditor = new CodeEditor.CodeEditor();
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            btnSave = new DevExpress.XtraEditors.SimpleButton();
            stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            ((System.ComponentModel.ISupportInitialize)chkRawMode.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)navigationFrame1).BeginInit();
            navigationFrame1.SuspendLayout();
            gridPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckedComboBoxEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit1).BeginInit();
            rawPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stackPanel1).BeginInit();
            stackPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // chkRawMode
            // 
            chkRawMode.Location = new System.Drawing.Point(14, 14);
            chkRawMode.Name = "chkRawMode";
            chkRawMode.Properties.Caption = "Edit Raw JS";
            chkRawMode.Size = new System.Drawing.Size(108, 24);
            chkRawMode.StyleController = layoutControl1;
            chkRawMode.TabIndex = 0;
            chkRawMode.CheckedChanged += ChkRawMode_CheckedChanged;
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(navigationFrame1);
            layoutControl1.Controls.Add(chkRawMode);
            layoutControl1.Controls.Add(simpleButton1);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(863, 386, 812, 500);
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(800, 611);
            layoutControl1.TabIndex = 1;
            layoutControl1.Text = "layoutControl1";
            // 
            // navigationFrame1
            // 
            navigationFrame1.Controls.Add(gridPage);
            navigationFrame1.Controls.Add(rawPage);
            navigationFrame1.Location = new System.Drawing.Point(14, 45);
            navigationFrame1.Name = "navigationFrame1";
            navigationFrame1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] { gridPage, rawPage });
            navigationFrame1.SelectedPage = gridPage;
            navigationFrame1.Size = new System.Drawing.Size(772, 552);
            navigationFrame1.TabIndex = 3;
            navigationFrame1.Text = "navigationFrame1";
            navigationFrame1.TransitionAnimationProperties.FrameCount = 0;
            navigationFrame1.TransitionAnimationProperties.FrameInterval = 0;
            navigationFrame1.TransitionType = DevExpress.Utils.Animation.Transitions.Fade;
            // 
            // gridPage
            // 
            gridPage.Caption = "gridPage";
            gridPage.Controls.Add(gridControl);
            gridPage.Name = "gridPage";
            gridPage.Size = new System.Drawing.Size(772, 552);
            // 
            // gridControl
            // 
            gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControl.Location = new System.Drawing.Point(0, 0);
            gridControl.MainView = gridView;
            gridControl.Name = "gridControl";
            gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemButtonEdit1, repositoryItemCheckedComboBoxEdit1 });
            gridControl.Size = new System.Drawing.Size(772, 552);
            gridControl.TabIndex = 0;
            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView });
            // 
            // gridView
            // 
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn1, gridColumn2, gridColumn3, gridColumn4, gridColumn5 });
            gridView.DetailHeight = 373;
            gridView.GridControl = gridControl;
            gridView.Name = "gridView";
            gridView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "Token";
            gridColumn1.FieldName = "Token";
            gridColumn1.MinWidth = 25;
            gridColumn1.Name = "gridColumn1";
            gridColumn1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 0;
            gridColumn1.Width = 148;
            // 
            // gridColumn2
            // 
            gridColumn2.Caption = "Foreground";
            gridColumn2.FieldName = "Foreground";
            gridColumn2.MinWidth = 25;
            gridColumn2.Name = "gridColumn2";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 1;
            gridColumn2.Width = 173;
            // 
            // gridColumn3
            // 
            gridColumn3.Caption = "Background";
            gridColumn3.FieldName = "Background";
            gridColumn3.MinWidth = 25;
            gridColumn3.Name = "gridColumn3";
            gridColumn3.Visible = true;
            gridColumn3.VisibleIndex = 2;
            gridColumn3.Width = 171;
            // 
            // gridColumn4
            // 
            gridColumn4.Caption = "Style";
            gridColumn4.ColumnEdit = repositoryItemCheckedComboBoxEdit1;
            gridColumn4.FieldName = "FontStyle";
            gridColumn4.MinWidth = 25;
            gridColumn4.Name = "gridColumn4";
            gridColumn4.Visible = true;
            gridColumn4.VisibleIndex = 3;
            gridColumn4.Width = 198;
            // 
            // repositoryItemCheckedComboBoxEdit1
            // 
            repositoryItemCheckedComboBoxEdit1.AutoHeight = false;
            repositoryItemCheckedComboBoxEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemCheckedComboBoxEdit1.Name = "repositoryItemCheckedComboBoxEdit1";
            // 
            // gridColumn5
            // 
            gridColumn5.Caption = "Delete";
            gridColumn5.ColumnEdit = repositoryItemButtonEdit1;
            gridColumn5.MinWidth = 25;
            gridColumn5.Name = "gridColumn5";
            gridColumn5.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            gridColumn5.OptionsColumn.AllowIncrementalSearch = false;
            gridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            gridColumn5.OptionsColumn.AllowMove = false;
            gridColumn5.OptionsColumn.AllowSize = false;
            gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            gridColumn5.OptionsColumn.FixedWidth = true;
            gridColumn5.OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridColumn5.OptionsColumn.ReadOnly = true;
            gridColumn5.Visible = true;
            gridColumn5.VisibleIndex = 4;
            gridColumn5.Width = 52;
            // 
            // repositoryItemButtonEdit1
            // 
            repositoryItemButtonEdit1.AutoHeight = false;
            repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "Delete token", -1, true, true, true, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            repositoryItemButtonEdit1.ButtonClick += repositoryItemButtonEdit1_ButtonClick;
            // 
            // rawPage
            // 
            rawPage.Caption = "rawPage";
            rawPage.Controls.Add(rawEditor);
            rawPage.Name = "rawPage";
            rawPage.Size = new System.Drawing.Size(772, 552);
            // 
            // rawEditor
            // 
            rawEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            rawEditor.EditorLanguage = "javascript";
            rawEditor.Location = new System.Drawing.Point(0, 0);
            rawEditor.Name = "rawEditor";
            rawEditor.ShowLineNumbers = false;
            rawEditor.Size = new System.Drawing.Size(772, 552);
            rawEditor.TabIndex = 0;
            // 
            // simpleButton1
            // 
            simpleButton1.Location = new System.Drawing.Point(643, 14);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new System.Drawing.Size(143, 27);
            simpleButton1.StyleController = layoutControl1;
            simpleButton1.TabIndex = 2;
            simpleButton1.Text = "Add new token";
            simpleButton1.Click += simpleButton1_Click;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem3, layoutControlItem1, emptySpaceItem1, layoutControlItem2 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(800, 611);
            Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = chkRawMode;
            layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            layoutControlItem3.MaxSize = new System.Drawing.Size(112, 27);
            layoutControlItem3.MinSize = new System.Drawing.Size(112, 27);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(112, 31);
            layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = navigationFrame1;
            layoutControlItem1.Location = new System.Drawing.Point(0, 31);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(776, 556);
            layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new System.Drawing.Point(112, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(517, 31);
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = simpleButton1;
            layoutControlItem2.Location = new System.Drawing.Point(629, 0);
            layoutControlItem2.MaxSize = new System.Drawing.Size(147, 31);
            layoutControlItem2.MinSize = new System.Drawing.Size(147, 31);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(147, 31);
            layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem2.TextVisible = false;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(621, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(80, 25);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(705, 2);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(80, 25);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.Click += BtnSave_Click;
            // 
            // stackPanel1
            // 
            stackPanel1.AutoSize = true;
            stackPanel1.Controls.Add(btnSave);
            stackPanel1.Controls.Add(btnCancel);
            stackPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            stackPanel1.LayoutDirection = DevExpress.Utils.Layout.StackPanelLayoutDirection.RightToLeft;
            stackPanel1.Location = new System.Drawing.Point(0, 611);
            stackPanel1.Name = "stackPanel1";
            stackPanel1.Size = new System.Drawing.Size(800, 29);
            stackPanel1.TabIndex = 4;
            stackPanel1.UseSkinIndents = true;
            // 
            // RulesForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(800, 640);
            Controls.Add(layoutControl1);
            Controls.Add(stackPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RulesForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Change Rules";
            ((System.ComponentModel.ISupportInitialize)chkRawMode.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)navigationFrame1).EndInit();
            navigationFrame1.ResumeLayout(false);
            gridPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckedComboBoxEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit1).EndInit();
            rawPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)stackPanel1).EndInit();
            stackPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private void ConfigureGridColumns() {
            // Delete button column
            var colDelete = new DevExpress.XtraGrid.Columns.GridColumn { 
                Caption = "Delete", 
                FieldName = "Delete", 
                Width = 50, 
                UnboundType = DevExpress.Data.UnboundColumnType.Object 
            };
            colDelete.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colDelete.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            colDelete.OptionsColumn.AllowMove = false;
            colDelete.OptionsColumn.AllowSize = false;
            colDelete.OptionsColumn.FixedWidth = true;

            var deleteButtonRepo = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            deleteButtonRepo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            deleteButtonRepo.Buttons[0].Caption = "Delete Rule";
            deleteButtonRepo.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete;
            deleteButtonRepo.ButtonClick += (s, e) => {
                if (gridView.FocusedRowHandle >= 0)
                    gridView.DeleteRow(gridView.FocusedRowHandle);
            };
            colDelete.ColumnEdit = deleteButtonRepo;
            gridControl.RepositoryItems.Add(deleteButtonRepo);
            colDelete.Visible = true;
            colDelete.VisibleIndex = 0;
            gridView.Columns.Add(colDelete);

            // Token column
            var colToken = new DevExpress.XtraGrid.Columns.GridColumn { 
                FieldName = "Token", 
                Caption = "Token" 
            };
            colToken.Visible = true;
            colToken.VisibleIndex = 1;
            gridView.Columns.Add(colToken);

            // Foreground (color picker)
            var colForeground = new DevExpress.XtraGrid.Columns.GridColumn { 
                FieldName = "Foreground", 
                Caption = "Foreground" 
            };
            var foregroundRepo = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            foregroundRepo.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            colForeground.ColumnEdit = foregroundRepo;
            gridControl.RepositoryItems.Add(foregroundRepo);
            colForeground.Visible = true;
            colForeground.VisibleIndex = 2;
            gridView.Columns.Add(colForeground);

            // Background (color picker, hidden by default)
            var colBackground = new DevExpress.XtraGrid.Columns.GridColumn { 
                FieldName = "Background", 
                Caption = "Background" 
            };
            var backgroundRepo = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            backgroundRepo.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            colBackground.ColumnEdit = backgroundRepo;
            gridControl.RepositoryItems.Add(backgroundRepo);
            colBackground.Visible = false;
            gridView.Columns.Add(colBackground);

            // FontStyle (checked combobox)
            var colFontStyle = new DevExpress.XtraGrid.Columns.GridColumn { 
                FieldName = "FontStyle", 
                Caption = "Font Style" 
            };
            var fontStyleRepo = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            fontStyleRepo.Items.Add(CodeEditor.Theming.MonacoFontStyle.Bold, "Bold");
            fontStyleRepo.Items.Add(CodeEditor.Theming.MonacoFontStyle.Italic, "Italic");
            fontStyleRepo.Items.Add(CodeEditor.Theming.MonacoFontStyle.Underline, "Underline");
            colFontStyle.ColumnEdit = fontStyleRepo;
            gridControl.RepositoryItems.Add(fontStyleRepo);
            colFontStyle.Visible = true;
            colFontStyle.VisibleIndex = 3;
            gridView.Columns.Add(colFontStyle);
        }

        #endregion
        private DevExpress.XtraEditors.CheckEdit chkRawMode;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private CodeEditor.CodeEditor rawEditor;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraBars.Navigation.NavigationFrame navigationFrame1;
        private DevExpress.XtraBars.Navigation.NavigationPage rawPage;
        private DevExpress.XtraBars.Navigation.NavigationPage gridPage;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
