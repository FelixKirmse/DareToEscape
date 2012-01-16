using System.Windows.Forms;

namespace MapEditor
{
    public partial class TransitionDialog : Form
    {
        private readonly Editor _editor;
        public TransitionDialog(Editor editor)
        {
            _editor = editor;
            InitializeComponent();
        }

        private void MapNameInputBoxKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                OkButtonClick(null,null);
            }
        }

        private void OkButtonClick(object sender, System.EventArgs e)
        {
            _editor.TransitionString = _mapNameInputBox.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
