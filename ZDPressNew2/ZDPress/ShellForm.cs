using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using ZDPress.Opc;
using ZDPress.UI.Views;

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

            Form form = UiHelper.GetFormSingle(typeof(MainForm));

            UiHelper.ShowForm(form, this);
        }
        


        protected override void OnClosing(CancelEventArgs e)
        {
            OpcResponderSingleton.Instance.TimerStop();

            Thread.Sleep(1000);

            base.OnClosing(e);
        }



        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            OpcResponderSingleton.Instance.OpcServerClose();
        }
    }
}
