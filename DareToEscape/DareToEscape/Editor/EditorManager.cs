using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.TileEngine;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using DareToEscape.Managers;
using BlackDragonEngine.Managers;

namespace DareToEscape.Editor
{
    static class EditorManager
    {
        private static IntPtr drawSurface;
        private static Form parentForm;
        private static PictureBox pictureBox;
        private static Control gameForm;
        private static VScrollBar vscroll;
        private static HScrollBar hscroll;
        private static MapEditor editorForm;

        public static int DrawLayer = 0;
        public static int DrawTile = 0;
        public static bool SettingCode = false;
        public static bool MakeUnpassable = false;
        public static bool MakePassable = true;
        public static bool GettingCode = false;
        public static bool InsertTile = false;
        public static Vector2 CellCoords = Vector2.Zero;

        public static string FillMode = "TILEFILL";

        public static bool WaitingForSecondClick = false;

        private static Vector2 startCell;

        private static PresentationParameters orgPParams;
        private static DareToEscape game;

        private static EventHandler gameFormVisibleChanged;
        private static EventHandler pictureBoxSizeChanged;
        private static EventHandler<PreparingDeviceSettingsEventArgs> preparingDeviceSettingsHandler;
        private static EventHandler<PreparingDeviceSettingsEventArgs> resetDeviceSettingsHandler;

        public static void Initialize()
        {
            gameFormVisibleChanged = new EventHandler(gameForm_VisibleChanged);
            pictureBoxSizeChanged = new EventHandler(pictureBox_SizeChanged);
            preparingDeviceSettingsHandler = new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
            resetDeviceSettingsHandler = new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_resetDeviceSettings);
            orgPParams = DareToEscape.Graphics.GraphicsDevice.PresentationParameters.Clone();
            DareToEscape.Graphics.PreparingDeviceSettings += resetDeviceSettingsHandler;            
        }

        public static void Activate()
        {            
            StateManager.GameState = GameStates.Editor;
            editorForm = new MapEditor();            
            editorForm.Show();
            drawSurface = editorForm.pctSurface.Handle;
            parentForm = editorForm;
            pictureBox = editorForm.pctSurface;
            game = (DareToEscape)VariableProvider.Game;
            editorForm.Game = game;
            
            DareToEscape.Graphics.PreparingDeviceSettings -= resetDeviceSettingsHandler;
            DareToEscape.Graphics.PreparingDeviceSettings += preparingDeviceSettingsHandler;
            DareToEscape.Graphics.GraphicsDevice.Reset();      

            gameForm = Control.FromHandle(game.Window.Handle);
            gameForm.VisibleChanged += gameFormVisibleChanged;
            pictureBox.SizeChanged += pictureBoxSizeChanged;
            gameForm.Hide();

            vscroll = (VScrollBar)parentForm.Controls["vScrollBar1"];
            hscroll = (HScrollBar)parentForm.Controls["hScrollBar1"];

            Camera.ViewPortWidth = pictureBox.Width;
            Camera.ViewPortHeight = pictureBox.Height;
            Camera.UpdateWorldRectangle();
            TileMap.spriteFont = FontProvider.GetFont("Mono14");
            pictureBox_SizeChanged(null, null);
        }

        private static void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = drawSurface;
        }

        private static void graphics_resetDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters = orgPParams.Clone();
        }

        public static void Deactivate()
        {
            gameForm.VisibleChanged -= gameFormVisibleChanged;
            pictureBox.SizeChanged -= pictureBoxSizeChanged;
            DareToEscape.Graphics.PreparingDeviceSettings -= preparingDeviceSettingsHandler;
            DareToEscape.Graphics.PreparingDeviceSettings += resetDeviceSettingsHandler;
            DareToEscape.Graphics.GraphicsDevice.Reset();
            DareToEscape.Graphics.PreferredBackBufferWidth = 800;
            DareToEscape.Graphics.PreferredBackBufferHeight = 600;
            Camera.ViewPortWidth = 800;
            Camera.ViewPortHeight = 600;
            DareToEscape.Graphics.ApplyChanges();
            StateManager.GameState = GameStates.Menu;            
            editorForm.Hide();
            gameForm.Show();
        }

        public static void JumpToLevel(string levelName)
        {
            Deactivate();
            StateManager.GameState = GameStates.Ingame;
            LevelManager.LoadLevel(levelName);
        }

        private static void gameForm_VisibleChanged(object sender, EventArgs e)
        {
            if (gameForm.Visible) gameForm.Visible = false;
        }

        private static void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            if (parentForm.WindowState != FormWindowState.Minimized)
            {
                DareToEscape.Graphics.PreferredBackBufferWidth = pictureBox.Width;
                DareToEscape.Graphics.PreferredBackBufferHeight = pictureBox.Height;
                Camera.ViewPortWidth = pictureBox.Width;
                Camera.ViewPortHeight = pictureBox.Height;
                DareToEscape.Graphics.ApplyChanges();
            }
        }        

        public static void Update()
        { 
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(FontProvider.GetFont("Mono14"), "WTFBBQ", ShortcutProvider.ScreenCenter, Color.Red);
        }
    }
}
