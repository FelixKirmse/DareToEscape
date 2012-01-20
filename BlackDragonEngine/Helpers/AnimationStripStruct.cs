using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Helpers
{
    /// <summary>
    ///   Struct that helps handling Animations, equivalent to AnimationStrip, but a struct
    /// </summary>
    public struct AnimationStripStruct
    {
        #region Declarations

        private readonly int _framesPerRow;
        private int _currentFrame;
        private bool _finishedPlaying;
        private float _frameDelay;
        private int _frameHeight;
        private float _frameTimer;
        private int _frameWidth;

        private bool _loopAnimation;
        private Rectangle _stripRect;

        #endregion

        #region Properties

        public int FrameWidth
        {
            get { return _frameWidth; }
            set { _frameWidth = value; }
        }

        public int FrameHeight
        {
            get { return _frameHeight; }
            set { _frameHeight = value; }
        }

        public Texture2D Texture { get; set; }

        public string Name { get; set; }

        public string NextAnimation { get; set; }

        public bool LoopAnimation
        {
            get { return _loopAnimation; }
            set { _loopAnimation = value; }
        }

        public bool FinishedPlaying
        {
            get { return _finishedPlaying; }
        }

        public int FrameCount { get; private set; }


        public float FrameLength
        {
            get { return _frameDelay; }
            set { _frameDelay = value; }
        }

        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle((_currentFrame%_framesPerRow)*_frameWidth + _stripRect.X,
                                     (_currentFrame/_framesPerRow)*_frameHeight + _stripRect.Y, _frameWidth,
                                     _frameHeight);
            }
        }

        #endregion

        #region Constructor

        public AnimationStripStruct(Texture2D texture, int frameWidth, string name, bool loop)
            : this(texture, frameWidth, name)
        {
            _loopAnimation = loop;
        }

        public AnimationStripStruct(Texture2D texture, int frameWidth, string name, bool loop, float frameDelay)
            : this(texture, frameWidth, name, loop)
        {
            _frameDelay = frameDelay;
        }

        public AnimationStripStruct(Texture2D texture, int frameWidth, string name)
            : this()
        {
            Texture = texture;
            _frameWidth = frameWidth;
            _frameHeight = texture.Height;
            _framesPerRow = texture.Width/frameWidth;
            FrameCount = _framesPerRow;
            Name = name;
            _stripRect = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public AnimationStripStruct(Texture2D texture, Rectangle stripRect, int frameCount, string name, bool loop = true,
                              float frameDelay = .05f)
            : this()
        {
            Texture = texture;
            _stripRect = stripRect;
            FrameCount = frameCount;
            _frameHeight = stripRect.Height;
            _frameWidth = stripRect.Width/frameCount;
            _framesPerRow = frameCount;
            Name = name;
            _loopAnimation = loop;
            _frameDelay = frameDelay;
        }

        public AnimationStripStruct(Texture2D texture, Rectangle stripRect, int frameWidth, int frameHeight, string name,
                              bool loop = true, float frameDelay = .05f)
            :this()
        {
            Texture = texture;
            _stripRect = stripRect;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            Name = name;
            _loopAnimation = loop;
            _frameDelay = frameDelay;
            _framesPerRow = _stripRect.Width/frameWidth;
            FrameCount = _framesPerRow*(_stripRect.Height/frameHeight);
        }
        #endregion

        #region Public Methods
        public void Play()
        {
            _currentFrame = 0;
            _finishedPlaying = false;
        }

        public void Update()
        {
            float elapsed = ShortCuts.ElapsedSeconds;
            _frameTimer += elapsed;
            if (_frameTimer >= _frameDelay)
            {
                ++_currentFrame;
                if (_currentFrame >= FrameCount)
                {
                    if (_loopAnimation)
                    {
                        _currentFrame = 0;
                    }
                    else
                    {
                        _currentFrame = FrameCount - 1;
                        _finishedPlaying = true;
                    }
                }
                _frameTimer = 0f;
            }
        }

        #endregion
    }
}