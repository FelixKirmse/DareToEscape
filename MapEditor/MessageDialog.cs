using System;
using System.Windows.Forms;

namespace MapEditor
{
    public delegate void OkEvent(string message);

    public partial class MessageDialog : Form
    {
        public MessageDialog()
        {
            InitializeComponent();
        }

        public event OkEvent OnOk;

        private void MapNameInputBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OkButtonClick(null, null);
            }
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            if (OnOk != null)
                OnOk(_messageInputBox.Text);
            DialogResult = DialogResult.OK;
        }
    }
}