using log4net;
using log4net.Appender;
using System;
using System.Configuration;
using System.IO;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZDPress.Dal;
using ZDPress.Opc;
using ZDPress.UI.Common;
using ZDPress.UI.ViewModels;

namespace ZDPress.UI.Views
{
    public partial class MainForm : Form
    {
        bool BackupDone { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["BackupDone"]); } }
        public ZdPressDal Dal { get; set; }
        public MainForm()
        {
            InitializeComponent();
            Dal = new ZdPressDal();
        }

        private void OnShowAutomateModeClick()
        {
            ShowFunctionSelectForm();
        }

        private void ShowFunctionSelectForm()
        {
            Cursor.Current = Cursors.WaitCursor;

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

        private void OnDBformClick()
        {
            Form mdiParent = UiHelper.GetMdiContainer(this);

            DBform form = (DBform)UiHelper.GetFormSingle(typeof(DBform));
            DBformViewModel viewModel = new DBformViewModel();
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

            ParametersForm form = (ParametersForm)UiHelper.GetFormSingle(typeof(ParametersForm));
            ParametersFormViewModel viewModel = new ParametersFormViewModel();
            form.ViewModel = viewModel;
            UiHelper.ShowForm(form, mdiParent);
        }


        private void zdToggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            bool value = zdToggleButton1.Checked;

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

        private void zdButton10_Click_1(object sender, EventArgs e)
        {

            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);

                if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    OnDBformClick();
                }
                else
                {
                    MessageBox.Show(@"Доступ запрещён!");
                }
            }
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
           // OnChangeUser();
        }

        //private void OnChangeUser()
        //{
        //    using (LogInWindow logInWindow = new LogInWindow())
        //    {
        //        logInWindow.StartPosition = FormStartPosition.CenterScreen;

        //        DialogResult result = logInWindow.ShowDialog();

        //        bool userPressOk = result == DialogResult.OK;

        //        if (!userPressOk)
        //        {
        //            return;
        //        }

        //        string login = logInWindow.Login;

        //        string passwordEnteredUser = logInWindow.Password;

        //        bool alreadyLogged = login == Program.PressContext.CurrentUser.Login;

        //        if (alreadyLogged)
        //        {
        //            return;
        //        }

        //        if (login == AppUser.User)
        //        {
        //            LoginAsUser();
        //        }
        //        else if (login == AppUser.Admin)
        //        {
        //            LoginAsAdmin(passwordEnteredUser);
        //        }
        //        else
        //        {
        //            MessageBox.Show(@"Не верный логин");
        //        }
        //    }
        //}

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

        protected async override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            string userInfo = toolStripLabelUserInfo.Text;

            if (userInfo != Program.PressContext.CurrentUser.UserTitle)
            {
                toolStripLabelUserInfo.Text = Program.PressContext.CurrentUser.UserTitle;
            }
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            int arhiveDay = 0;
            int.TryParse(configuration.AppSettings.Settings["BackupDate"].Value, out arhiveDay);

            try
            {
                if (DateTime.Now.Day == arhiveDay && !BackupDone)
                {


                    log4net.Repository.Hierarchy.Hierarchy logHierarchy =
    (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();

                    IAppender appender = logHierarchy.Root.Appenders[0];

                    FileAppender fa = (FileAppender)appender;

                    string logDir = Path.GetDirectoryName(fa.File);

                    DirectoryInfo dir = new DirectoryInfo(logDir);

                    if (dir.Exists)
                    {
                        foreach (var file in dir.GetFiles())
                        {
                            try
                            {
                                file.Delete();
                            }
                            catch (Exception ex)
                            {
                                continue;
                                throw;
                            }
                        }
                    }

                    DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.Now.Day);
                    DateTime endDate = DateTime.Now.Date;

                    // bool resultBackup = AutoBackup(startDate, endDate, configuration.AppSettings.Settings["AutoBackupPath"].Value).Result;
                    bool resultBackup = await Task.Run(() => Dal.BackupJsonOperationAsync(
                     startDate,
                     endDate,
                     configuration.AppSettings.Settings["AutoBackupPath"].Value));
                    if (resultBackup)
                    {
                        MessageBox.Show(@"Автоматическое архивирование выполнено успешно!");
                        configuration.AppSettings.Settings["BackupDone"].Value = true.ToString();
                        configuration.Save();

                        ConfigurationManager.RefreshSection("appSettings");
                    }
                    else
                    {
                        MessageBox.Show(@"Автоматическое архивирование не выполнено!");
                    }

                }
                else if (DateTime.Now.Day != arhiveDay)
                {
                    configuration.AppSettings.Settings["BackupDone"].Value = false.ToString();
                    configuration.Save();

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        //private async Task<bool> AutoBackup(DateTime startDate, DateTime endDate, string saveFilePath)
        //{
        //    //return await Task.Run(() => Dal.BackupJsonOperationAsync(
        //    //        startDate,
        //    //        endDate,
        //    //        saveFilePath));

        //}

        private void toolStripButtonLogOut_Click(object sender, EventArgs e)
        {
            LoginAsUser();
        }
    }
}
