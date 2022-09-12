using System;
using System.ComponentModel;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Windows;
using ZDPress.Opc;
using ZDPress.UI.Views;
using System.Drawing.Printing;

namespace ZDPress.UI
{
    public partial class ShellForm : Form
    {
        public ShellForm()
        {
            InitializeComponent();
        }
        

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

           
            OnShellShow();
        }


        private void OnShellShow()
        {
            WindowState = FormWindowState.Maximized;
           
            MinimumSize = Size;

            Form form = UiHelper.GetFormSingle(typeof(MainForm));

            UiHelper.ShowForm(form, this);

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            bool firstStart = bool.Parse(configuration.AppSettings.Settings["FirstStart"].Value);

            if (!firstStart)
            {
                PrinterSettings printSettings = new PrinterSettings();
                configuration.AppSettings.Settings["BackupPath"].Value = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                configuration.AppSettings.Settings["AutoBackupPath"].Value = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                configuration.AppSettings.Settings["RegisterArhivePath"].Value = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                configuration.AppSettings.Settings["PassportsArhivePath"].Value = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                configuration.AppSettings.Settings["PrinterName"].Value = printSettings.PrinterName;
                configuration.AppSettings.Settings["FirstStart"].Value = "True";
                configuration.Save();

                if (!Directory.Exists(@"C:\Logs"))
                {
                    Directory.CreateDirectory(@"C:\Logs");
                }
                
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            OpcResponderSingleton.Instance.TimerStop();
            MainConstant.CancellationTokenSourcePLCread.Cancel();
            if (MainConstant.plc.client != null)
            {                
                MainConstant.plc.client.Dispose();                
            }
            if (MainConstant.plc != null)
            {
                MainConstant.plc.Dispose();
            }

            Thread.Sleep(1000);

            base.OnClosing(e);
        }
    }
}
