using System;
using System.Windows.Forms;

namespace ZDPress.UI.Views
{
    public partial class FunctionSelectForm : Form
    {
        public FunctionSelectForm()
        {
            InitializeComponent();
        }

        private void zdButton3_Click(object sender, EventArgs e)
        {
            OnBackClick();
        }

        private void OnBackClick()
        {
            this.Hide();
        }

      

       

        private void zdButton1_Click(object sender, EventArgs e)
        {
            OnWheelClick();//Выбор прессования колес и переход на экран выбора операции 
        }

        private void OnWheelClick()
        {
            Form mdiParent = UiHelper.GetMdiContainer(this);
            OperationSelectForm1 form = (OperationSelectForm1)UiHelper.GetFormSingle(typeof(OperationSelectForm1));
            UiHelper.ShowForm(form, mdiParent);
        }

        private void zdButton2_Click(object sender, EventArgs e)
        {
            // Выбор прессования тормозных дисков и переход на экран выбора операции 
            OnBreakDiskClick();
        }

        private void OnBreakDiskClick()
        {
            Form mdiParent = UiHelper.GetMdiContainer(this);
            Form form = UiHelper.GetFormSingle(typeof(OperationSelectForm2));
            UiHelper.ShowForm(form, mdiParent);
        }
    }
}
