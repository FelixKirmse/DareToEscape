using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using System.IO;
using BlackDragonEngine.HelpMaps;
using Microsoft.Xna.Framework.Input;
using BlackDragonEngine.Helpers;
using XNARectangle = Microsoft.Xna.Framework.Rectangle;
using xTile;

namespace CodeEditor
{
    public partial class EditorForm : Form
    {
        public Editor Editor;        
        private string cwd;
        private string mapPath;
        private string editorContentPath;

        private string currentMap;

        public EditorForm()
        {           
            InitializeComponent();
        }

        private void Exit(object sender, EventArgs e)
        {
            Editor.Exit();
            Application.Exit();
        }

        private void gameUpdate_Tick(object sender, EventArgs e)
        {
            FixScrollBarScales();

            if (Form.ActiveForm == this)
                Editor.Tick();
        }

        public void FixScrollBarScales()
        {
            Camera.WorldRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, Editor.CurrentMap.DisplayWidth, Editor.CurrentMap.DisplayHeight);
            Camera.ViewPortWidth = editorOutput.Width;
            Camera.ViewPortHeight = editorOutput.Height;

            Editor.Viewport.Height = editorOutput.Height;
            Editor.Viewport.Width = editorOutput.Width;
                        
            vScrollBar1.Minimum = 0;
            vScrollBar1.Maximum = Camera.WorldRectangle.Height - Editor.Viewport.Height;

            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = Camera.WorldRectangle.Width - Editor.Viewport.Width;
        }

        private void EditorForm_Load(object sender, EventArgs e)
        {            
            
        }

        private void EditorForm_Shown(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader(Application.StartupPath + @"\config.cfg");
                cwd = sr.ReadLine();
                editorContentPath = sr.ReadLine();
                sr.Close();
            }
            catch
            {
                folderBrowser.Description = @"Select Game's Map Directory";
                folderBrowser.ShowDialog();
                cwd = folderBrowser.SelectedPath;

                folderBrowser.Description = @"Select the ContentFolder of the Editor";
                folderBrowser.ShowDialog();
                editorContentPath = folderBrowser.SelectedPath;

                StreamWriter sw = new StreamWriter(Application.StartupPath + @"\config.cfg");
                sw.WriteLine(cwd);
                sw.WriteLine(editorContentPath);
                sw.Close();
            }
            while (cwd == "")
            {
                folderBrowser.Description = @"Select Game's Map Directory";
                folderBrowser.ShowDialog();
                cwd = folderBrowser.SelectedPath;

                folderBrowser.Description = @"Select the ContentFolder of the Editor";
                folderBrowser.ShowDialog();
                editorContentPath = folderBrowser.SelectedPath;

                StreamWriter sw = new StreamWriter(Application.StartupPath + @"\config.cfg");
                sw.WriteLine(cwd);
                sw.WriteLine(editorContentPath);
                sw.Close();
            }
            
            CopyContentFiles();
            openFileDialog.Title = "Select Map to Load";
            openFileDialog.ShowDialog();
        }

        private void CopyContentFiles()
        {                       
            string sourceTextures = cwd + @"\textures\TileSets";
            string sourceMaps = cwd + @"\maps";
            mapPath = sourceMaps;
            openFileDialog.InitialDirectory = mapPath;            
        }       

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            currentMap = openFileDialog.SafeFileName.Split('.')[0];
            Editor.CurrentMap = Editor.Content.Load<Map>(@"maps/" + currentMap);
            Editor.CurrentMap.LoadTileSheets(Editor.DisplayDevice);

            int editorLayerInt = Editor.CurrentMap.Properties["PlayerLayer"];
            xTile.Layers.Layer editorLayer = Editor.CurrentMap.Layers[editorLayerInt];
            TileMap.TileHeight = Editor.CurrentMap.Properties["TileSize"];
            TileMap.TileWidth = Editor.CurrentMap.Properties["TileSize"];
            TileMap.MapHeight = editorLayer.TileHeight / TileMap.TileHeight * editorLayer.LayerHeight;
            TileMap.MapWidth = editorLayer.TileWidth / TileMap.TileWidth * editorLayer.LayerWidth;

            Camera.UpdateWorldRectangle();
            try
            {
                TileMap.LoadMap(new FileStream(mapPath + @"/" + currentMap + ".map", FileMode.Open), true);
            }
            catch
            {
                TileMap.ClearMap();
            }
            gameUpdate.Start();
        }

        private void newWorkingDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowser.Description = @"Select Game's Content Directory";
            folderBrowser.ShowDialog();
            cwd = folderBrowser.SelectedPath;

            folderBrowser.Description = @"Select the ContentFolder of the Editor";
            folderBrowser.ShowDialog();
            editorContentPath = folderBrowser.SelectedPath;

            StreamWriter sw = new StreamWriter(Application.StartupPath + @"\config.cfg");
            sw.WriteLine(cwd);
            sw.WriteLine(editorContentPath);
            sw.Close();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void contentCopyButton_Click(object sender, EventArgs e)
        {
            TileMap.ClearMap();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TileMap.SaveMap(new FileStream(mapPath + @"/" + currentMap + ".map", FileMode.Create), true);
        }

        private void rectangleSelector_CheckedChanged(object sender, EventArgs e)
        {
            Editor.RectangleMode = rectangleSelector.Checked;
            getCodeRadioButton.Enabled = !rectangleSelector.Checked;
            setCodeRadio.Checked = true;
        }

        private void showStuff_CheckedChanged(object sender, EventArgs e)
        {
            Editor.DrawStuff = showStuff.Checked;
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
            List<string> codeList = new List<string>();
            foreach (string code in codeListBox.Items)
            {
                codeList.Add(code);
            }

            MapSquare square = TileMap.GetMapSquareAtCell(cellX, cellY);
            square.Codes = codeList;
        }

        private void setPassableRadio_CheckedChanged(object sender, EventArgs e)
        {
            Editor.Passable = setPassableRadio.Checked;
        }

        private void setCodeRadio_CheckedChanged(object sender, EventArgs e)
        {
            Editor.SetCode = setCodeRadio.Checked;
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
    }
}
