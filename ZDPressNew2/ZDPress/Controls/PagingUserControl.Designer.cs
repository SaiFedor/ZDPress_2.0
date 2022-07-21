namespace ZDPress.UI.Controls
{
    partial class PagingUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PagingUserControl));
            this.pagingToolStrip = new System.Windows.Forms.ToolStrip();
            this.btnFirst = new System.Windows.Forms.ToolStripButton();
            this.btnBackward = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.btnForward = new System.Windows.Forms.ToolStripButton();
            this.btnLast = new System.Windows.Forms.ToolStripButton();
            this.pageSizeListComboBox = new System.Windows.Forms.ComboBox();
            this.labelTitle1 = new System.Windows.Forms.Label();
            this.labelTitle2 = new System.Windows.Forms.Label();
            this.labelPageInfo = new System.Windows.Forms.Label();
            this.pagingToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pagingToolStrip
            // 
            this.pagingToolStrip.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pagingToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.pagingToolStrip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.pagingToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.pagingToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFirst,
            this.btnBackward,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripButton9,
            this.toolStripButton10,
            this.btnForward,
            this.btnLast});
            this.pagingToolStrip.Location = new System.Drawing.Point(-1, 5);
            this.pagingToolStrip.Name = "pagingToolStrip";
            this.pagingToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.pagingToolStrip.Size = new System.Drawing.Size(358, 25);
            this.pagingToolStrip.TabIndex = 0;
            this.pagingToolStrip.Text = "toolStrip1";
            // 
            // btnFirst
            // 
            this.btnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFirst.Image = global::ZDPress.UI.Properties.Resources.fastreverse;
            this.btnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(23, 22);
            this.btnFirst.Text = "toolStripButton1";
            this.btnFirst.ToolTipText = "Первая";
            // 
            // btnBackward
            // 
            this.btnBackward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBackward.Image = global::ZDPress.UI.Properties.Resources.Back;
            this.btnBackward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(23, 22);
            this.btnBackward.Text = "toolStripButton1";
            this.btnBackward.ToolTipText = "Предыдущая";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "1";
            this.toolStripButton1.ToolTipText = "Страница 1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "2";
            this.toolStripButton2.ToolTipText = "Страница 2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "3";
            this.toolStripButton3.ToolTipText = "Страница 3";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "4";
            this.toolStripButton4.ToolTipText = "Страница 4";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "5";
            this.toolStripButton5.ToolTipText = "Страница 5";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "6";
            this.toolStripButton6.ToolTipText = "Страница 6";
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton7.Text = "7";
            this.toolStripButton7.ToolTipText = "Страница 7";
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "8";
            this.toolStripButton8.ToolTipText = "Страница 8";
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton9.Text = "9";
            this.toolStripButton9.ToolTipText = "Страница 9";
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(25, 22);
            this.toolStripButton10.Text = "10";
            this.toolStripButton10.ToolTipText = "Страница 10";
            // 
            // btnForward
            // 
            this.btnForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnForward.Image = global::ZDPress.UI.Properties.Resources.Forward;
            this.btnForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(23, 22);
            this.btnForward.Text = "toolStripButton6";
            this.btnForward.ToolTipText = "Следующая";
            // 
            // btnLast
            // 
            this.btnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLast.Image = global::ZDPress.UI.Properties.Resources.fastforward;
            this.btnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(23, 22);
            this.btnLast.Text = "toolStripButton6";
            this.btnLast.ToolTipText = "Последняя";
            // 
            // pageSizeListComboBox
            // 
            this.pageSizeListComboBox.FormattingEnabled = true;
            this.pageSizeListComboBox.Location = new System.Drawing.Point(383, 5);
            this.pageSizeListComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.pageSizeListComboBox.Name = "pageSizeListComboBox";
            this.pageSizeListComboBox.Size = new System.Drawing.Size(53, 21);
            this.pageSizeListComboBox.TabIndex = 1;
            this.pageSizeListComboBox.SelectedIndexChanged += new System.EventHandler(this.pageSizeListComboBox_SelectedIndexChanged);
            // 
            // labelTitle1
            // 
            this.labelTitle1.AutoSize = true;
            this.labelTitle1.Location = new System.Drawing.Point(359, 8);
            this.labelTitle1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTitle1.Name = "labelTitle1";
            this.labelTitle1.Size = new System.Drawing.Size(24, 13);
            this.labelTitle1.TabIndex = 2;
            this.labelTitle1.Text = "По ";
            // 
            // labelTitle2
            // 
            this.labelTitle2.AutoSize = true;
            this.labelTitle2.Location = new System.Drawing.Point(438, 8);
            this.labelTitle2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTitle2.Name = "labelTitle2";
            this.labelTitle2.Size = new System.Drawing.Size(62, 13);
            this.labelTitle2.TabIndex = 3;
            this.labelTitle2.Text = "элементов";
            // 
            // labelPageInfo
            // 
            this.labelPageInfo.AutoSize = true;
            this.labelPageInfo.Location = new System.Drawing.Point(439, 8);
            this.labelPageInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPageInfo.Name = "labelPageInfo";
            this.labelPageInfo.Size = new System.Drawing.Size(0, 13);
            this.labelPageInfo.TabIndex = 4;
            // 
            // PagingUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelPageInfo);
            this.Controls.Add(this.labelTitle2);
            this.Controls.Add(this.labelTitle1);
            this.Controls.Add(this.pageSizeListComboBox);
            this.Controls.Add(this.pagingToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PagingUserControl";
            this.Size = new System.Drawing.Size(503, 31);
            this.pagingToolStrip.ResumeLayout(false);
            this.pagingToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip pagingToolStrip;
        private System.Windows.Forms.ToolStripButton btnFirst;
        private System.Windows.Forms.ToolStripButton btnBackward;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripButton btnForward;
        private System.Windows.Forms.ToolStripButton btnLast;
        private System.Windows.Forms.ComboBox pageSizeListComboBox;
        private System.Windows.Forms.Label labelTitle1;
        private System.Windows.Forms.Label labelTitle2;
        private System.Windows.Forms.Label labelPageInfo;
    }
}
