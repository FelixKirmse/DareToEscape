using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.TileEngine;
using DareToEscape.GameStates;
using DareToEscape.Providers;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using XNARectangle = Microsoft.Xna.Framework.Rectangle;

namespace DareToEscape.Editor
{
    public partial class MapEditor : Form
    {
        private readonly string _cwd = Application.StartupPath + "/Content/maps";

        private readonly string _loadLevel;
        public DareToEscape Game;
        private string _currentMapName;
        private bool _updateMapSize = true;
        private readonly EditorManager _editorManager;

        public MapEditor()
        {
            _editorManager = GameVariableProvider.EditorManager;
            InitializeComponent();
            GameVariableProvider.MapGenerator.OnGenerationFinished += () => _updateMapSize = true;
        }

        public MapEditor(string levelName)
        {
            _editorManager = GameVariableProvider.EditorManager;
            InitializeComponent();
            _loadLevel = levelName;
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            _editorManager.Deactivate();
        }

        private void LoadImageLists(bool createImages = false)
        {
            string filepath;

            filepath = Application.StartupPath + @"\Content\textures\tilesheets\tilesheet.png";
            tileList.Images.Clear();

            var tileSheet = new Bitmap(filepath);
            int tilecount = 0;
            for (int y = 0; y < tileSheet.Height/(TileMap.TileHeight + TileMap.TileOffset); ++y)
            {
                for (int x = 0; x < tileSheet.Width/(TileMap.TileWidth + TileMap.TileOffset); ++x)
                {
                    if (!createImages)
                    {
                        tileList.Images.Add(
                            new Bitmap(Application.StartupPath + "/Content/textures/editor/tiles/" +
                                       tilecount.ToString() + ".bmp"));
                    }
                    else
                    {
                        Bitmap newBitmap =
                            tileSheet.Clone(
                                new Rectangle(x*(TileMap.TileWidth + TileMap.TileOffset),
                                              y*(TileMap.TileHeight + TileMap.TileOffset), TileMap.TileWidth,
                                              TileMap.TileHeight), PixelFormat.DontCare);
                        tileList.Images.Add(newBitmap);
                        newBitmap.Save(Application.StartupPath + "/Content/textures/editor/tiles/" +
                                       tilecount.ToString() + ".bmp");
                    }
                    ++tilecount;
                }
            }

            listTiles.SmallImageList = tileList;
            tilecount = 0;
            listTiles.Clear();

            for (int y = 0; y < tileSheet.Height/(TileMap.TileHeight + TileMap.TileOffset); ++y)
            {
                for (int x = 0; x < tileSheet.Width/(TileMap.TileWidth + TileMap.TileOffset); ++x)
                {
                    string itemName = tilecount.ToString();
                    listTiles.Items.Add(new ListViewItem(itemName, tilecount++));
                }
            }


            tilecount = 0;
            listEntities.Clear();
            var dirInfo = new DirectoryInfo(Application.StartupPath + @"\Content\textures\editor\entities");
            FileInfo[] files = dirInfo.GetFiles();
            tilecount = 0;
            foreach (FileInfo file in files)
            {
                entityList.Images.Add(new Bitmap(file.FullName));
                listEntities.Items.Add(new ListViewItem(file.Name.Replace(".bmp", ""), tilecount++));
            }
            listEntities.SmallImageList = entityList;
        }

        private void FixScrollBarScales()
        {
            if (_updateMapSize)
                Camera.UpdateWorldRectangle();
            Camera.ViewPortWidth = pctSurface.Width;
            Camera.ViewPortHeight = pctSurface.Height;
        }

        private void MapEditor_Load(object sender, EventArgs e)
        {
            timerGameUpdate.Stop();
            LoadImageLists();
            FixScrollBarScales();
            TileMap.EditorMode = true;
            backgroundToolStripMenuItem.Checked = true;
        }

        private void MapEditor_Resize(object sender, EventArgs e)
        {
            FixScrollBarScales();
        }

