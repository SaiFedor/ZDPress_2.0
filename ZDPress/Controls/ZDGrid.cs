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
    public partial class ZDGrid : DataGridView
    {
        public ZDGrid()
        {
            InitializeComponent();
            RowPostPaint += dataGridView_RowPostPaint;
            InitializeCustomComponent();
        }



        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
            {
                // Calculate the bounds of the row 
                int rowHeaderWidth = RowHeadersVisible ? RowHeadersWidth : 0;
                Rectangle rowBounds = new Rectangle(
                    rowHeaderWidth,
                    e.RowBounds.Top,
                    Columns.GetColumnsWidth(DataGridViewElementStates.Visible) - HorizontalScrollingOffset + 1,
                    e.RowBounds.Height);

                // Paint the border 
                ControlPaint.DrawBorder(e.Graphics, rowBounds, Color.Red , ButtonBorderStyle.Solid);//AliceBlue
            }
        }

        private void InitializeCustomComponent()
        {

            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();




            ((ISupportInitialize)(this)).BeginInit();
            SuspendLayout();
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(255, 246, 246);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            AutoGenerateColumns = false;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.AliceBlue;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;

            DefaultCellStyle = dataGridViewCellStyle2;
            MultiSelect = false;
            ReadOnly = true;
            RowHeadersVisible = false;
            
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.Padding = new Padding(3);
            dataGridViewCellStyle3.SelectionBackColor = Color.AliceBlue;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            
            RowsDefaultCellStyle = dataGridViewCellStyle3;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;

            ((ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
