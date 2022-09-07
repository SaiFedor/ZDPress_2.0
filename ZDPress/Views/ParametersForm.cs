using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ZDPress.Dal.Entities;
using ZDPress.Opc;
using ZDPress.UI.ViewModels;

namespace ZDPress.UI.Views
{
    public partial class ParametersForm : Form
    {
        public ParametersForm()
        {
            InitializeComponent();
        }

        public ParametersFormViewModel ViewModel { get; set; }


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            BindViewModel();
            ViewModel.RunAutoUpdateParameters(2000);
        }


        private void BindViewModel()
        {
            maskedTextBox1.DataBindings.Add(new Binding("Text", ViewModel, "SpeedPress", true, DataSourceUpdateMode.OnPropertyChanged));
            maskedTextBox2.DataBindings.Add(new Binding("Text", ViewModel, "MaxPress", true, DataSourceUpdateMode.OnPropertyChanged));
            maskedTextBox3.DataBindings.Add(new Binding("Text", ViewModel, "Instrument", true, DataSourceUpdateMode.OnPropertyChanged));
            maskedTextBox4.DataBindings.Add(new Binding("Text", ViewModel, "EmphasisTravers", true, DataSourceUpdateMode.OnPropertyChanged));
            maskedTextBox5.DataBindings.Add(new Binding("Text", ViewModel, "EmphasisPlunger", true, DataSourceUpdateMode.OnPropertyChanged));
            zdLabel13.DataBindings.Add(new Binding("BackColor", ViewModel, "PlcConnectState", true, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void OnBackClick()
        {
            Form form = UiHelper.GetFormSingle(typeof(ParametersForm));
            form.Hide();
        }

        private void OnSaveParamsClick()
        {
            Cursor.Current = Cursors.WaitCursor;
            ViewModel.SaveParameters();
            Cursor.Current = Cursors.Default;
        }

        private void zdButton3_Click(object sender, EventArgs e)
        {
            OnSaveParamsClick();
        }

        private void zdButton4_Click(object sender, EventArgs e)
        {
            OnBackClick();
        }
    }
}
