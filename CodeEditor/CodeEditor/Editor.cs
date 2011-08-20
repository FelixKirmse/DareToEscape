using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Windows.Forms;
using BlackDragonEngine.HelpMaps;
using BlackDragonEngine.Helpers;
using xTile;
using xTile.Display;
using xTile.Dimensions;
using xRectangle = xTile.Dimensions.Rectangle;
using XNARectangle = Microsoft.Xna.Framework.Rectangle;
using BlackDragonEngine.Providers;
using xKeys = Microsoft.Xna.Framework.Input.Keys;

namespace CodeEditor
{
    public class Editor : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        IntPtr drawSurface;
        Form parentForm;
        PictureBox pictureBox;
        Control gameForm;        
        VScrollBar vscroll;
        HScrollBar hscroll;

        public XnaDisplayDevice DisplayDevice;
        public xRectangle Viewport;
        public Map CurrentMap;

        public bool RectangleMode = false;
        private bool waitingForSecondClick;
        private Vector2 startCell;
        public bool DrawStuff = true;

        public bool Passable = true;
        public bool SetCode = true;

        public Editor(IntPtr drawSurface, Form parentForm, PictureBox surfacePictureBox)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.drawSurface = drawSurface;
            this.parentForm = parentForm;
            this.pictureBox = surfacePictureBox;

            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);

            Mouse.WindowHandle = drawSurface;

            gameForm = Control.FromHandle(this.Window.Handle);
            gameForm.VisibleChanged += new EventHandler(gameForm_VisibleChanged);
            pictureBox.SizeChanged += new EventHandler(pictureBox_SizeChanged);

            vscroll = (VScrollBar)parentForm.Controls["vScrollBar1"];
            hscroll = (HScrollBar)parentForm.Controls["hScrollBar1"];

            VariableProvider.Game = this;
        }

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = drawSurface;
        }

        private void gameForm_VisibleChanged(object sender, EventArgs e)
        {
            if (gameForm.Visible) gameForm.Visible = false;
        }

        void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            if (parentForm.WindowState != FormWindowState.Minimized)
            {
                graphics.PreferredBackBufferWidth = pictureBox.Width;
                graphics.PreferredBackBufferHeight = pictureBox.Height;
                Camera.ViewPortWidth = pictureBox.Width;
                Camera.ViewPortHeight = pictureBox.Height;
                Viewport.Width = pictureBox.Width;
                Viewport.Height = pictureBox.Height;
                graphics.ApplyChanges();
            }
        }

        protected override void Initialize()
        {            
            base.Initialize();
            pictureBox_SizeChanged(null, null);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            DisplayDevice = new XnaDisplayDevice(Content, GraphicsDevice);
            Viewport = new xRectangle(new Size(800, 600));
            TileMap.Initialize(Content.Load<Texture2D>(@"textures/white"), Content.Load<SpriteFont>(@"fonts/pericles8"));
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (Form.ActiveForm == parentForm)
            {
                if(CurrentMap != null)
                {
                    InputProvider.Update();
                    MouseState ms = InputProvider.MouseState;
                    if ((ms.X > 0) && (ms.Y > 0) && (ms.X < Camera.ViewPortWidth) && (ms.Y < Camera.ViewPortHeight))
                    {
                        Camera.Position = new Vector2(hscroll.Value, vscroll.Value);
                        
                        CurrentMap.Update(gameTime.ElapsedGameTime.Milliseconds);

                        if (ShortcutProvider.IsKeyDown(xKeys.W))
                            Camera.Position += new Vector2(0, -5);
                        if (ShortcutProvider.IsKeyDown(xKeys.A))
                            Camera.Position += new Vector2(-5, 0);
                        if (ShortcutProvider.IsKeyDown(xKeys.S))
                            Camera.Position += new Vector2(0, 5);
                        if (ShortcutProvider.IsKeyDown(xKeys.D))
                            Camera.Position += new Vector2(5, 0);
                        hscroll.Value = (int)Camera.Position.X;
                        vscroll.Value = (int)Camera.Position.Y;
                        Viewport.Location = new Location((int)Camera.Position.X, (int)Camera.Position.Y);

                        Vector2 mouseLoc = Camera.ScreenToWorld(new Vector2(ms.X, ms.Y));
                        int cellX = (int)MathHelper.Clamp(TileMap.GetCellByPixelX((int)mouseLoc.X), 0, TileMap.MapWidth - 1);
                        int cellY = (int)MathHelper.Clamp(TileMap.GetCellByPixelY((int)mouseLoc.Y), 0, TileMap.MapHeight - 1);

                        if (Camera.WorldRectangle.Contains((int)mouseLoc.X, (int)mouseLoc.Y))
                        {
                            if (!RectangleMode)
                            {
                                if (ShortcutProvider.LeftButtonClicked())
                                {
                                    TileMap.GetMapSquareAtCell(cellX, cellY).Passable = Passable;
                                }
                                if (ShortcutProvider.RightButtonClicked())
                                {
                                    if (SetCode)
                                    {
                                        ((EditorForm)parentForm).SetCodeList(cellX, cellY);
                                    }
                                    else
                                    {
                                        ((EditorForm)parentForm).GetCodeList(TileMap.GetCellCodes(cellX, cellY));
                                    }
                                }
                            }
                            else
                            {
                                if (ShortcutProvider.LeftButtonClickedNowButNotLastFrame())
                                {
                                    if (!waitingForSecondClick)
                                    {
                                        startCell = new Vector2(cellX, cellY);
                                        waitingForSecondClick = true;
                                    }
                                    else
                                    {
                                        Vector2 endCell = new Vector2(cellX, cellY);
                                        waitingForSecondClick = false;

                                        for (int cellx = (int)startCell.X; cellx <= endCell.X; ++cellx)
                                        {
                                            for (int celly = (int)startCell.Y; celly <= endCell.Y; ++celly)
                                            {
                                                TileMap.GetMapSquareAtCell(cellx, celly).Passable = Passable;
                                            }
                                        }
                                    }
                                }
                                else if (ShortcutProvider.RightButtonClickedButNotLastFrame())
                                {
                                    if (!waitingForSecondClick)
                                    {
                                        startCell = new Vector2(cellX, cellY);
                                        waitingForSecondClick = true;
                                    }
                                    else
                                    {
                                        Vector2 endCell = new Vector2(cellX, cellY);
                                        waitingForSecondClick = false;

                                        for (int cellx = (int)startCell.X; cellx <= endCell.X; ++cellx)
                                        {
                                            for (int celly = (int)startCell.Y; celly <= endCell.Y; ++celly)
                                            {
                                                if (SetCode)
                                                {
                                                    ((EditorForm)parentForm).SetCodeList(cellx, celly);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (CurrentMap != null)
            {
                CurrentMap.Draw(DisplayDevice, Viewport);


                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                if (((EditorForm)parentForm).showStuff.Checked)
                {
                    TileMap.Draw(spriteBatch);
                    if (waitingForSecondClick)
                    {
                        TileMap.DrawRectangleIndicator(spriteBatch, InputProvider.MouseState, startCell);
                    }
                }
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
