using System;
using System.Windows.Forms;

namespace ZDPress.UI.Views
{
    public partial class OperationSelectForm2 : Form
    {
        public OperationSelectForm2()
        {
            InitializeComponent();
        }

        private void zdButton1_Click(object sender, EventArgs e)
        {
            OnBackClick();
        }

        private void OnBackClick()
        {
            Form form = UiHelper.GetFormSingle(typeof(OperationSelectForm2));
            form.Hide();
        }

        private void zdButton2_Click(object sender, EventArgs e)
        {
            MiddleBrakeDiskClick();
        }

        private void MiddleBrakeDiskClick()
        {
            //throw new NotImplementedException();
        }

        private void zdButton4_Click(object sender, EventArgs e)
        {
            LeftBrakeDiskClick();
        }

        private void LeftBrakeDiskClick()
        {
            //throw new NotImplementedException();
        }

        private void zdButton6_Click(object sender, EventArgs e)
        {
            RightBrakeDiskClick();
        }

        private void RightBrakeDiskClick()
        {
            //throw new NotImplementedException();
        }

        private void zdButton3_Click(object sender, EventArgs e)
        {
            OnMeasureAxizClick();
        }

        private void OnMeasureAxizClick()
        {
            //throw new NotImplementedException();
        }

        private void zdButton5_Click(object sender, EventArgs e)
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
            UiHelper.ShowForm(form, mdiParent);
        }

        private void zdButton7_Click(object sender, EventArgs e)
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
    }
}
