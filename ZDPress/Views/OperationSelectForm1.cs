using System;
using System.Windows.Forms;
using ZDPress.Opc;
using ZDPress.UI.ViewModels;

namespace ZDPress.UI.Views
{
    public partial class OperationSelectForm1 : Form
    {
        public bool WasOpen { get; set; }
        public OperationSelectFormViewModel ViewModel { get; set; }

        public OperationSelectForm1()
        {
            InitializeComponent();
            ViewModel = new OperationSelectFormViewModel();
            
        }


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            
            if (!WasOpen)
            {
                WasOpen = true;
            }
            else 
            {
                ViewModel.LeftWheel = false;//TODO: ????
            }
            
            BindViewModel();
        }
        
        
        private void BindViewModel()
        {
            zdButton3.DataBindings.Add(new Binding("Checked", ViewModel, "LeftWheel", true, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void zdButton1_Click(object sender, EventArgs e)
        {
            Hide();
        }

      
        private void OnLeftWheelClick()
        {
            Cursor.Current = Cursors.WaitCursor;

            bool value = zdButton3.Checked;

            OpcResponderSingleton.Instance.WriteBitToOpc(BitParameters.LeftWheel, value);

            Cursor.Current = Cursors.Default;
        }



        private void OnRightWheelClick()
        {
            Cursor.Current = Cursors.WaitCursor;

            bool value = zdToggleButton1.Checked;

            OpcResponderSingleton.Instance.WriteBitToOpc(BitParameters.RightWheel, value);

            Cursor.Current = Cursors.Default;
        }


        private void zdButton4_Click(object sender, EventArgs e)
        {
            OnChartClick();
        }

        private void ShowChartForm()
        {
            Form mdiParent = UiHelper.GetMdiContainer(this);
            ChartForm form = (ChartForm)UiHelper.GetFormSingle(typeof(ChartForm));
            UiHelper.ShowForm(form, mdiParent);
        }

        private void OnChartClick()
        {
            ShowChartForm();
        }

        private void zdButton6_Click(object sender, EventArgs e)
        {
            OnParamatersClick();
        }

        private void OnParamatersClick()
        {
            ShowParametersForm();
        }

        private void ShowParametersForm()
        {
            Form mdiParent = UiHelper.GetMdiContainer(this);

            Form form = UiHelper.GetFormSingle(typeof(ParametersForm));

            UiHelper.ShowForm(form, mdiParent);
        }

        private void zdButton3_CheckedChanged(object sender, EventArgs e)
        {
            OnLeftWheelClick();
        }


        private void zdToggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            OnRightWheelClick();
        }


        private void OnMeasureAxizClick()
        {
            Cursor.Current = Cursors.WaitCursor;

            bool value = zdToggleButton2.Checked;
            
            OpcResponderSingleton.Instance.WriteBitToOpc(BitParameters.IzmerenieOsiStart, value);

            Cursor.Current = Cursors.Default;
        }


        private void zdToggleButton2_CheckedChanged(object sender, EventArgs e)
        {
            OnMeasureAxizClick();
        }
    }
}
