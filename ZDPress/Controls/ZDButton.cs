using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZDPress.UI.Controls
{
    public partial class ZDButton : Button
    {
        public ZDButton()
        {
            InitializeComponent();
            CusomizeControl();
        }

        private void CusomizeControl()
        {
            Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            MinimumSize = new System.Drawing.Size(0, 69);
            Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            BackColor = Color.AliceBlue;
            FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        }
    }
}
