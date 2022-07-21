using System;
using System.Windows.Forms;

namespace ZDPress.UI.Views
{
    public partial class LogInWindow : Form
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            AcceptLogin();
        }

        private void AcceptLogin()
        {
            Login = textBoxLogin.Text;

            Password = textBoxPassword.Text;

            DialogResult = DialogResult.OK;
        }

        public string Login { get; set; }

        public string Password { get; set; }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down
                AcceptLogin();
            }
        }
    }
}
