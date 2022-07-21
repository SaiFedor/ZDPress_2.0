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

            ViewModel = new ParametersFormViewModel();
        }

        public ParametersFormViewModel ViewModel { get; set; }


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            BindViewModel();
        }


        private void BindViewModel()
        {
            maskedTextBox1.DataBindings.Add(new Binding("Text", ViewModel, "SpeedPress", true, DataSourceUpdateMode.OnPropertyChanged));
            maskedTextBox2.DataBindings.Add(new Binding("Text", ViewModel, "WheelPosition", true, DataSourceUpdateMode.OnPropertyChanged));
            maskedTextBox3.DataBindings.Add(new Binding("Text", ViewModel, "Instrument", true, DataSourceUpdateMode.OnPropertyChanged));
            maskedTextBox4.DataBindings.Add(new Binding("Text", ViewModel, "EmphasisTravers", true, DataSourceUpdateMode.OnPropertyChanged));
            maskedTextBox5.DataBindings.Add(new Binding("Text", ViewModel, "EmphasisPlunger", true, DataSourceUpdateMode.OnPropertyChanged));
        }




        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            OpcResponderSingleton.Instance.ViewItems(ViewModel.GetAsParametesDictionary().Select(el => el.Key).ToList());

            List<OpcParameter> parameters = OpcResponderSingleton.Instance.ProcessParameters(OpcResponderSingleton.Instance.Parameters);
            
            ViewModel.SetPropertiesByParameters(parameters);
        }
      

        private void OnBackClick()
        {
            Form form = UiHelper.GetFormSingle(typeof(ParametersForm));
            form.Hide();
        }

    

        private void OnSaveParamsClick()
        {
            Cursor.Current = Cursors.WaitCursor;            
            OpcResponderSingleton.Instance.WriteToOpcList(ViewModel.GetAsParametesDictionary());
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
