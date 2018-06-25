namespace MapEditor
{
    partial class Editor
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
            this._pctSurface = new System.Windows.Forms.PictureBox();
            this._menustrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableEditorViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._treeView = new System.Windows.Forms.TreeView();
            this._tileSheetBox = new System.Windows.Forms.PictureBox();
            this.tickTimer = new System.Windows.Forms.Timer(this.components);
            this.EntityList = new System.Windows.Forms.ImageList(this.components);
            this._entitiesList = new System.Windows.Forms.ListView();
            this._codesList = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._mapSelectorDialog = new System.Windows.Forms.FolderBrowserDialog();
            this._focusTextbox = new System.Windows.Forms.TextBox();
            this._playButton = new System.Windows.Forms.Button();
            this._tileIndexLabel = new System.Windows.Forms.Label();
            this._positionLabel = new System.Windows.Forms.Label();
            this._layerGroupBox = new System.Windows.Forms.GroupBox();
            this._drawAllCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this._layerSelect = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this._pctSurface)).BeginInit();
            this._menustrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tileSheetBox)).BeginInit();
            this._layerGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _pctSurface
            // 
            this._pctSurface.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._pctSurface.Location = new System.Drawing.Point(602, 27);
            this._pctSurface.Name = "_pctSurface";
            this._pctSurface.Size = new System.Drawing.Size(640, 480);
            this._pctSurface.TabIndex = 0;
            this._pctSurface.TabStop = false;
            // 
            // _menustrip
            // 
            this._menustrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._menustrip.Dock = System.Windows.Forms.DockStyle.None;
            this._menustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this._menustrip.Location = new System.Drawing.Point(12, 0);
            this._menustrip.MaximumSize = new System.Drawing.Size(50121, 5410212);
            this._menustrip.Name = "_menustrip";
            this._menustrip.Size = new System.Drawing.Size(89, 24);
            this._menustrip.TabIndex = 1;
            this._menustrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFolderToolStripMenuItem,
            this.newFileToolStripMenuItem,
            this.renameToolStripMenuItem1,
            this.deleteToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newFolderToolStripMenuItem
            // 
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.newFolderToolStripMenuItem.Text = "New Folder";
            this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.NewFolderToolStripMenuItemClick);
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.newFileToolStripMenuItem.Text = "New Map";
            this.newFileToolStripMenuItem.Click += new System.EventHandler(this.NewFileToolStripMenuItemClick);
            // 
            // renameToolStripMenuItem1
            // 
            this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
            this.renameToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.renameToolStripMenuItem1.Size = new System.Drawing.Size(209, 22);
            this.renameToolStripMenuItem1.Text = "Rename";
            this.renameToolStripMenuItem1.Click += new System.EventHandler(this.RenameToolStripMenuItem1Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItemClick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableEditorViewToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // enableEditorViewToolStripMenuItem
            // 
            this.enableEditorViewToolStripMenuItem.CheckOnClick = true;
            this.enableEditorViewToolStripMenuItem.Name = "enableEditorViewToolStripMenuItem";
            this.enableEditorViewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.enableEditorViewToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.enableEditorViewToolStripMenuItem.Text = "Enable Editor View";
            this.enableEditorViewToolStripMenuItem.Click += new System.EventHandler(this.EnableEditorViewToolStripMenuItemClick);
            // 
            // _treeView
            // 
            this._treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._treeView.LabelEdit = true;
            this._treeView.Location = new System.Drawing.Point(12, 54);
            this._treeView.Name = "_treeView";
            this._treeView.Size = new System.Drawing.Size(334, 380);
            this._treeView.TabIndex = 2;
            this._treeView.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeViewBeforeLabelEdit);
            this._treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeViewAfterLabelEdit);
            this._treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewAfterSelect);
            // 
            // _tileSheetBox
            // 
            this._tileSheetBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tileSheetBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._tileSheetBox.Location = new System.Drawing.Point(12, 388);
            this._tileSheetBox.Name = "_tileSheetBox";
            this._tileSheetBox.Size = new System.Drawing.Size(334, 563);
            this._tileSheetBox.TabIndex = 3;
            this._tileSheetBox.TabStop = false;
            this._tileSheetBox.Paint += new System.Windows.Forms.PaintEventHandler(this.TileSheetBoxPaint);
            this._tileSheetBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TileSheetBoxMouseClick);
            // 
            // tickTimer
            // 
            this.tickTimer.Interval = 16;
            this.tickTimer.Tick += new System.EventHandler(this.TickTimerTick);
            // 
            // EntityList
            // 
            this.EntityList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.EntityList.ImageSize = new System.Drawing.Size(16, 16);
            this.EntityList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // _entitiesList
            // 
            this._entitiesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._entitiesList.Location = new System.Drawing.Point(352, 54);
            this._entitiesList.Name = "_entitiesList";
            this._entitiesList.Size = new System.Drawing.Size(243, 897);
            this._entitiesList.TabIndex = 4;
            this._entitiesList.UseCompatibleStateImageBehavior = false;
            this._entitiesList.View = System.Windows.Forms.View.SmallIcon;
            this._entitiesList.SelectedIndexChanged += new System.EventHandler(this.EntitiesListSelectedIndexChanged);
            // 
            // _codesList
            // 
            this._codesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._codesList.Location = new System.Drawing.Point(602, 531);
            this._codesList.Name = "_codesList";
            this._codesList.Size = new System.Drawing.Size(360, 420);
            this._codesList.TabIndex = 5;
            this._codesList.UseCompatibleStateImageBehavior = false;
            this._codesList.View = System.Windows.Forms.View.List;
            this._codesList.SelectedIndexChanged += new System.EventHandler(this.CodesListSelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(602, 512);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Codes/Flags";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(353, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Entities";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Maps";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 372);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "TileSheet";
            // 
            // _mapSelectorDialog
            // 
            this._mapSelectorDialog.Description = "Select the DareToEscapeContent folder!";
            this._mapSelectorDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // _focusTextbox
            // 
            this._focusTextbox.Location = new System.Drawing.Point(1023, 474);
            this._focusTextbox.Name = "_focusTextbox";
            this._focusTextbox.Size = new System.Drawing.Size(10, 20);
            this._focusTextbox.TabIndex = 10;
            // 
            // _playButton
            // 
            this._playButton.Location = new System.Drawing.Point(969, 531);
            this._playButton.Name = "_playButton";
            this._playButton.Size = new System.Drawing.Size(115, 23);
            this._playButton.TabIndex = 11;
            this._playButton.Text = "Play Game in Editor";
            this._playButton.UseVisualStyleBackColor = true;
            this._playButton.Click += new System.EventHandler(this.PlayButtonClick);
            // 
            // _tileIndexLabel
            // 
            this._tileIndexLabel.AutoSize = true;
            this._tileIndexLabel.Location = new System.Drawing.Point(113, 369);
            this._tileIndexLabel.Name = "_tileIndexLabel";
            this._tileIndexLabel.Size = new System.Drawing.Size(0, 13);
            this._tileIndexLabel.TabIndex = 12;
            // 
            // _positionLabel
            // 
            this._positionLabel.AutoSize = true;
            this._positionLabel.Location = new System.Drawing.Point(966, 665);
            this._positionLabel.Name = "_positionLabel";
            this._positionLabel.Size = new System.Drawing.Size(0, 13);
            this._positionLabel.TabIndex = 13;
            // 
            // _layerGroupBox
            // 
            this._layerGroupBox.Controls.Add(this._drawAllCheckBox);
            this._layerGroupBox.Controls.Add(this.label5);
            this._layerGroupBox.Controls.Add(this._layerSelect);
            this._layerGroupBox.Location = new System.Drawing.Point(972, 560);
            this._layerGroupBox.Name = "_layerGroupBox";
            this._layerGroupBox.Size = new System.Drawing.Size(270, 71);
            this._layerGroupBox.TabIndex = 14;
            this._layerGroupBox.TabStop = false;
            this._layerGroupBox.Text = "LayerControl";
            // 
            // _drawAllCheckBox
            // 
            this._drawAllCheckBox.AutoSize = true;
            this._drawAllCheckBox.Checked = true;
            this._drawAllCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._drawAllCheckBox.Location = new System.Drawing.Point(9, 49);
            this._drawAllCheckBox.Name = "_drawAllCheckBox";
            this._drawAllCheckBox.Size = new System.Drawing.Size(98, 17);
            this._drawAllCheckBox.TabIndex = 2;
            this._drawAllCheckBox.Text = "Draw all Layers";
            this._drawAllCheckBox.UseVisualStyleBackColor = true;
            this._drawAllCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "WorkLayer:";
            // 
            // _layerSelect
            // 
            this._layerSelect.FormattingEnabled = true;
            this._layerSelect.Items.AddRange(new object[] {
            "Background",
            "Interactive",
            "Foreground"});
            this._layerSelect.Location = new System.Drawing.Point(74, 19);
            this._layerSelect.Name = "_layerSelect";
            this._layerSelect.Size = new System.Drawing.Size(120, 21);
            this._layerSelect.TabIndex = 0;
            this._layerSelect.SelectedIndexChanged += new System.EventHandler(this.LayerSelectSelectedIndexChanged);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 956);
            this.Controls.Add(this._layerGroupBox);
            this.Controls.Add(this._positionLabel);
            this.Controls.Add(this._tileIndexLabel);
            this.Controls.Add(this._playButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._codesList);
            this.Controls.Add(this._entitiesList);
            this.Controls.Add(this._tileSheetBox);
            this.Controls.Add(this._treeView);
            this.Controls.Add(this._pctSurface);
            this.Controls.Add(this._menustrip);
            this.Controls.Add(this._focusTextbox);
            this.MainMenuStrip = this._menustrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1270, 1024);
            this.MinimumSize = new System.Drawing.Size(1270, 858);
            this.Name = "Editor";
            this.Text = "Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this._pctSurface)).EndInit();
            this._menustrip.ResumeLayout(false);
            this._menustrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tileSheetBox)).EndInit();
            this._layerGroupBox.ResumeLayout(false);
            this._layerGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _pctSurface;
        private System.Windows.Forms.MenuStrip _menustrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TreeView _treeView;
        private System.Windows.Forms.PictureBox _tileSheetBox;
        private System.Windows.Forms.Timer tickTimer;
        private System.Windows.Forms.ImageList EntityList;
        private System.Windows.Forms.ListView _entitiesList;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableEditorViewToolStripMenuItem;
        private System.Windows.Forms.ListView _codesList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog _mapSelectorDialog;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.TextBox _focusTextbox;
        private System.Windows.Forms.Button _playButton;
        private System.Windows.Forms.Label _tileIndexLabel;
        public System.Windows.Forms.Label _positionLabel;
        private System.Windows.Forms.GroupBox _layerGroupBox;
        private System.Windows.Forms.CheckBox _drawAllCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox _layerSelect;
    }
}