        private void listTiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listTiles.SelectedIndices.Count > 0)
            {
                _editorManager.DrawTile = listTiles.SelectedIndices[0];
                _editorManager.CurrentItem = GameVariableProvider.EditorManager.GetEditorItemByName(listTiles.SelectedIndices[0].ToString());
            }
        }


        private void radioPassable_CheckedChanged(object sender, EventArgs e)
        {
            if (radioPassable.Checked)
            {
                _editorManager.SettingCode = false;
                _editorManager.MakePassable = true;
                _editorManager.MakeUnpassable = false;
                _editorManager.GettingCode = false;
            }
        }

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _editorManager.DrawLayer = 0;
            backgroundToolStripMenuItem.Checked = true;
            interactiveToolStripMenuItem.Checked = false;
            foregroundToolStripMenuItem.Checked = false;
        }

        private void interactiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _editorManager.DrawLayer = 1;
            backgroundToolStripMenuItem.Checked = false;
            interactiveToolStripMenuItem.Checked = true;
            foregroundToolStripMenuItem.Checked = false;
        }

        private void foregroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _editorManager.DrawLayer = 2;
            backgroundToolStripMenuItem.Checked = false;
            interactiveToolStripMenuItem.Checked = false;
            foregroundToolStripMenuItem.Checked = true;
        }

        private void timerGameUpdate_Tick(object sender, EventArgs e)
        {
            FixScrollBarScales();

            if (ActiveForm == this)
                Game.Tick();

            if (_updateMapSize)
                mapSizeLabel.Text = "Map size: " + TileMap.MapWidth + " x " + TileMap.MapHeight;
            coordLbl.Text = "MapCell: (" + _editorManager.CellCoords.X + @"|" + _editorManager.CellCoords.Y + ")";
        }

        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            try
            {
                TileMap.LoadMap(new FileStream(_cwd + @"/" + _currentMapName, FileMode.Open));
            }
            catch
            {
                Debug.Print("Unable to load map file");
            }
        }

        private void saveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
            SaveMap();
        }

        private void clearMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TileMap.ClearMap();
        }

        private void MapEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            _editorManager.Deactivate();
        }

        private void radioUnpassable_CheckedChanged(object sender, EventArgs e)
        {
            if (radioUnpassable.Checked)
            {
                _editorManager.SettingCode = false;
                _editorManager.MakePassable = false;
                _editorManager.MakeUnpassable = true;
                _editorManager.GettingCode = false;
            }
        }

        private void radioCode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCode.Checked)
            {
                _editorManager.SettingCode = true;
                _editorManager.MakePassable = false;
                _editorManager.MakeUnpassable = false;
                _editorManager.GettingCode = false;
            }
        }

        private void MapEditor_Shown(object sender, EventArgs e)
        {
            timerGameUpdate.Start();
            openFileDialog.InitialDirectory = _cwd;
            saveFileDialog.InitialDirectory = _cwd;

            if (_loadLevel != null)
            {
                TileMap.LoadMap(new FileStream(_cwd + @"/" + _loadLevel + ".map", FileMode.Open));
                _currentMapName = _loadLevel + ".map";
            }
        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            _editorManager.JumpToLevel(_currentMapName.Replace(".map", ""));
        }

        private void backgroundRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (backgroundRadioButton.Checked)
            {
                _editorManager.DrawLayer = 0;
                backgroundToolStripMenuItem.Checked = true;
                interactiveToolStripMenuItem.Checked = false;
                foregroundToolStripMenuItem.Checked = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (interactiveRadioButton.Checked)
            {
                _editorManager.DrawLayer = 1;
                backgroundToolStripMenuItem.Checked = false;
                interactiveToolStripMenuItem.Checked = true;
                foregroundToolStripMenuItem.Checked = false;
            }
        }

        private void foregroundRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (foregroundRadioButton.Checked)
            {
                _editorManager.DrawLayer = 2;
                backgroundToolStripMenuItem.Checked = false;
                interactiveToolStripMenuItem.Checked = false;
                foregroundToolStripMenuItem.Checked = true;
            }
        }

        private void editModeItemCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (editModeItemCheckBox.Checked)
                TileMap.EditorMode = true;
            else
                TileMap.EditorMode = false;
        }

        private void rectangleSelectionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rectangleSelectionCheckBox.Checked)
            {
                _editorManager.FillMode = "RECTANGLEFILL";
                getCodeRadio.Enabled = false;
            }
            else
            {
                _editorManager.FillMode = "TILEFILL";
                getCodeRadio.Enabled = true;
            }
        }

        private void getCodeRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (getCodeRadio.Checked)
            {
                _editorManager.SettingCode = false;
                _editorManager.MakePassable = false;
                _editorManager.MakeUnpassable = false;
                _editorManager.GettingCode = true;
            }
        }

        public void GetCodeList(List<string> codeList)
        {
            codeListBox.Items.Clear();
            foreach (string code in codeList)
            {
                codeListBox.Items.Add(code);
            }
        }

        public void SetCodeList(int cellX, int cellY)
        {
            var codeList = new List<string>();
            foreach (string code in codeListBox.Items)
            {
                codeList.Add(code);
            }

            List<string> codes = TileMap.GetCellCodes(cellX, cellY);
            codes = codeList;
            TileMap.SetCellCodes(cellX, cellY, codes);
        }

        private void addCodeButton_Click(object sender, EventArgs e)
        {
            if (addCodeInput.Text != "")
                codeListBox.Items.Add(addCodeInput.Text);
            addCodeInput.Text = "";
        }

        private void removeCodesButton_Click(object sender, EventArgs e)
        {
            var items = new string[codeListBox.SelectedItems.Count];
            for (int i = 0; i < codeListBox.SelectedItems.Count; ++i)
            {
                items[i] = (string) codeListBox.SelectedItems[i];
            }
            foreach (string item in items)
            {
                codeListBox.Items.Remove(item);
            }
        }

        private void addCodeInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Enter))
            {
                addCodeButton_Click(sender, e);
            }
        }

        private void codeListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Delete))
            {
                removeCodesButton_Click(sender, e);
            }
        }

        private void insertTileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _editorManager.InsertTile = insertTileCheckBox.Checked;
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            _currentMapName = openFileDialog.SafeFileName;
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            string[] filePathArray = saveFileDialog.FileName.Split('\\');
            _currentMapName = filePathArray[filePathArray.Length - 1];
        }

        private void saveMapToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentMapName))
                saveFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(_currentMapName))
                SaveMap();
        }

        private void SaveMap()
        {
            TileMap.SaveMap(new FileStream(_cwd + @"/" + _currentMapName, FileMode.Create));
        }

        private void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TileMap.ClearMap();
            _currentMapName = "";
        }

        private void deleteCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _editorManager.RemoveTile = deleteCheckbox.Checked;
        }

        private void smartLeftClick_CheckedChanged(object sender, EventArgs e)
        {
            _editorManager.SmartInsert = smartLeftClick.Checked;
        }

        private void listEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            _editorManager.CurrentItem = _editorManager.GetEditorItemByName(listEntities.SelectedItems[0].Text);
        }

        private void playInEditorButton_Click(object sender, EventArgs e)
        {
            _editorManager.PlayLevel = !_editorManager.PlayLevel;
            TileMap.EditorMode = !_editorManager.PlayLevel;
            playInEditorButton.Text = _editorManager.PlayLevel ? "Return To Edit-Mode" : "Play Map in Editor";
            bool state = TileMap.EditorMode;
            listTiles.Enabled = state;
            listEntities.Enabled = state;
            mapPropertiesGroupBox.Enabled = state;
            editModeItemCheckBox.Enabled = state;
            groupBox3.Enabled = state;
            leftClickGroupBox.Enabled = state;
            groupBox2.Enabled = state;
            layerSelectGroupBox.Enabled = state;
            groupBoxRightClick.Enabled = state;
            focusButton.Focus();
        }

        private void GenerateRandomMapToolStripMenuItemClick(object sender, EventArgs e)
        {
            _updateMapSize = false;
            GameVariableProvider.MapGenerator.GenerateNewMap();
        }
    }
}