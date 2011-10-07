using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.IO;
using BlackDragonEngine.TileEngine;
using BlackDragonEngine.Helpers;
using Microsoft.Xna.Framework.Input;

using XNARectangle = Microsoft.Xna.Framework.Rectangle;

namespace DareToEscape.Editor
{
    
    public partial class MapEditor : Form
    {
        public DareToEscape Game;
        private string cwd;       
        private string currentMapName;        

        private string loadLevel;

        public MapEditor()
        {
            InitializeComponent();            
        }

        public MapEditor(string levelName)
        {
            InitializeComponent();
            loadLevel = levelName;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorManager.Deactivate();            
        }

        private void LoadImageLists()
        {
            LoadImageLists(false);
        }

        private void LoadImageLists(bool createImages)
        {            
            string filepath;            
            
            filepath = Application.StartupPath + @"\Content\textures\tilesheets\tilesheet.png";
            tileList.Images.Clear();            
            
            Bitmap tileSheet = new Bitmap(filepath);
            int tilecount = 0;
            for (int y = 0; y < tileSheet.Height / (TileMap.TileHeight + TileMap.TileOffset); ++y)
            {
                for (int x = 0; x < tileSheet.Width / (TileMap.TileWidth + TileMap.TileOffset); ++x)
                {

                    if (!createImages)
                    {
                        tileList.Images.Add(new Bitmap(Application.StartupPath + "/Content/textures/editor/tiles/" + tilecount.ToString() + ".bmp"));
                    }
                    else
                    {
                        Bitmap newBitmap = tileSheet.Clone(new System.Drawing.Rectangle(x * (TileMap.TileWidth + TileMap.TileOffset), y * (TileMap.TileHeight + TileMap.TileOffset), TileMap.TileWidth, TileMap.TileHeight), System.Drawing.Imaging.PixelFormat.DontCare);                                                
                        tileList.Images.Add(newBitmap);
                        newBitmap.Save(Application.StartupPath + "/Content/textures/editor/tiles/" + tilecount.ToString() + ".bmp");  
                    }
                    ++tilecount;
                }
            }
                        
            listTiles.SmallImageList = tileList;                  
            tilecount = 0;
            listTiles.Clear();

            for (int y = 0; y < tileSheet.Height / (TileMap.TileHeight + TileMap.TileOffset); ++y)
            {
                for (int x = 0; x < tileSheet.Width / (TileMap.TileWidth + TileMap.TileOffset); ++x)
                {
                    string itemName = tilecount.ToString();
                    listTiles.Items.Add(new ListViewItem(itemName, tilecount++));
                }
            }

            
            tilecount = 0;
            listEntities.Clear();
            DirectoryInfo dirInfo = new DirectoryInfo(Application.StartupPath + @"\Content\textures\editor\entities");
            var files = dirInfo.GetFiles();
            tilecount = 0;
            foreach (var file in files)
            {
                entityList.Images.Add(new Bitmap(file.FullName));
                listEntities.Items.Add(new ListViewItem(file.Name.Replace(".bmp", ""), tilecount++));
            }
            listEntities.SmallImageList = entityList;        
        }

        private void FixScrollBarScales()
        {
            Camera.WorldRectangle = new XNARectangle(0, 0, TileMap.TileWidth * TileMap.MapWidth, TileMap.TileHeight * TileMap.MapHeight);
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
                EditorManager.DrawTile = listTiles.SelectedIndices[0];
                EditorManager.CurrentItem = EditorManager.GetEditorItemByName(listTiles.SelectedIndices[0].ToString());      
            }
        }        


