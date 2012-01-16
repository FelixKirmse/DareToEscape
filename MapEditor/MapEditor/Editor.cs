using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using BlackDragonEngine.Helpers;
using DareToEscape.Managers;

namespace MapEditor
{
    public partial class Editor : Form
    {
        private const int ScaleFactor = 1;
        private const int TileSize = 8;

        private const string ConfigFile = "editorConfig.cfg";

        private string _currentMapName;
        private bool _doNothing;
        private bool _drawMarker;
        private bool _firstTime = true;
        private string _mapPath; //Path to DareToEscapeContent
        private Rectangle _marker;
        private string _oldLabel;

        public Editor()
        {
            InitializeComponent();
            LoadPath();
            _mapPath += "/";
            _treeView.Nodes.Add("maps");
            PopulateTree(_mapPath + "maps/", _treeView.Nodes[0]);
            Image image = Image.FromFile(Application.StartupPath + @"/Content/textures/tilesheets/tilesheet.png");
            _tileSheetBox.Image = ResizeImage(image, image.Width*ScaleFactor, image.Height*ScaleFactor);
            _marker = new Rectangle(0, 0, Tile, Tile);
            tickTimer.Start();
            LoadEntities();
            LoadCodes();
        }

        private int Tile
        {
            get { return ScaleFactor*TileSize; }
        }

        public MapEditor Game { private get; set; }

        public PictureBox PctSurface
        {
            get { return _pctSurface; }
        }

        private void LoadPath()
        {
            if (File.Exists(Application.StartupPath + "/" + ConfigFile))
            {
                try
                {
                    using (var sr = new StreamReader(Application.StartupPath + "/" + ConfigFile))
                    {
                        _mapPath = sr.ReadLine();
                        if (!Directory.Exists(_mapPath + "/maps"))
                            SetNewMapPath();
                    }
                }
                catch (Exception)
                {
                    SetNewMapPath();
                }
            }
            else
            {
                SetNewMapPath();
            }
        }

        private void SetNewMapPath()
        {
            _mapSelectorDialog.ShowDialog();
            _mapPath = _mapSelectorDialog.SelectedPath;
            if (!Directory.Exists(_mapPath + "/maps"))
            {
                SetNewMapPath();
                return;
            }
            using (var sw = new StreamWriter(Application.StartupPath + "/" + ConfigFile))
            {
                sw.WriteLine(_mapPath);
            }
        }

        private void LoadEntities()
        {
            _entitiesList.Items.Add("Player");
            _entitiesList.Items.Add("Small Turret");
            _entitiesList.Items.Add("Medium Turret");
            _entitiesList.Items.Add("Boss 1 (Turret)");
            _entitiesList.Items.Add("Exit");
            _entitiesList.Items.Add("Checkpoint");
        }

        private void LoadCodes()
        {
            _codesList.Items.Add("Bosstrigger");
        }

        private void PopulateTree(string dir, TreeNode node)
        {
            var directory = new DirectoryInfo(dir);
            foreach (var d in directory.GetDirectories())
            {
                var t = new TreeNode(d.Name);
                PopulateTree(d.FullName, t);
                node.Nodes.Add(t);
            }
            foreach (var f in directory.GetFiles())
            {
                if (f.Extension != ".map") continue;
                var t = new TreeNode(f.Name);
                node.Nodes.Add(t);
            }
        }

        public Image ResizeImage(Image image, int width, int height)
        {
            var result = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.CompositingQuality = CompositingQuality.AssumeLinear;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.DrawImage(image, 0, 0, result.Width, result.Height);
            }
            return result;
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
            Game.Exit();
        }

        private void EditorFormClosing(object sender, FormClosingEventArgs e)
        {
            ExitToolStripMenuItemClick(null, null);
        }

        private void TileSheetBoxPaint(object sender, PaintEventArgs e)
        {
            if (!_drawMarker) return;
            using (var pen = new Pen(Color.Red, 1))
            {
                e.Graphics.DrawRectangle(pen, _marker);
            }
        }

        private void TileSheetBoxMouseClick(object sender, MouseEventArgs e)
        {
            int tilesPerRow = _tileSheetBox.Image.Width/Tile;
            int tileIndex = (e.X/Tile) + ((e.Y/Tile)*tilesPerRow);
            _tileIndexLabel.Text = string.Format("Selected Index: {0}", tileIndex);
            Game.CurrentItem = Item.GetItemByTileId(tileIndex);
            _marker = new Rectangle((tileIndex%tilesPerRow)*Tile, (tileIndex/tilesPerRow)*Tile, Tile, Tile);
            _drawMarker = true;
            _doNothing = true;
            try
            {
                _entitiesList.SelectedItems[0].Selected = false;
                _codesList.SelectedItems[0].Selected = false;
            }
            catch (Exception)
            {
            }
            finally
            {
                _doNothing = false;
            }
        }

