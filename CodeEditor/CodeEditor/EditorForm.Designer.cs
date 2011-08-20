namespace CodeEditor
{
    partial class EditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.editorOutput = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newWorkingDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightClickGroupBox = new System.Windows.Forms.GroupBox();
            this.getCodeRadioButton = new System.Windows.Forms.RadioButton();
            this.setCodeRadio = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.removeCodesButton = new System.Windows.Forms.Button();
            this.addCodeButton = new System.Windows.Forms.Button();
            this.addCodeInput = new System.Windows.Forms.TextBox();
            this.codeListBox = new System.Windows.Forms.ListBox();
            this.leftClickModeGroupBox = new System.Windows.Forms.GroupBox();
            this.setUnpassableRadio = new System.Windows.Forms.RadioButton();
            this.setPassableRadio = new System.Windows.Forms.RadioButton();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.gameUpdate = new System.Windows.Forms.Timer(this.components);
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.contentCopyButton = new System.Windows.Forms.Button();
            this.rectangleSelector = new System.Windows.Forms.CheckBox();
            this.showStuff = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.editorOutput)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.rightClickGroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.leftClickModeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // editorOutput
            // 
            this.editorOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editorOutput.Location = new System.Drawing.Point(403, 27);
            this.editorOutput.Name = "editorOutput";
            this.editorOutput.Size = new System.Drawing.Size(800, 600);
            this.editorOutput.TabIndex = 0;
            this.editorOutput.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1232, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mapToolStripMenuItem.Text = "Map";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.Exit);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWorkingDirectoryToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // newWorkingDirectoryToolStripMenuItem
            // 
            this.newWorkingDirectoryToolStripMenuItem.Name = "newWorkingDirectoryToolStripMenuItem";
            this.newWorkingDirectoryToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.newWorkingDirectoryToolStripMenuItem.Text = "New Working Directory";
            this.newWorkingDirectoryToolStripMenuItem.Click += new System.EventHandler(this.newWorkingDirectoryToolStripMenuItem_Click);
            // 
            // rightClickGroupBox
            // 
            this.rightClickGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rightClickGroupBox.Controls.Add(this.getCodeRadioButton);
            this.rightClickGroupBox.Controls.Add(this.setCodeRadio);
            this.rightClickGroupBox.Location = new System.Drawing.Point(12, 125);
            this.rightClickGroupBox.Name = "rightClickGroupBox";
            this.rightClickGroupBox.Size = new System.Drawing.Size(385, 56);
            this.rightClickGroupBox.TabIndex = 2;
            this.rightClickGroupBox.TabStop = false;
            this.rightClickGroupBox.Text = "Right Click Mode";
            // 
            // getCodeRadioButton
            // 
            this.getCodeRadioButton.AutoSize = true;
            this.getCodeRadioButton.Location = new System.Drawing.Point(113, 20);
            this.getCodeRadioButton.Name = "getCodeRadioButton";
            this.getCodeRadioButton.Size = new System.Drawing.Size(75, 17);
            this.getCodeRadioButton.TabIndex = 1;
            this.getCodeRadioButton.TabStop = true;
            this.getCodeRadioButton.Text = "Get Codes";
            this.getCodeRadioButton.UseVisualStyleBackColor = true;
            // 
            // setCodeRadio
            // 
            this.setCodeRadio.AutoSize = true;
            this.setCodeRadio.Checked = true;
            this.setCodeRadio.Location = new System.Drawing.Point(7, 20);
            this.setCodeRadio.Name = "setCodeRadio";
            this.setCodeRadio.Size = new System.Drawing.Size(74, 17);
            this.setCodeRadio.TabIndex = 0;
            this.setCodeRadio.TabStop = true;
            this.setCodeRadio.Text = "Set Codes";
            this.setCodeRadio.UseVisualStyleBackColor = true;
            this.setCodeRadio.CheckedChanged += new System.EventHandler(this.setCodeRadio_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.removeCodesButton);
            this.groupBox3.Controls.Add(this.addCodeButton);
            this.groupBox3.Controls.Add(this.addCodeInput);
            this.groupBox3.Controls.Add(this.codeListBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 187);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(385, 200);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Code Handling";
            // 
            // removeCodesButton
            // 
            this.removeCodesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.removeCodesButton.Location = new System.Drawing.Point(230, 34);
            this.removeCodesButton.Name = "removeCodesButton";
            this.removeCodesButton.Size = new System.Drawing.Size(149, 20);
            this.removeCodesButton.TabIndex = 3;
            this.removeCodesButton.Text = "Remove selected codes";
            this.removeCodesButton.UseVisualStyleBackColor = true;
            this.removeCodesButton.Click += new System.EventHandler(this.removeCodesButton_Click);
            // 
            // addCodeButton
            // 
            this.addCodeButton.Location = new System.Drawing.Point(113, 21);
            this.addCodeButton.Name = "addCodeButton";
            this.addCodeButton.Size = new System.Drawing.Size(75, 20);
            this.addCodeButton.TabIndex = 2;
            this.addCodeButton.Text = "Add Code";
            this.addCodeButton.UseVisualStyleBackColor = true;
            this.addCodeButton.Click += new System.EventHandler(this.addCodeButton_Click);
            // 
            // addCodeInput
            // 
            this.addCodeInput.Location = new System.Drawing.Point(7, 21);
            this.addCodeInput.Name = "addCodeInput";
            this.addCodeInput.Size = new System.Drawing.Size(100, 20);
            this.addCodeInput.TabIndex = 1;
            this.addCodeInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.addCodeInput_KeyPress);
            // 
            // codeListBox
            // 
            this.codeListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.codeListBox.FormattingEnabled = true;
            this.codeListBox.Location = new System.Drawing.Point(7, 60);
            this.codeListBox.Name = "codeListBox";
            this.codeListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.codeListBox.Size = new System.Drawing.Size(372, 134);
            this.codeListBox.TabIndex = 0;
            this.codeListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.codeListBox_KeyPress);
            // 
            // leftClickModeGroupBox
            // 
            this.leftClickModeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.leftClickModeGroupBox.Controls.Add(this.setUnpassableRadio);
            this.leftClickModeGroupBox.Controls.Add(this.setPassableRadio);
            this.leftClickModeGroupBox.Location = new System.Drawing.Point(12, 63);
            this.leftClickModeGroupBox.Name = "leftClickModeGroupBox";
            this.leftClickModeGroupBox.Size = new System.Drawing.Size(385, 56);
            this.leftClickModeGroupBox.TabIndex = 3;
            this.leftClickModeGroupBox.TabStop = false;
            this.leftClickModeGroupBox.Text = "Left Click Mode";
            // 
            // setUnpassableRadio
            // 
            this.setUnpassableRadio.AutoSize = true;
            this.setUnpassableRadio.Location = new System.Drawing.Point(113, 20);
            this.setUnpassableRadio.Name = "setUnpassableRadio";
            this.setUnpassableRadio.Size = new System.Drawing.Size(100, 17);
            this.setUnpassableRadio.TabIndex = 1;
            this.setUnpassableRadio.TabStop = true;
            this.setUnpassableRadio.Text = "Set Unpassable";
            this.setUnpassableRadio.UseVisualStyleBackColor = true;
            // 
            // setPassableRadio
            // 
            this.setPassableRadio.AutoSize = true;
            this.setPassableRadio.Checked = true;
            this.setPassableRadio.Location = new System.Drawing.Point(7, 20);
            this.setPassableRadio.Name = "setPassableRadio";
            this.setPassableRadio.Size = new System.Drawing.Size(87, 17);
            this.setPassableRadio.TabIndex = 0;
            this.setPassableRadio.TabStop = true;
            this.setPassableRadio.Text = "Set Passable";
            this.setPassableRadio.UseVisualStyleBackColor = true;
            this.setPassableRadio.CheckedChanged += new System.EventHandler(this.setPassableRadio_CheckedChanged);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.Location = new System.Drawing.Point(1206, 27);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 600);
            this.vScrollBar1.TabIndex = 25;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.Location = new System.Drawing.Point(403, 630);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(800, 17);
            this.hScrollBar1.TabIndex = 26;
            // 
            // gameUpdate
            // 
            this.gameUpdate.Interval = 16;
            this.gameUpdate.Tick += new System.EventHandler(this.gameUpdate_Tick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "tIDE - Maps|*.tide";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // contentCopyButton
            // 
            this.contentCopyButton.Location = new System.Drawing.Point(12, 369);
            this.contentCopyButton.Name = "contentCopyButton";
            this.contentCopyButton.Size = new System.Drawing.Size(385, 23);
            this.contentCopyButton.TabIndex = 27;
            this.contentCopyButton.Text = "Clear Map";
            this.contentCopyButton.UseVisualStyleBackColor = true;
            this.contentCopyButton.Click += new System.EventHandler(this.contentCopyButton_Click);
            // 
            // rectangleSelector
            // 
            this.rectangleSelector.AutoSize = true;
            this.rectangleSelector.Location = new System.Drawing.Point(12, 40);
            this.rectangleSelector.Name = "rectangleSelector";
            this.rectangleSelector.Size = new System.Drawing.Size(105, 17);
            this.rectangleSelector.TabIndex = 28;
            this.rectangleSelector.Text = "Rectangle Mode";
            this.rectangleSelector.UseVisualStyleBackColor = true;
            this.rectangleSelector.CheckedChanged += new System.EventHandler(this.rectangleSelector_CheckedChanged);
            // 
            // showStuff
            // 
            this.showStuff.AutoSize = true;
            this.showStuff.Checked = true;
            this.showStuff.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showStuff.Location = new System.Drawing.Point(125, 40);
            this.showStuff.Name = "showStuff";
            this.showStuff.Size = new System.Drawing.Size(113, 17);
            this.showStuff.TabIndex = 29;
            this.showStuff.Text = "Visualize HelpMap";
            this.showStuff.UseVisualStyleBackColor = true;
            this.showStuff.CheckedChanged += new System.EventHandler(this.showStuff_CheckedChanged);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 652);
            this.Controls.Add(this.showStuff);
            this.Controls.Add(this.rectangleSelector);
            this.Controls.Add(this.contentCopyButton);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.leftClickModeGroupBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.rightClickGroupBox);
            this.Controls.Add(this.editorOutput);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EditorForm";
            this.Text = "EditorForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Exit);
            this.Load += new System.EventHandler(this.EditorForm_Load);
            this.Shown += new System.EventHandler(this.EditorForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.editorOutput)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.rightClickGroupBox.ResumeLayout(false);
            this.rightClickGroupBox.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.leftClickModeGroupBox.ResumeLayout(false);
            this.leftClickModeGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox editorOutput;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox rightClickGroupBox;
        private System.Windows.Forms.RadioButton getCodeRadioButton;
        private System.Windows.Forms.RadioButton setCodeRadio;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button removeCodesButton;
        private System.Windows.Forms.Button addCodeButton;
        private System.Windows.Forms.TextBox addCodeInput;
        private System.Windows.Forms.ListBox codeListBox;
        private System.Windows.Forms.GroupBox leftClickModeGroupBox;
        private System.Windows.Forms.RadioButton setUnpassableRadio;
        private System.Windows.Forms.RadioButton setPassableRadio;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Timer gameUpdate;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newWorkingDirectoryToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button contentCopyButton;
        private System.Windows.Forms.CheckBox rectangleSelector;
        public System.Windows.Forms.CheckBox showStuff;

    }
}