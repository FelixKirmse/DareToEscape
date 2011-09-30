namespace DareToEscape.Editor
{
    partial class MapEditor
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
            this.pctSurface = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMapToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.interactiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foregroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileList = new System.Windows.Forms.ImageList(this.components);
            this.listTiles = new System.Windows.Forms.ListView();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.groupBoxRightClick = new System.Windows.Forms.GroupBox();
            this.insertTileCheckBox = new System.Windows.Forms.CheckBox();
            this.getCodeRadio = new System.Windows.Forms.RadioButton();
            this.radioUnpassable = new System.Windows.Forms.RadioButton();
            this.radioCode = new System.Windows.Forms.RadioButton();
            this.radioPassable = new System.Windows.Forms.RadioButton();
            this.timerGameUpdate = new System.Windows.Forms.Timer(this.components);
            this.cwdButton = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.cwdLabel = new System.Windows.Forms.Label();
            this.startGameButton = new System.Windows.Forms.Button();
            this.tileMapWidthInput = new System.Windows.Forms.TextBox();
            this.tileMapHeightInput = new System.Windows.Forms.TextBox();
            this.tileMapWidthLabel = new System.Windows.Forms.Label();
            this.tileMapHeightLabel = new System.Windows.Forms.Label();
            this.entityList = new System.Windows.Forms.ImageList(this.components);
            this.layerSelectGroupBox = new System.Windows.Forms.GroupBox();
            this.foregroundRadioButton = new System.Windows.Forms.RadioButton();
            this.interactiveRadioButton = new System.Windows.Forms.RadioButton();
            this.backgroundRadioButton = new System.Windows.Forms.RadioButton();
            this.coordLbl = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rectangleSelectionCheckBox = new System.Windows.Forms.CheckBox();
            this.editModeItemCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.removeCodesButton = new System.Windows.Forms.Button();
            this.addCodeButton = new System.Windows.Forms.Button();
            this.addCodeInput = new System.Windows.Forms.TextBox();
            this.codeListBox = new System.Windows.Forms.ListBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listEntities = new System.Windows.Forms.ListView();
            this.leftClickGroupBox = new System.Windows.Forms.GroupBox();
            this.deleteCheckbox = new System.Windows.Forms.CheckBox();
            this.plainLeftClick = new System.Windows.Forms.RadioButton();
            this.smartLeftClick = new System.Windows.Forms.RadioButton();
            this.playInEditorButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pctSurface)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBoxRightClick.SuspendLayout();
            this.layerSelectGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.leftClickGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pctSurface
            // 
            this.pctSurface.Location = new System.Drawing.Point(549, 27);
            this.pctSurface.Name = "pctSurface";
            this.pctSurface.Size = new System.Drawing.Size(800, 600);
            this.pctSurface.TabIndex = 0;
            this.pctSurface.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.layerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1399, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMapToolStripMenuItem,
            this.loadMapToolStripMenuItem,
            this.saveMapToolStripMenuItem1,
            this.saveMapToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newMapToolStripMenuItem
            // 
            this.newMapToolStripMenuItem.Name = "newMapToolStripMenuItem";
            this.newMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newMapToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.newMapToolStripMenuItem.Text = "New Map";
            this.newMapToolStripMenuItem.Click += new System.EventHandler(this.newMapToolStripMenuItem_Click);
            // 
            // loadMapToolStripMenuItem
            // 
            this.loadMapToolStripMenuItem.Name = "loadMapToolStripMenuItem";
            this.loadMapToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.loadMapToolStripMenuItem.Text = "&Load Map";
            this.loadMapToolStripMenuItem.Click += new System.EventHandler(this.loadMapToolStripMenuItem_Click);
            // 
            // saveMapToolStripMenuItem1
            // 
            this.saveMapToolStripMenuItem1.Name = "saveMapToolStripMenuItem1";
            this.saveMapToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMapToolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.saveMapToolStripMenuItem1.Text = "&Save Map";
            this.saveMapToolStripMenuItem1.Click += new System.EventHandler(this.saveMapToolStripMenuItem1_Click);
            // 
            // saveMapToolStripMenuItem
            // 
            this.saveMapToolStripMenuItem.Name = "saveMapToolStripMenuItem";
            this.saveMapToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.saveMapToolStripMenuItem.Text = "&Save Map as...";
            this.saveMapToolStripMenuItem.Click += new System.EventHandler(this.saveMapToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(167, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearMapToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // clearMapToolStripMenuItem
            // 
            this.clearMapToolStripMenuItem.Name = "clearMapToolStripMenuItem";
            this.clearMapToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.clearMapToolStripMenuItem.Text = "&Clear Map";
            this.clearMapToolStripMenuItem.Click += new System.EventHandler(this.clearMapToolStripMenuItem_Click);
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundToolStripMenuItem,
            this.interactiveToolStripMenuItem,
            this.foregroundToolStripMenuItem});
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.layerToolStripMenuItem.Text = "&Layer";
            // 
            // backgroundToolStripMenuItem
            // 
            this.backgroundToolStripMenuItem.Name = "backgroundToolStripMenuItem";
            this.backgroundToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.backgroundToolStripMenuItem.Text = "&Background";
            this.backgroundToolStripMenuItem.Click += new System.EventHandler(this.backgroundToolStripMenuItem_Click);
            // 
            // interactiveToolStripMenuItem
            // 
            this.interactiveToolStripMenuItem.Name = "interactiveToolStripMenuItem";
            this.interactiveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.interactiveToolStripMenuItem.Text = "&Interactive";
            this.interactiveToolStripMenuItem.Click += new System.EventHandler(this.interactiveToolStripMenuItem_Click);
            // 
            // foregroundToolStripMenuItem
            // 
            this.foregroundToolStripMenuItem.Name = "foregroundToolStripMenuItem";
            this.foregroundToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.foregroundToolStripMenuItem.Text = "&Foreground";
            this.foregroundToolStripMenuItem.Click += new System.EventHandler(this.foregroundToolStripMenuItem_Click);
            // 
            // tileList
            // 
            this.tileList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.tileList.ImageSize = new System.Drawing.Size(16, 16);
            this.tileList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listTiles
            // 
            this.listTiles.AutoArrange = false;
            this.listTiles.HideSelection = false;
            this.listTiles.LabelWrap = false;
            this.listTiles.Location = new System.Drawing.Point(194, 42);
            this.listTiles.MultiSelect = false;
            this.listTiles.Name = "listTiles";
            this.listTiles.Size = new System.Drawing.Size(327, 37);
            this.listTiles.TabIndex = 2;
            this.listTiles.TileSize = new System.Drawing.Size(16, 16);
            this.listTiles.UseCompatibleStateImageBehavior = false;
            this.listTiles.View = System.Windows.Forms.View.SmallIcon;
            this.listTiles.SelectedIndexChanged += new System.EventHandler(this.listTiles_SelectedIndexChanged);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.LargeChange = 48;
            this.vScrollBar1.Location = new System.Drawing.Point(1349, 27);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 603);
            this.vScrollBar1.TabIndex = 3;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.LargeChange = 48;
            this.hScrollBar1.Location = new System.Drawing.Point(549, 630);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(800, 20);
            this.hScrollBar1.TabIndex = 4;
            // 
            // groupBoxRightClick
            // 
            this.groupBoxRightClick.Controls.Add(this.insertTileCheckBox);
            this.groupBoxRightClick.Controls.Add(this.getCodeRadio);
            this.groupBoxRightClick.Controls.Add(this.radioUnpassable);
            this.groupBoxRightClick.Controls.Add(this.radioCode);
            this.groupBoxRightClick.Controls.Add(this.radioPassable);
            this.groupBoxRightClick.Location = new System.Drawing.Point(9, 429);
            this.groupBoxRightClick.Name = "groupBoxRightClick";
            this.groupBoxRightClick.Size = new System.Drawing.Size(173, 103);
            this.groupBoxRightClick.TabIndex = 5;
            this.groupBoxRightClick.TabStop = false;
            this.groupBoxRightClick.Text = "Right Click Mode";
            // 
            // insertTileCheckBox
            // 
            this.insertTileCheckBox.AutoSize = true;
            this.insertTileCheckBox.Location = new System.Drawing.Point(6, 80);
            this.insertTileCheckBox.Name = "insertTileCheckBox";
            this.insertTileCheckBox.Size = new System.Drawing.Size(95, 17);
            this.insertTileCheckBox.TabIndex = 10;
            this.insertTileCheckBox.Text = "Also Insert Tile";
            this.insertTileCheckBox.UseVisualStyleBackColor = true;
            this.insertTileCheckBox.CheckedChanged += new System.EventHandler(this.insertTileCheckBox_CheckedChanged);
            // 
            // getCodeRadio
            // 
            this.getCodeRadio.AutoSize = true;
            this.getCodeRadio.Location = new System.Drawing.Point(80, 35);
            this.getCodeRadio.Name = "getCodeRadio";
            this.getCodeRadio.Size = new System.Drawing.Size(70, 17);
            this.getCodeRadio.TabIndex = 9;
            this.getCodeRadio.TabStop = true;
            this.getCodeRadio.Text = "Get Code";
            this.getCodeRadio.UseVisualStyleBackColor = true;
            this.getCodeRadio.CheckedChanged += new System.EventHandler(this.getCodeRadio_CheckedChanged);
            // 
            // radioUnpassable
            // 
            this.radioUnpassable.AutoSize = true;
            this.radioUnpassable.Location = new System.Drawing.Point(80, 17);
            this.radioUnpassable.Name = "radioUnpassable";
            this.radioUnpassable.Size = new System.Drawing.Size(81, 17);
            this.radioUnpassable.TabIndex = 8;
            this.radioUnpassable.TabStop = true;
            this.radioUnpassable.Text = "Unpassable";
            this.radioUnpassable.UseVisualStyleBackColor = true;
            this.radioUnpassable.CheckedChanged += new System.EventHandler(this.radioUnpassable_CheckedChanged);
            // 
            // radioCode
            // 
            this.radioCode.AutoSize = true;
            this.radioCode.Location = new System.Drawing.Point(6, 35);
            this.radioCode.Name = "radioCode";
            this.radioCode.Size = new System.Drawing.Size(69, 17);
            this.radioCode.TabIndex = 1;
            this.radioCode.TabStop = true;
            this.radioCode.Text = "Set Code";
            this.radioCode.UseVisualStyleBackColor = true;
            this.radioCode.CheckedChanged += new System.EventHandler(this.radioCode_CheckedChanged);
            // 
            // radioPassable
            // 
            this.radioPassable.AutoSize = true;
            this.radioPassable.Checked = true;
            this.radioPassable.Location = new System.Drawing.Point(6, 17);
            this.radioPassable.Name = "radioPassable";
            this.radioPassable.Size = new System.Drawing.Size(68, 17);
            this.radioPassable.TabIndex = 0;
            this.radioPassable.TabStop = true;
            this.radioPassable.Text = "Passable";
            this.radioPassable.UseVisualStyleBackColor = true;
            this.radioPassable.CheckedChanged += new System.EventHandler(this.radioPassable_CheckedChanged);
            // 
            // timerGameUpdate
            // 
            this.timerGameUpdate.Enabled = true;
            this.timerGameUpdate.Interval = 20;
            this.timerGameUpdate.Tick += new System.EventHandler(this.timerGameUpdate_Tick);
            // 
            // cwdButton
            // 
            this.cwdButton.Location = new System.Drawing.Point(9, 27);
            this.cwdButton.Name = "cwdButton";
            this.cwdButton.Size = new System.Drawing.Size(159, 23);
            this.cwdButton.TabIndex = 8;
            this.cwdButton.Text = "Change working directory";
            this.cwdButton.UseVisualStyleBackColor = true;
            this.cwdButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // folderBrowser
            // 
            this.folderBrowser.Description = "Select working directory";
            // 
            // cwdLabel
            // 
            this.cwdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cwdLabel.AutoSize = true;
            this.cwdLabel.Location = new System.Drawing.Point(13, 663);
            this.cwdLabel.Name = "cwdLabel";
            this.cwdLabel.Size = new System.Drawing.Size(0, 13);
            this.cwdLabel.TabIndex = 9;
            // 
            // startGameButton
            // 
            this.startGameButton.Location = new System.Drawing.Point(9, 56);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(159, 23);
            this.startGameButton.TabIndex = 10;
            this.startGameButton.Text = "Start Game";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
            // 
            // tileMapWidthInput
            // 
            this.tileMapWidthInput.Location = new System.Drawing.Point(64, 142);
            this.tileMapWidthInput.Name = "tileMapWidthInput";
            this.tileMapWidthInput.Size = new System.Drawing.Size(100, 20);
            this.tileMapWidthInput.TabIndex = 12;
            this.tileMapWidthInput.Leave += new System.EventHandler(this.tileMapWidthInput_Leave);
            // 
            // tileMapHeightInput
            // 
            this.tileMapHeightInput.Location = new System.Drawing.Point(64, 168);
            this.tileMapHeightInput.Name = "tileMapHeightInput";
            this.tileMapHeightInput.Size = new System.Drawing.Size(100, 20);
            this.tileMapHeightInput.TabIndex = 13;
            this.tileMapHeightInput.Leave += new System.EventHandler(this.tileMapHeightInput_Leave);
            // 
            // tileMapWidthLabel
            // 
            this.tileMapWidthLabel.AutoSize = true;
            this.tileMapWidthLabel.Location = new System.Drawing.Point(6, 145);
            this.tileMapWidthLabel.Name = "tileMapWidthLabel";
            this.tileMapWidthLabel.Size = new System.Drawing.Size(56, 13);
            this.tileMapWidthLabel.TabIndex = 14;
            this.tileMapWidthLabel.Text = "MapWidth";
            // 
            // tileMapHeightLabel
            // 
            this.tileMapHeightLabel.AutoSize = true;
            this.tileMapHeightLabel.Location = new System.Drawing.Point(6, 171);
            this.tileMapHeightLabel.Name = "tileMapHeightLabel";
            this.tileMapHeightLabel.Size = new System.Drawing.Size(59, 13);
            this.tileMapHeightLabel.TabIndex = 15;
            this.tileMapHeightLabel.Text = "MapHeight";
            // 
            // entityList
            // 
            this.entityList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.entityList.ImageSize = new System.Drawing.Size(28, 28);
            this.entityList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // layerSelectGroupBox
            // 
            this.layerSelectGroupBox.Controls.Add(this.foregroundRadioButton);
            this.layerSelectGroupBox.Controls.Add(this.interactiveRadioButton);
            this.layerSelectGroupBox.Controls.Add(this.backgroundRadioButton);
            this.layerSelectGroupBox.Location = new System.Drawing.Point(188, 429);
            this.layerSelectGroupBox.Name = "layerSelectGroupBox";
            this.layerSelectGroupBox.Size = new System.Drawing.Size(173, 103);
            this.layerSelectGroupBox.TabIndex = 19;
            this.layerSelectGroupBox.TabStop = false;
            this.layerSelectGroupBox.Text = "Fast Layer Select";
            // 
            // foregroundRadioButton
            // 
            this.foregroundRadioButton.AutoSize = true;
            this.foregroundRadioButton.Location = new System.Drawing.Point(6, 66);
            this.foregroundRadioButton.Name = "foregroundRadioButton";
            this.foregroundRadioButton.Size = new System.Drawing.Size(79, 17);
            this.foregroundRadioButton.TabIndex = 2;
            this.foregroundRadioButton.Text = "Foreground";
            this.foregroundRadioButton.UseVisualStyleBackColor = true;
            this.foregroundRadioButton.CheckedChanged += new System.EventHandler(this.foregroundRadioButton_CheckedChanged);
            // 
            // interactiveRadioButton
            // 
            this.interactiveRadioButton.AutoSize = true;
            this.interactiveRadioButton.Checked = true;
            this.interactiveRadioButton.Location = new System.Drawing.Point(6, 43);
            this.interactiveRadioButton.Name = "interactiveRadioButton";
            this.interactiveRadioButton.Size = new System.Drawing.Size(75, 17);
            this.interactiveRadioButton.TabIndex = 1;
            this.interactiveRadioButton.TabStop = true;
            this.interactiveRadioButton.Text = "Interactive";
            this.interactiveRadioButton.UseVisualStyleBackColor = true;
            this.interactiveRadioButton.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // backgroundRadioButton
            // 
            this.backgroundRadioButton.AutoSize = true;
            this.backgroundRadioButton.Location = new System.Drawing.Point(6, 20);
            this.backgroundRadioButton.Name = "backgroundRadioButton";
            this.backgroundRadioButton.Size = new System.Drawing.Size(83, 17);
            this.backgroundRadioButton.TabIndex = 0;
            this.backgroundRadioButton.Text = "Background";
            this.backgroundRadioButton.UseVisualStyleBackColor = true;
            this.backgroundRadioButton.CheckedChanged += new System.EventHandler(this.backgroundRadioButton_CheckedChanged);
            // 
            // coordLbl
            // 
            this.coordLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.coordLbl.AutoSize = true;
            this.coordLbl.Location = new System.Drawing.Point(-170, 592);
            this.coordLbl.Name = "coordLbl";
            this.coordLbl.Size = new System.Drawing.Size(16, 13);
            this.coordLbl.TabIndex = 20;
            this.coordLbl.Text = "---";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rectangleSelectionCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(367, 429);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(173, 103);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Brushes";
            // 
            // rectangleSelectionCheckBox
            // 
            this.rectangleSelectionCheckBox.AutoSize = true;
            this.rectangleSelectionCheckBox.Location = new System.Drawing.Point(6, 21);
            this.rectangleSelectionCheckBox.Name = "rectangleSelectionCheckBox";
            this.rectangleSelectionCheckBox.Size = new System.Drawing.Size(104, 17);
            this.rectangleSelectionCheckBox.TabIndex = 0;
            this.rectangleSelectionCheckBox.Text = "Rectangle brush";
            this.rectangleSelectionCheckBox.UseVisualStyleBackColor = true;
            this.rectangleSelectionCheckBox.CheckedChanged += new System.EventHandler(this.rectangleSelectionCheckBox_CheckedChanged);
            // 
            // editModeItemCheckBox
            // 
            this.editModeItemCheckBox.AutoSize = true;
            this.editModeItemCheckBox.Checked = true;
            this.editModeItemCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.editModeItemCheckBox.Location = new System.Drawing.Point(7, 194);
            this.editModeItemCheckBox.Name = "editModeItemCheckBox";
            this.editModeItemCheckBox.Size = new System.Drawing.Size(155, 17);
            this.editModeItemCheckBox.TabIndex = 22;
            this.editModeItemCheckBox.Text = "Draw codes and passability";
            this.editModeItemCheckBox.UseVisualStyleBackColor = true;
            this.editModeItemCheckBox.CheckedChanged += new System.EventHandler(this.editModeItemCheckBox_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.removeCodesButton);
            this.groupBox3.Controls.Add(this.addCodeButton);
            this.groupBox3.Controls.Add(this.addCodeInput);
            this.groupBox3.Controls.Add(this.codeListBox);
            this.groupBox3.Location = new System.Drawing.Point(9, 223);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(356, 200);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Code Handling";
            // 
            // removeCodesButton
            // 
            this.removeCodesButton.Location = new System.Drawing.Point(194, 21);
            this.removeCodesButton.Name = "removeCodesButton";
            this.removeCodesButton.Size = new System.Drawing.Size(149, 23);
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
            this.codeListBox.FormattingEnabled = true;
            this.codeListBox.Location = new System.Drawing.Point(7, 60);
            this.codeListBox.Name = "codeListBox";
            this.codeListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.codeListBox.Size = new System.Drawing.Size(336, 134);
            this.codeListBox.TabIndex = 0;
            this.codeListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.codeListBox_KeyPress);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "map";
            this.openFileDialog.Filter = "Map-Files|*.map";
            this.openFileDialog.Title = "Load a Map";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "map";
            this.saveFileDialog.Filter = "Map-Files|*.map";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(336, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Tiles";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(336, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Entities";
            // 
            // listEntities
            // 
            this.listEntities.AutoArrange = false;
            this.listEntities.HideSelection = false;
            this.listEntities.LabelWrap = false;
            this.listEntities.Location = new System.Drawing.Point(194, 118);
            this.listEntities.MultiSelect = false;
            this.listEntities.Name = "listEntities";
            this.listEntities.Size = new System.Drawing.Size(327, 92);
            this.listEntities.TabIndex = 25;
            this.listEntities.TileSize = new System.Drawing.Size(32, 32);
            this.listEntities.UseCompatibleStateImageBehavior = false;
            this.listEntities.View = System.Windows.Forms.View.SmallIcon;
            this.listEntities.SelectedIndexChanged += new System.EventHandler(this.listEntities_SelectedIndexChanged);
            // 
            // leftClickGroupBox
            // 
            this.leftClickGroupBox.Controls.Add(this.deleteCheckbox);
            this.leftClickGroupBox.Controls.Add(this.plainLeftClick);
            this.leftClickGroupBox.Controls.Add(this.smartLeftClick);
            this.leftClickGroupBox.Location = new System.Drawing.Point(367, 354);
            this.leftClickGroupBox.Name = "leftClickGroupBox";
            this.leftClickGroupBox.Size = new System.Drawing.Size(173, 69);
            this.leftClickGroupBox.TabIndex = 27;
            this.leftClickGroupBox.TabStop = false;
            this.leftClickGroupBox.Text = "Left Click Mode";
            // 
            // deleteCheckbox
            // 
            this.deleteCheckbox.AutoSize = true;
            this.deleteCheckbox.Location = new System.Drawing.Point(74, 31);
            this.deleteCheckbox.Name = "deleteCheckbox";
            this.deleteCheckbox.Size = new System.Drawing.Size(86, 17);
            this.deleteCheckbox.TabIndex = 2;
            this.deleteCheckbox.Text = "Remove Tile";
            this.deleteCheckbox.UseVisualStyleBackColor = true;
            this.deleteCheckbox.CheckedChanged += new System.EventHandler(this.deleteCheckbox_CheckedChanged);
            // 
            // plainLeftClick
            // 
            this.plainLeftClick.AutoSize = true;
            this.plainLeftClick.Location = new System.Drawing.Point(7, 44);
            this.plainLeftClick.Name = "plainLeftClick";
            this.plainLeftClick.Size = new System.Drawing.Size(48, 17);
            this.plainLeftClick.TabIndex = 1;
            this.plainLeftClick.Text = "Plain";
            this.plainLeftClick.UseVisualStyleBackColor = true;
            // 
            // smartLeftClick
            // 
            this.smartLeftClick.AutoSize = true;
            this.smartLeftClick.Checked = true;
            this.smartLeftClick.Location = new System.Drawing.Point(7, 21);
            this.smartLeftClick.Name = "smartLeftClick";
            this.smartLeftClick.Size = new System.Drawing.Size(52, 17);
            this.smartLeftClick.TabIndex = 0;
            this.smartLeftClick.TabStop = true;
            this.smartLeftClick.Text = "Smart";
            this.smartLeftClick.UseVisualStyleBackColor = true;
            this.smartLeftClick.CheckedChanged += new System.EventHandler(this.smartLeftClick_CheckedChanged);
            // 
            // playInEditorButton
            // 
            this.playInEditorButton.Location = new System.Drawing.Point(9, 86);
            this.playInEditorButton.Name = "playInEditorButton";
            this.playInEditorButton.Size = new System.Drawing.Size(159, 23);
            this.playInEditorButton.TabIndex = 28;
            this.playInEditorButton.Text = "Play Map in Editor";
            this.playInEditorButton.UseVisualStyleBackColor = true;
            this.playInEditorButton.Click += new System.EventHandler(this.playInEditorButton_Click);
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1399, 685);
            this.Controls.Add(this.playInEditorButton);
            this.Controls.Add(this.leftClickGroupBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listEntities);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.editModeItemCheckBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.coordLbl);
            this.Controls.Add(this.layerSelectGroupBox);
            this.Controls.Add(this.tileMapHeightLabel);
            this.Controls.Add(this.tileMapWidthLabel);
            this.Controls.Add(this.tileMapHeightInput);
            this.Controls.Add(this.tileMapWidthInput);
            this.Controls.Add(this.startGameButton);
            this.Controls.Add(this.cwdLabel);
            this.Controls.Add(this.cwdButton);
            this.Controls.Add(this.groupBoxRightClick);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.listTiles);
            this.Controls.Add(this.pctSurface);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MapEditor";
            this.Text = "MapEditor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MapEditor_FormClosed);
            this.Load += new System.EventHandler(this.MapEditor_Load);
            this.Shown += new System.EventHandler(this.MapEditor_Shown);
            this.Resize += new System.EventHandler(this.MapEditor_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pctSurface)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxRightClick.ResumeLayout(false);
            this.groupBoxRightClick.PerformLayout();
            this.layerSelectGroupBox.ResumeLayout(false);
            this.layerSelectGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.leftClickGroupBox.ResumeLayout(false);
            this.leftClickGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pctSurface;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backgroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem interactiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem foregroundToolStripMenuItem;
        private System.Windows.Forms.ImageList tileList;
        private System.Windows.Forms.ListView listTiles;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.GroupBox groupBoxRightClick;
        private System.Windows.Forms.RadioButton radioCode;
        private System.Windows.Forms.RadioButton radioPassable;
        private System.Windows.Forms.Timer timerGameUpdate;
        private System.Windows.Forms.RadioButton radioUnpassable;
        private System.Windows.Forms.Button cwdButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Label cwdLabel;
        private System.Windows.Forms.Button startGameButton;
        private System.Windows.Forms.TextBox tileMapWidthInput;
        private System.Windows.Forms.TextBox tileMapHeightInput;
        private System.Windows.Forms.Label tileMapWidthLabel;
        private System.Windows.Forms.Label tileMapHeightLabel;
        private System.Windows.Forms.ImageList entityList;
        private System.Windows.Forms.GroupBox layerSelectGroupBox;
        private System.Windows.Forms.RadioButton foregroundRadioButton;
        private System.Windows.Forms.RadioButton interactiveRadioButton;
        private System.Windows.Forms.RadioButton backgroundRadioButton;
        private System.Windows.Forms.Label coordLbl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox editModeItemCheckBox;
        private System.Windows.Forms.CheckBox rectangleSelectionCheckBox;
        private System.Windows.Forms.RadioButton getCodeRadio;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button removeCodesButton;
        private System.Windows.Forms.Button addCodeButton;
        private System.Windows.Forms.TextBox addCodeInput;
        private System.Windows.Forms.ListBox codeListBox;
        private System.Windows.Forms.CheckBox insertTileCheckBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem saveMapToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newMapToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listEntities;
        private System.Windows.Forms.GroupBox leftClickGroupBox;
        private System.Windows.Forms.RadioButton plainLeftClick;
        private System.Windows.Forms.RadioButton smartLeftClick;
        private System.Windows.Forms.CheckBox deleteCheckbox;
        private System.Windows.Forms.Button playInEditorButton;
    }
}