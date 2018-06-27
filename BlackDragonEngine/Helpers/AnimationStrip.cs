using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Helpers
{
    /// <summary>
    ///     Class that helps handling Animations
    /// </summary>
    public sealed class AnimationStrip
    {
        #region Declarations

        private readonly int _framesPerRow;
        private int _currentFrame;
        private float _frameTimer;

        private readonly Rectangle _stripRect;

        #endregion

        #region Properties

        public int FrameWidth { get; set; }

        public int FrameHeight { get; set; }

        public Texture2D Texture { get; set; }

        public string Name { get; set; }

        public string NextAnimation { get; set; }

        public bool LoopAnimation { get; set; }

        public bool FinishedPlaying { get; private set; }

        public int FrameCount { get; }


        public float FrameLength { get; set; }

        public Rectangle FrameRectangle => new Rectangle(_currentFrame % _framesPerRow * FrameWidth + _stripRect.X,
            _currentFrame / _framesPerRow * FrameHeight + _stripRect.Y, FrameWidth,
            FrameHeight);

        #endregion

        #region Constructor

        public AnimationStrip(Texture2D texture, int frameWidth, string name, bool loop)
            : this(texture, frameWidth, name)
        {
            LoopAnimation = loop;
        }

        public AnimationStrip(Texture2D texture, int frameWidth, string name, bool loop, float frameDelay)
            : this(texture, frameWidth, name, loop)
        {
            FrameLength = frameDelay;
        }

        public AnimationStrip(Texture2D texture, int frameWidth, string name)
        {
            Texture = texture;
            FrameWidth = frameWidth;
            FrameHeight = texture.Height;
            _framesPerRow = texture.Width / frameWidth;
            FrameCount = _framesPerRow;
            Name = name;
            _stripRect = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public AnimationStrip(Texture2D texture, Rectangle stripRect, int frameCount, string name, bool loop = true,
            float frameDelay = .05f)
        {
            Texture = texture;
            _stripRect = stripRect;
            FrameCount = frameCount;
            FrameHeight = stripRect.Height;
            FrameWidth = stripRect.Width / frameCount;
            _framesPerRow = frameCount;
            Name = name;
            LoopAnimation = loop;
            FrameLength = frameDelay;
        }

        public AnimationStrip(Texture2D texture, Rectangle stripRect, int frameWidth, int frameHeight, string name,
            bool loop = true, float frameDelay = .05f)
        {
            Texture = texture;
            _stripRect = stripRect;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            Name = name;
            LoopAnimation = loop;
            FrameLength = frameDelay;
            _framesPerRow = _stripRect.Width / frameWidth;
            FrameCount = _framesPerRow * (_stripRect.Height / frameHeight);
        }

        #endregion

        #region Public Methods

        public void Play()
        {
            _currentFrame = 0;
            FinishedPlaying = false;
        }

        public void Update()
        {
            var elapsed = ShortCuts.ElapsedSeconds;
            _frameTimer += elapsed;
            if (_frameTimer >= FrameLength)
            {
                ++_currentFrame;
                if (_currentFrame >= FrameCount)
                {
                    if (LoopAnimation)
                    {
                        _currentFrame = 0;
                    }
                    else
                    {
                        _currentFrame = FrameCount - 1;
                        FinishedPlaying = true;
                    }
                }

                _frameTimer = 0f;
            }
        }

        #endregion
    }
}