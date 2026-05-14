namespace CodeEditorExample {
    partial class CustomLanguageForm {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing) {
                monarchEditor?.Dispose();
                configEditor?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            txtLanguageId = new DevExpress.XtraEditors.TextEdit();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            configEditor = new CodeEditor.CodeEditor();
            monarchEditor = new CodeEditor.CodeEditor();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            btnSave = new DevExpress.XtraEditors.SimpleButton();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            ((System.ComponentModel.ISupportInitialize)txtLanguageId.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stackPanel1).BeginInit();
            stackPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtLanguageId
            // 
            txtLanguageId.Location = new System.Drawing.Point(126, 14);
            txtLanguageId.Name = "txtLanguageId";
            txtLanguageId.Properties.NullValuePrompt = "Enter a Language Name (letters, digits, '-' or '_'; case-sensitive).";
            txtLanguageId.Size = new System.Drawing.Size(660, 22);
            txtLanguageId.StyleController = layoutControl1;
            txtLanguageId.TabIndex = 0;
            txtLanguageId.EditValueChanged += TxtLanguageId_EditValueChanged;
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(configEditor);
            layoutControl1.Controls.Add(monarchEditor);
            layoutControl1.Controls.Add(txtLanguageId);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(645, 322, 812, 500);
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(800, 609);
            layoutControl1.TabIndex = 1;
            layoutControl1.Text = "layoutControl1";
            // 
            // configEditor
            // 
            configEditor.EditorLanguage = "javascript";
            configEditor.Location = new System.Drawing.Point(402, 40);
            configEditor.Name = "configEditor";
            configEditor.ShowLineNumbers = false;
            configEditor.Size = new System.Drawing.Size(384, 554);
            configEditor.TabIndex = 3;
            // 
            // monarchEditor
            // 
            monarchEditor.EditorLanguage = "javascript";
            monarchEditor.Location = new System.Drawing.Point(14, 40);
            monarchEditor.Name = "monarchEditor";
            monarchEditor.ShowLineNumbers = false;
            monarchEditor.Size = new System.Drawing.Size(384, 554);
            monarchEditor.TabIndex = 2;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlItem3, layoutControlItem2, simpleSeparator1 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(800, 609);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = txtLanguageId;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(776, 26);
            layoutControlItem1.Text = "Language Name:";
            layoutControlItem1.TextSize = new System.Drawing.Size(97, 16);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = configEditor;
            layoutControlItem3.Location = new System.Drawing.Point(388, 26);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(388, 558);
            layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = monarchEditor;
            layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(388, 558);
            layoutControlItem2.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            simpleSeparator1.Location = new System.Drawing.Point(0, 584);
            simpleSeparator1.Name = "simpleSeparator1";
            simpleSeparator1.Size = new System.Drawing.Size(776, 1);
            // 
            // btnSave
            // 
            btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnSave.Enabled = false;
            btnSave.Location = new System.Drawing.Point(695, 2);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(90, 27);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(591, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(100, 27);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            // 
            // stackPanel1
            // 
            stackPanel1.AutoSize = true;
            stackPanel1.Controls.Add(btnSave);
            stackPanel1.Controls.Add(btnCancel);
            stackPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            stackPanel1.LayoutDirection = DevExpress.Utils.Layout.StackPanelLayoutDirection.RightToLeft;
            stackPanel1.Location = new System.Drawing.Point(0, 609);
            stackPanel1.Name = "stackPanel1";
            stackPanel1.Size = new System.Drawing.Size(800, 31);
            stackPanel1.TabIndex = 6;
            stackPanel1.UseSkinIndents = true;
            // 
            // CustomLanguageForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(800, 640);
            Controls.Add(layoutControl1);
            Controls.Add(stackPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CustomLanguageForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Register Custom Language";
            ((System.ComponentModel.ISupportInitialize)txtLanguageId.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).EndInit();
            ((System.ComponentModel.ISupportInitialize)stackPanel1).EndInit();
            stackPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txtLanguageId;
        private CodeEditor.CodeEditor monarchEditor;
        private CodeEditor.CodeEditor configEditor;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
    }
}
