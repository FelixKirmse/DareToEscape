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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._treeView = new System.Windows.Forms.TreeView();
            this._tileSheetBox = new System.Windows.Forms.PictureBox();
            this.tickTimer = new System.Windows.Forms.Timer(this.components);
            this.EntityList = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this._codeGroupBox = new System.Windows.Forms.GroupBox();
            this._codeBox = new System.Windows.Forms.ComboBox();
            this._addCodeBtn = new System.Windows.Forms.Button();
            this._existingCodesBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this._pctSurface)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tileSheetBox)).BeginInit();
            this._codeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _pctSurface
            // 
            this._pctSurface.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._pctSurface.Location = new System.Drawing.Point(236, 27);
            this._pctSurface.Name = "_pctSurface";
            this._pctSurface.Size = new System.Drawing.Size(640, 480);
            this._pctSurface.TabIndex = 0;
            this._pctSurface.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(45, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save as..";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // _treeView
            // 
            this._treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._treeView.Location = new System.Drawing.Point(12, 27);
            this._treeView.Name = "_treeView";
            this._treeView.Size = new System.Drawing.Size(219, 947);
            this._treeView.TabIndex = 2;
            // 
            // _tileSheetBox
            // 
            this._tileSheetBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._tileSheetBox.Location = new System.Drawing.Point(236, 513);
            this._tileSheetBox.Name = "_tileSheetBox";
            this._tileSheetBox.Size = new System.Drawing.Size(334, 254);
            this._tileSheetBox.TabIndex = 3;
            this._tileSheetBox.TabStop = false;
            this._tileSheetBox.Paint += new System.Windows.Forms.PaintEventHandler(this.TileSheetBoxPaint);
            this._tileSheetBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TileSheetBoxMouseClick);
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
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Location = new System.Drawing.Point(237, 773);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(639, 201);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // _codeGroupBox
            // 
            this._codeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._codeGroupBox.Controls.Add(this._existingCodesBox);
            this._codeGroupBox.Controls.Add(this._addCodeBtn);
            this._codeGroupBox.Controls.Add(this._codeBox);
            this._codeGroupBox.Location = new System.Drawing.Point(576, 513);
            this._codeGroupBox.Name = "_codeGroupBox";
            this._codeGroupBox.Size = new System.Drawing.Size(300, 254);
            this._codeGroupBox.TabIndex = 5;
            this._codeGroupBox.TabStop = false;
            this._codeGroupBox.Text = "Code Handling";
            // 
            // _codeBox
            // 
            this._codeBox.FormattingEnabled = true;
            this._codeBox.Location = new System.Drawing.Point(7, 20);
            this._codeBox.Name = "_codeBox";
            this._codeBox.Size = new System.Drawing.Size(174, 21);
            this._codeBox.TabIndex = 0;
            // 
            // _addCodeBtn
            // 
            this._addCodeBtn.Location = new System.Drawing.Point(188, 20);
            this._addCodeBtn.Name = "_addCodeBtn";
            this._addCodeBtn.Size = new System.Drawing.Size(106, 23);
            this._addCodeBtn.TabIndex = 1;
            this._addCodeBtn.Text = "Add Code";
            this._addCodeBtn.UseVisualStyleBackColor = true;
            // 
            // _existingCodesBox
            // 
            this._existingCodesBox.FormattingEnabled = true;
            this._existingCodesBox.Location = new System.Drawing.Point(7, 47);
            this._existingCodesBox.Name = "_existingCodesBox";
            this._existingCodesBox.Size = new System.Drawing.Size(287, 199);
            this._existingCodesBox.TabIndex = 2;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 986);
            this.Controls.Add(this._codeGroupBox);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this._tileSheetBox);
            this.Controls.Add(this._treeView);
            this.Controls.Add(this._pctSurface);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Editor";
            this.Text = "Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this._pctSurface)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tileSheetBox)).EndInit();
            this._codeGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _pctSurface;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TreeView _treeView;
        private System.Windows.Forms.PictureBox _tileSheetBox;
        private System.Windows.Forms.Timer tickTimer;
        private System.Windows.Forms.ImageList EntityList;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox _codeGroupBox;
        private System.Windows.Forms.ListBox _existingCodesBox;
        private System.Windows.Forms.Button _addCodeBtn;
        private System.Windows.Forms.ComboBox _codeBox;
    }
}