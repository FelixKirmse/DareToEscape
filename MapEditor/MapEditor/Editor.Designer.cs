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
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            ((System.ComponentModel.ISupportInitialize)(this._pctSurface)).BeginInit();
            this._menustrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tileSheetBox)).BeginInit();
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
            this._treeView.Location = new System.Drawing.Point(12, 54);
            this._treeView.Name = "_treeView";
            this._treeView.Size = new System.Drawing.Size(334, 311);
            this._treeView.TabIndex = 2;
            this._treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewAfterSelect);
            // 
            // _tileSheetBox
            // 
            this._tileSheetBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._tileSheetBox.Location = new System.Drawing.Point(12, 388);
            this._tileSheetBox.Name = "_tileSheetBox";
            this._tileSheetBox.Size = new System.Drawing.Size(334, 586);
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
            // _entitiesList
            // 
            this._entitiesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._entitiesList.Location = new System.Drawing.Point(352, 54);
            this._entitiesList.Name = "_entitiesList";
            this._entitiesList.Size = new System.Drawing.Size(243, 920);
            this._entitiesList.TabIndex = 4;
            this._entitiesList.UseCompatibleStateImageBehavior = false;
            this._entitiesList.View = System.Windows.Forms.View.SmallIcon;
            this._entitiesList.SelectedIndexChanged += new System.EventHandler(this.EntitiesListSelectedIndexChanged);
            // 
            // _codesList
            // 
            this._codesList.Location = new System.Drawing.Point(602, 531);
            this._codesList.Name = "_codesList";
            this._codesList.Size = new System.Drawing.Size(360, 443);
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
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 986);
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
            this.MainMenuStrip = this._menustrip;
            this.Name = "Editor";
            this.Text = "Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this._pctSurface)).EndInit();
            this._menustrip.ResumeLayout(false);
            this._menustrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tileSheetBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _pctSurface;
        private System.Windows.Forms.MenuStrip _menustrip;
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
        private System.Windows.Forms.ListView _entitiesList;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableEditorViewToolStripMenuItem;
        private System.Windows.Forms.ListView _codesList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}