        private void radioPassable_CheckedChanged(object sender, EventArgs e)
        {
            if (radioPassable.Checked){
                EditorManager.SettingCode = false;
                EditorManager.MakePassable = true;
                EditorManager.MakeUnpassable = false;
                EditorManager.GettingCode = false;
            }
        }        

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorManager.DrawLayer = 0;
            backgroundToolStripMenuItem.Checked = true;
            interactiveToolStripMenuItem.Checked = false;
            foregroundToolStripMenuItem.Checked = false;
        }

        private void interactiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorManager.DrawLayer = 1;
            backgroundToolStripMenuItem.Checked = false;
            interactiveToolStripMenuItem.Checked = true;
            foregroundToolStripMenuItem.Checked = false;
        }

        private void foregroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorManager.DrawLayer = 2;
            backgroundToolStripMenuItem.Checked = false;
            interactiveToolStripMenuItem.Checked = false;
            foregroundToolStripMenuItem.Checked = true;
        }

        private void timerGameUpdate_Tick(object sender, EventArgs e)
        {
            FixScrollBarScales();
            
            if(Form.ActiveForm == this)
            Game.Tick();

            mapSizeLabel.Text = "Map size: " + TileMap.MapWidth + " x " + TileMap.MapHeight;
            coordLbl.Text = "MapCell: (" + EditorManager.CellCoords.X + @"|" + EditorManager.CellCoords.Y + ")";
            
        }

        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();            
            try 
            {
                TileMap.LoadMap(new FileStream(cwd + @"/" + currentMapName, FileMode.Open));                
            }
            catch 
            {
                System.Diagnostics.Debug.Print("Unable to load map file");
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
            EditorManager.Deactivate();
        }

        private void radioUnpassable_CheckedChanged(object sender, EventArgs e)
        {
            if (radioUnpassable.Checked)
            {
                EditorManager.SettingCode = false;
                EditorManager.MakePassable = false;
                EditorManager.MakeUnpassable = true;
                EditorManager.GettingCode = false;
            }            
        }

        private void radioCode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCode.Checked)
            {
                EditorManager.SettingCode = true;
                EditorManager.MakePassable = false;
                EditorManager.MakeUnpassable = false;
                EditorManager.GettingCode = false;
            }            
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowser.ShowDialog();
            cwd = folderBrowser.SelectedPath;
            cwdLabel.Text = "Current Working Directory: " + cwd;
            StreamWriter sw = new StreamWriter(Application.StartupPath + @"\config.cfg");
            
            sw.WriteLine(cwd);
            sw.Close();
        }

        private void MapEditor_Shown(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader(Application.StartupPath + @"\config.cfg");
                cwd = sr.ReadLine();
                sr.Close();
                cwdLabel.Text = cwd;
            }
            catch {
                folderBrowser.ShowDialog();
                cwd = folderBrowser.SelectedPath;
                cwdLabel.Text = cwd;
                StreamWriter sw = new StreamWriter(Application.StartupPath + @"\config.cfg");                
                sw.WriteLine(cwd);
                sw.Close();
            }
            if (cwd == "")
            {
                folderBrowser.ShowDialog();
                cwd = folderBrowser.SelectedPath;
                cwdLabel.Text = cwd; 
                StreamWriter sw = new StreamWriter(Application.StartupPath + @"\config.cfg");                
                sw.WriteLine(cwd);
                sw.Close();
            }  
            timerGameUpdate.Start();
            openFileDialog.InitialDirectory = cwd;
            saveFileDialog.InitialDirectory = cwd;

            if (loadLevel != null)
            {
                TileMap.LoadMap(new FileStream(cwd + @"/" + loadLevel + ".map", FileMode.Open));
                currentMapName = loadLevel + ".map";                        
            }
        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            EditorManager.JumpToLevel(currentMapName.Replace(".map",""));
        }  
  
        private void backgroundRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (backgroundRadioButton.Checked)
            {
                EditorManager.DrawLayer = 0;
                backgroundToolStripMenuItem.Checked = true;
                interactiveToolStripMenuItem.Checked = false;
                foregroundToolStripMenuItem.Checked = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (interactiveRadioButton.Checked)
            {
                EditorManager.DrawLayer = 1;
                backgroundToolStripMenuItem.Checked = false;
                interactiveToolStripMenuItem.Checked = true;
                foregroundToolStripMenuItem.Checked = false;
            }
        }

        private void foregroundRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (foregroundRadioButton.Checked)
            {
                EditorManager.DrawLayer = 2;
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
                EditorManager.FillMode = "RECTANGLEFILL";
                getCodeRadio.Enabled = false;                
            }
            else
            {
                EditorManager.FillMode = "TILEFILL";
                getCodeRadio.Enabled = true;
            }            
        }

        private void getCodeRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (getCodeRadio.Checked)
            {
                EditorManager.SettingCode = false;
                EditorManager.MakePassable = false;
                EditorManager.MakeUnpassable = false;
                EditorManager.GettingCode = true;
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

        public void SetCodeList(int cellX,int cellY)
        {
            List<string> codeList = new List<string>();             
            foreach (string code in codeListBox.Items)
            {
                codeList.Add(code);
            }

            MapSquare square = TileMap.GetMapSquareAtCell(cellX, cellY);
            if (square == null)
                square = new MapSquare(null, null, null, true);
            square.Codes = codeList;
            TileMap.SetMapSquareAtCell(cellX, cellY, square);
        }

        private void addCodeButton_Click(object sender, EventArgs e)
        {
            if(addCodeInput.Text != "")
                codeListBox.Items.Add(addCodeInput.Text);
            addCodeInput.Text = "";
        }

        private void removeCodesButton_Click(object sender, EventArgs e)
        {
            string[] items = new string[codeListBox.SelectedItems.Count];
            for (int i = 0; i < codeListBox.SelectedItems.Count; ++i)
            {
                items[i] = (string)codeListBox.SelectedItems[i];
            }
            foreach (string item in items)
            {
                codeListBox.Items.Remove(item);
            }
        }

        private void addCodeInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                addCodeButton_Click(sender, e);
            }
        }

        private void codeListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Delete))
            {
                removeCodesButton_Click(sender, e);
            }
        }

        private void insertTileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EditorManager.InsertTile = insertTileCheckBox.Checked;
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            currentMapName = openFileDialog.SafeFileName;
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            string[] filePathArray = saveFileDialog.FileName.Split('\\');
            currentMapName = filePathArray[filePathArray.Length - 1];
        }

        private void saveMapToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMapName))
                saveFileDialog.ShowDialog();
            if(!string.IsNullOrEmpty(currentMapName))
            SaveMap();
        }

        private void SaveMap()
        {
            TileMap.SaveMap(new FileStream(cwd + @"/" + currentMapName, FileMode.Create));
        }

        private void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TileMap.ClearMap();
            currentMapName = "";
        }

        private void deleteCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            EditorManager.RemoveTile = deleteCheckbox.Checked;
        }

        private void smartLeftClick_CheckedChanged(object sender, EventArgs e)
        {
            EditorManager.SmartInsert = smartLeftClick.Checked;
        }

        private void listEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            EditorManager.CurrentItem = EditorManager.GetEditorItemByName(listEntities.SelectedItems[0].Text);
        }        

        private void playInEditorButton_Click(object sender, EventArgs e)
        {
            EditorManager.PlayLevel = !EditorManager.PlayLevel;
            TileMap.EditorMode = !EditorManager.PlayLevel;
            playInEditorButton.Text = EditorManager.PlayLevel ? "Return To Edit-Mode" : "Play Map in Editor";            
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
    }
}