        private void TickTimerTick(object sender, EventArgs e)
        {
            Game.Tick();
            _tileSheetBox.Refresh();
        }

        private void TreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!e.Node.Text.Contains(".map")) return;
            if (!_firstTime)
            {
                Game.SaveMap(_currentMapName);
            }
            else
            {
                _firstTime = false;
            }
            Game.LoadMap(_mapPath + e.Node.FullPath);
            _currentMapName = _mapPath + e.Node.FullPath;
        }

        private void EnableEditorViewToolStripMenuItemClick(object sender, EventArgs e)
        {
            Game.TileMap.EditorMode = enableEditorViewToolStripMenuItem.Checked;
        }

        private void CodesListSelectedIndexChanged(object sender, EventArgs e)
        {
            if (_doNothing) return;
            _drawMarker = false;
            _doNothing = true;
            try
            {
                _entitiesList.SelectedItems[0].Selected = false;
            }
            catch(Exception)
            {
            }
            finally
            {
                _doNothing = false;
                Game.CurrentItem = Item.GetItemByCodeId(_codesList.SelectedIndices[0]);
            }
        }

        private void EntitiesListSelectedIndexChanged(object sender, EventArgs e)
        {
            if (_doNothing) return;
            _drawMarker = false;
            _doNothing = true;
            try
            {
                _codesList.SelectedItems[0].Selected = false;
            }
            catch (Exception)
            {
            }
            finally
            {
                _doNothing = false;
                Game.CurrentItem = Item.GetItemByEntityId(_entitiesList.SelectedIndices[0]);
            }
        }

        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            Game.SaveMap(_currentMapName);
        }

        private void TreeViewAfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (File.Exists(_mapPath + _oldLabel))
            {
                File.Move(_mapPath + _oldLabel, _mapPath + e.Node.Parent.FullPath + "/" + e.Label);
                _currentMapName = _mapPath + e.Node.Parent.FullPath + "/" + e.Label;
            }
            else
            {
                Directory.Move(_mapPath + _oldLabel, _mapPath + e.Node.Parent.FullPath + "/" + e.Label);
            }
        }

        private void TreeViewBeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.FullPath == "maps")
            {
                e.CancelEdit = true;
                return;
            }
            _oldLabel = e.Node.FullPath;
        }

        private void RenameToolStripMenuItem1Click(object sender, EventArgs e)
        {
            _treeView.SelectedNode.BeginEdit();
        }

        private void NewFileToolStripMenuItemClick(object sender, EventArgs e)
        {
            TreeNode node = _treeView.SelectedNode.Text.Contains(".map")
                                ? _treeView.SelectedNode.Parent
                                : _treeView.SelectedNode;
            TreeNode newNode = node.Nodes.Add("New Map.map");
            File.Create(_mapPath + node.FullPath + "/New Map.map");
            newNode.BeginEdit();
        }

        private void NewFolderToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_treeView.SelectedNode.Text == "maps") return;
            TreeNode node = _treeView.SelectedNode.Parent;
            TreeNode newNode = node.Nodes.Add("New Folder");
            Directory.CreateDirectory(_mapPath + node.FullPath + "/New Folder");
            newNode.BeginEdit();
        }

        private void DeleteToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_treeView.SelectedNode.Text == "maps") return;
            TreeNode node = _treeView.SelectedNode;
            if (node.Text.Contains(".map"))
            {
                File.Delete(_mapPath + node.FullPath);
            }
            else
            {
                Directory.Delete(_mapPath + node.FullPath, true);
            }
            node.Remove();
            _currentMapName = null;
        }

        private void PlayButtonClick(object sender, EventArgs e)
        {
            Game.Playing = !Game.Playing;
            if (Game.Playing)
            {
                _focusTextbox.Focus();
                _focusTextbox.LostFocus += RefocusInputBox;
                Camera.UpdateWorldRectangle(Game.TileMap);
            }
            else
            {
                _focusTextbox.LostFocus -= RefocusInputBox;
                BulletManager.GetInstance().ClearAllBullets();
            }
        }

        private void RefocusInputBox(object sender, EventArgs e)
        {
            _focusTextbox.Focus();
            _focusTextbox.Clear();
        }
    }
}