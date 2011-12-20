using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace DareToEscape.Helpers
{
    [Serializable]
    public struct ResolutionInformation
    {

        public Viewport Viewport;
        public bool FullScreen;
        public Size Resolution;
        public Matrix Matrix;


        public ResolutionInformation(Viewport viewport, bool fullScreen, Size resolution, Matrix matrix)
        {
            Viewport = viewport;
            FullScreen = fullScreen;
            Resolution = resolution;
            Matrix = matrix;
        }
    }

    public partial class ResolutionChooser : Form
    {
        private readonly float _aspectRatio;
        private bool _remember;
        private Viewport _viewport;
        private Matrix _matrix;
        private bool _fullScreen;
        private Size _resolution;
        public static readonly string Settings = string.Format(@"{0}\settings.cfg", Application.StartupPath);
        private readonly DareToEscape _parent;
        private ResolutionInformation _resInfo;

        

        public ResolutionChooser(DareToEscape parent)
        {
            _parent = parent;
            DisplayMode cdm = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            _aspectRatio = (float) cdm.Width/cdm.Height;
            InitializeComponent();
            _resolutionComboBox.SelectedIndex = 0;
        }

        private void ResolutionComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            int scale = _resolutionComboBox.SelectedIndex;
            _matrix = scale > 0 ? Matrix.CreateScale(scale, scale, 1f) : Matrix.CreateScale(1f);
            const int width = DareToEscape.ResolutionWidth;
            const int height = DareToEscape.ResolutionHeight;
            _fullScreen = scale == 0;
            if (scale == 0)
            {
                _viewport = new Viewport
                                {
                                    X = (int) (_aspectRatio * height - width)/2,
                                    Y = 0,
                                    Width = width,
                                    Height = height,
                                    MinDepth = 0,
                                    MaxDepth = 1
                                };
                _resolution = new Size((int)(_aspectRatio * height), height);
                
            }
            else
            {
                _viewport = new Viewport
                                {
                                    X = 0,
                                    Y = 0,
                                    Width = width*scale,
                                    Height = height*scale,
                                    MinDepth = 0,
                                    MaxDepth = 1
                                };
                _resolution = new Size(_viewport.Width, _viewport.Height);
            }
        }

        private void RememberCheckboxCheckedChanged(object sender, EventArgs e)
        {
            _remember = rememberCheckbox.Checked;
        }

        private void StartGameBtnClick(object sender, EventArgs e)
        {
            _resInfo = new ResolutionInformation(_viewport, _fullScreen, _resolution, _matrix);
            if(_remember)
            {
                var fs = new FileStream(Settings, FileMode.Create);
                var xmls = new XmlSerializer(_resInfo.GetType());
                xmls.Serialize(fs, _resInfo);
                fs.Close();
            }
            Hide();
            _parent.ResInfo = _resInfo;
            Close();
        }
    }
}