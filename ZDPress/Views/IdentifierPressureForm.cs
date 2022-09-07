using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using ZDPress.Opc;
using ZDPress.UI.ViewModels;

namespace ZDPress.UI.Views
{
    public partial class IdentifierPressureForm : Form
    {

        public IdentifierPressureViewModel ViewModel { get; set; }

        public IdentifierPressureForm()
        {
            InitializeComponent();
        }

        private void zdButtonBack_Click(object sender, System.EventArgs e)
        {
            OnBackClick();                      
        }

        private void OnBackClick()
        {
            Hide();

            //ViewModel.StopAutoUpdateParameters();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            System.Diagnostics.Debug.WriteLine("OnClosing");
        }

        private void BindViewModel()
        {
            if (ViewModel == null)
            {
                new Exception("ViewModel == null");
            }

            zdLabel7.DataBindings.Add(new Binding("Text", ViewModel, "DispPress1", true, DataSourceUpdateMode.OnPropertyChanged));
            zdLabel6.DataBindings.Add(new Binding("Text", ViewModel, "DispPress2", true, DataSourceUpdateMode.OnPropertyChanged));
            zdLabel5.DataBindings.Add(new Binding("Text", ViewModel, "DispPress3", true, DataSourceUpdateMode.OnPropertyChanged));

            zdLabel8.DataBindings.Add(new Binding("BackColor", ViewModel, "PlcConnectState", true, DataSourceUpdateMode.OnPropertyChanged));

            label1.DataBindings.Add(new Binding("Text", ViewModel, "AlarmBP1", true, DataSourceUpdateMode.OnPropertyChanged));

            label2.DataBindings.Add(new Binding("Text", ViewModel, "AlarmBP2", true, DataSourceUpdateMode.OnPropertyChanged));
            label3.DataBindings.Add(new Binding("Text", ViewModel, "AlarmBP3", true, DataSourceUpdateMode.OnPropertyChanged));
        }

    
        

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            BindViewModel();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            ViewModel.RunAutoUpdateParameters(2000);
        }        


   }
}
