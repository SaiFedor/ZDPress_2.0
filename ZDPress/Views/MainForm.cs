using System;
using System.Configuration;
using System.Windows.Forms;
using ZDPress.Opc;
using ZDPress.UI.Common;
using ZDPress.UI.ViewModels;

namespace ZDPress.UI.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
      

        private void OnShowAutomateModeClick()
        {
            ShowFunctionSelectForm();
        }


        private void ShowFunctionSelectForm()
        {
            Cursor.Current = Cursors.WaitCursor;

            OpcResponderSingleton.Instance.WriteBitToOpc(BitParameters.AvtomatRezhim, true);

            Cursor.Current = Cursors.Default;


            Form mdiParent = UiHelper.GetMdiContainer(this);

            Form form = UiHelper.GetFormSingle(typeof(FunctionSelectForm));

            UiHelper.ShowForm(form, mdiParent);
        }

     
        private void zdButton13_Click(object sender, EventArgs e)
        {
            OnChartClick();
        }


        private void OnChartClick()
        {
            ShowChartForm();
        }


        private void ShowChartForm()
        {
            Form mdiParent = UiHelper.GetMdiContainer(this);

            ChartForm form = (ChartForm)UiHelper.GetFormSingle(typeof(ChartForm));

            ChartFormViewModel viewModel = new ChartFormViewModel 
            {
                PressOperation = OpcLayer.CurrentPressOperation//TODO: подумать как лучше брать текущую операцию
            };
            form.buttonSaveOperation.Visible = false;
            viewModel.CanSaveOperation = false;
            form.ViewModel = viewModel;
            

            form.Mode = ChartFormShowMode.ShowCurrentOperation;

            UiHelper.ShowForm(form, mdiParent);
        }


        private void OnIdentifierPressureClick()
        {
            Form mdiParent = UiHelper.GetMdiContainer(this);

            IdentifierPressureForm form = (IdentifierPressureForm)UiHelper.GetFormSingle(typeof(IdentifierPressureForm));
            IdentifierPressureViewModel viewModel = new IdentifierPressureViewModel();
            form.ViewModel = viewModel;
            UiHelper.ShowForm(form, mdiParent);
        }

        private void zdButton14_Click(object sender, EventArgs e)
        {
            OnParametersClick();
        }


        private void OnParametersClick()
        {
            ShowParametersForm();
        }


        private void ShowParametersForm()
        {
            Form mdiParent = UiHelper.GetMdiContainer(this);

            Form form = UiHelper.GetFormSingle(typeof(ParametersForm));

            UiHelper.ShowForm(form, mdiParent);
        }


        private void zdToggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            bool value = zdToggleButton1.Checked;

            OpcResponderSingleton.Instance.WriteBitToOpc(BitParameters.RuchnoRezhim, value);

            Cursor.Current = Cursors.Default;
        }

        private void zdButton8_Click_1(object sender, EventArgs e)
        {
            OnShowAutomateModeClick();
        }

        private void zdButton9_Click_1(object sender, EventArgs e)
        {
            OnIdentifierPressureClick();
        }

        private void zdButton7_Click_1(object sender, EventArgs e)
        {
            OnOperationsClick();
        }


        private void OnOperationsClick()
        {
            Form form = UiHelper.GetFormSingle(typeof(OperationListForm));

            UiHelper.ShowForm(form, UiHelper.GetMdiContainer(this));
        }

        private void toolStripButtonChangeUser_Click(object sender, EventArgs e)
        {
            OnChangeUser();
        }

        private void OnChangeUser()
        {
            using (LogInWindow logInWindow = new LogInWindow())
            {
                logInWindow.StartPosition = FormStartPosition.CenterScreen;

                DialogResult result = logInWindow.ShowDialog();

                bool userPressOk = result == DialogResult.OK;

                if (!userPressOk)
                {
                    return;
                }

                string login = logInWindow.Login;

                string passwordEnteredUser = logInWindow.Password;

                bool alreadyLogged = login == Program.PressContext.CurrentUser.Login;

                if (alreadyLogged)
                {
                    return;
                }

                if (login == AppUser.User)
                {
                    LoginAsUser();
                }
                else if (login == AppUser.Admin)
                {
                    LoginAsAdmin(passwordEnteredUser);
                }
                else
                {
                    MessageBox.Show(@"Не верный логин");
                }
            }
        }

        private void LoginAsAdmin(string passwordEnteredUser)
        {
            // залогинились админом
            string encryptedPasswordFromConfig = ConfigurationManager.AppSettings["pwd"];

            if (string.IsNullOrEmpty(encryptedPasswordFromConfig))
            {
                MessageBox.Show(@"Ошибка! Конфигурационный файл не содердит данных о пароле");
                return;
            }

            string encriptedPasswordEnteredUser = Encriptor.EncryptAESWithBase64(passwordEnteredUser);

            if (encryptedPasswordFromConfig != passwordEnteredUser)
            {
                MessageBox.Show(@"Неверный логин или пароль");
                return;
            }

            Program.PressContext.CurrentUser.Role = ControlBasedRightsManager.InitializeRoleByLogin(AppUser.Admin);

            Program.PressContext.CurrentUser.Login = AppUser.Admin;

            ControlBasedRightsManager.ConfigureControlsByRole(this, Program.PressContext.CurrentUser.Role);

            toolStripLabelUserInfo.Text = Program.PressContext.CurrentUser.UserTitle;

            toolStripButtonLogOut.Visible = true;
        }

        private void LoginAsUser()
        {
            Program.PressContext.CurrentUser.Role = ControlBasedRightsManager.InitializeRoleByLogin(AppUser.User);
            Program.PressContext.CurrentUser.Login = AppUser.User;
            ControlBasedRightsManager.ConfigureControlsByRole(this, Program.PressContext.CurrentUser.Role);
            toolStripLabelUserInfo.Text = Program.PressContext.CurrentUser.UserTitle;
            toolStripButtonLogOut.Visible = false;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            string userInfo = toolStripLabelUserInfo.Text;

            if (userInfo != Program.PressContext.CurrentUser.UserTitle)
            {
                toolStripLabelUserInfo.Text = Program.PressContext.CurrentUser.UserTitle;
            }
        }

        private void toolStripButtonLogOut_Click(object sender, EventArgs e)
        {
            LoginAsUser();
        }
    }
}
