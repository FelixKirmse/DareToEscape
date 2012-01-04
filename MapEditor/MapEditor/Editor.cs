using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using BlackDragonEngine.TileEngine;
using DareToEscape;

namespace MapEditor
{
    public partial class Editor : Form
    {
        private readonly string _mapPath = Application.StartupPath +
                                           @"/../../../../../DareToEscape/DareToEscapeContent/";

        private const int ScaleFactor = 1;
        private const int TileSize = 8;
        private Rectangle _marker;
        private bool _drawMarker;
        private int Tile { get { return ScaleFactor*TileSize; } }
        private string _currentMapName;
        private bool _firstTime = true;

        public Editor()
        {
            InitializeComponent();
            _treeView.Nodes.Add("maps");
            PopulateTree(_mapPath + "maps/", _treeView.Nodes[0]);
            var image = Image.FromFile(Application.StartupPath + @"/Content/textures/tilesheets/tilesheet.png");
            _tileSheetBox.Image = ResizeImage(image, image.Width * ScaleFactor, image.Height * ScaleFactor);
            _marker = new Rectangle(0, 0, Tile, Tile);
            tickTimer.Start();
        }

        public MapEditor Game { private get; set; }

        public PictureBox PctSurface
        {
            get { return _pctSurface; }
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
           
            using (var graphics = Graphics.FromImage(result))
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
            using(var pen = new Pen(Color.Red, 1))
            {
                e.Graphics.DrawRectangle(pen, _marker);
            }
        }

        private void TileSheetBoxMouseClick(object sender, MouseEventArgs e)
        {
            int tilesPerRow = _tileSheetBox.Image.Width/Tile;
            int tileIndex = (e.X/Tile) + ((e.Y/Tile)*tilesPerRow);
            Game.CurrentItem = Item.GetItemByTileId(tileIndex);
            _marker = new Rectangle((tileIndex%tilesPerRow) * Tile, (tileIndex / tilesPerRow) * Tile, Tile, Tile);
            _drawMarker = true;
        }

        private void TickTimerTick(object sender, EventArgs e)
        {
            Game.Tick();
            _tileSheetBox.Refresh();
        }

        private void TreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!e.Node.Text.Contains(".map")) return;
            if(!_firstTime)
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
    }
}