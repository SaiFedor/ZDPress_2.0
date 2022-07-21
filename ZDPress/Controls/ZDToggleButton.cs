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
    public partial class ZDToggleButton : CheckBox
    {
        public ZDToggleButton()
        {
            InitializeComponent();
            CustomizeControl();
        }

        private void CustomizeControl()
        {
            Appearance = System.Windows.Forms.Appearance.Button;
            FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            MinimumSize = new System.Drawing.Size(0, 69);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumSlateBlue;
            BackColor = Color.AliceBlue;
            TextAlign = ContentAlignment.MiddleCenter;
        }
    }
}
