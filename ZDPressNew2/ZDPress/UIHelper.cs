using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ZDPress.UI
{
    /// <summary>
    /// TODO: какая то муть...
    /// </summary>
    public static class UiHelper
    {
        public static readonly List<Form> ChildForms = new List<Form>();

        public static Form GetFormSingle(Type formType)
        {
            Form form = ChildForms.FirstOrDefault(c => c.GetType() == formType);

            if (form != null) return form;

            form = (Form)Activator.CreateInstance(formType);

            ChildForms.Add(form);

            return form;
        }


        /// <summary>
        /// Покажет форму.
        /// </summary>
        /// <param name="form">Форма которую показать.</param>
        /// <param name="mdiParent">Mdi родитель.</param>
        public static void ShowForm(Form form, Form mdiParent)
        {

            if (mdiParent != null)
            {
                form.MdiParent = mdiParent;                
            }
            form.MdiParent = mdiParent;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.ControlBox = false;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Show();
        }
       

        public static void CloseForm(Type formType)
        {
            Form form = GetFormSingle(formType);

            ChildForms.Remove(form);

            form.Close();
        }

        public static Form GetMdiContainer(Form form)
        {
            if (form.IsMdiContainer)
            {
                return form;
            }
          
            Form mdiContainer = form;

            while (mdiContainer.MdiParent != null)
            {
                mdiContainer = mdiContainer.MdiParent;
            }

            return mdiContainer.IsMdiContainer ? mdiContainer : null;
        }
    }
}
