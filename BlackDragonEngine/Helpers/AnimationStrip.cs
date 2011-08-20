using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Providers;

namespace BlackDragonEngine.Helpers
{

    public class AnimationStrip
    {
        #region Declarations
        private Texture2D texture;
        private int frameWidth;
        private int frameHeight;

        private float frameTimer = 0f;
        private float frameDelay = 0.05f;

        private int currentFrame;

        private bool loopAnimation = true;
        private bool finishedPlaying = false;

        private string name;
        private string nextAnimation;
        #endregion

        #region Properties
        public int FrameWidth {
            get { return frameWidth; }
            set { frameWidth = value; }
        }

        public int FrameHeight {
            get { return frameHeight; }
            set { frameHeight = value; }
        }

        public Texture2D Texture {
            get { return texture; }
            set { texture = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public string NextAnimation {
            get { return nextAnimation; }
            set { nextAnimation = value; }
        }

        public bool LoopAnimation {
            get { return loopAnimation; }
            set { loopAnimation = value; }
        }

        public bool FinishedPlaying {
            get { return finishedPlaying; }
        }

        public int FrameCount {
            get { return texture.Width / frameWidth; }
        }

        public float FrameLength {
            get { return frameDelay; }
            set { frameDelay = value; }
        }

        public Rectangle FrameRectangle {
            get { return new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight); }            
        }
        #endregion

        #region Constructor
        public AnimationStrip(Texture2D texture, int frameWidth, string name, bool loop)
            :this(texture, frameWidth, name)
        {
            loopAnimation = loop;
        }

        public AnimationStrip(Texture2D texture, int frameWidth, string name, bool loop, float frameDelay)
            : this(texture, frameWidth, name, loop)
        {
            this.frameDelay = frameDelay;
        }

        public AnimationStrip(Texture2D texture, int frameWidth, string name) {
            this.texture = texture;
            this.frameWidth = frameWidth;
            this.frameHeight = texture.Height;
            this.name = name;
        }
        #endregion

        #region Public Methods
        public void Play() {
            currentFrame = 0;
            finishedPlaying = false;
        }

        public void Update()
        {
            float elapsed = ShortcutProvider.ElapsedSeconds;
            frameTimer += elapsed;
            if (frameTimer >= frameDelay) {
                ++currentFrame;
                if (currentFrame >= FrameCount) {
                    if (loopAnimation) {
                        currentFrame = 0;
                    }
                    else {
                        currentFrame = FrameCount - 1;
                        finishedPlaying = true;
                    }
                }
                frameTimer = 0f;
            }
        }
        #endregion
    }
}
