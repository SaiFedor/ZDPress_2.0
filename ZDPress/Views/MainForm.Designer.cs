namespace ZDPress.UI.Views
{
    public partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.zdToggleButton1 = new ZDPress.UI.Controls.ZDToggleButton();
            this.zdButton9 = new ZDPress.UI.Controls.ZDButton();
            this.zdButton13 = new ZDPress.UI.Controls.ZDButton();
            this.zdButton7 = new ZDPress.UI.Controls.ZDButton();
            this.zdButton14 = new ZDPress.UI.Controls.ZDButton();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelUserInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonChangeUser = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLogOut = new System.Windows.Forms.ToolStripButton();
            this.zdButton1 = new ZDPress.UI.Controls.ZDButton();
            this.zdButton2 = new ZDPress.UI.Controls.ZDButton();
            this.zdButton3 = new ZDPress.UI.Controls.ZDButton();
            this.zdButton4 = new ZDPress.UI.Controls.ZDButton();
            this.zdButton5 = new ZDPress.UI.Controls.ZDButton();
            this.zdButton6 = new ZDPress.UI.Controls.ZDButton();
            this.zdLabel1 = new ZDPress.UI.Controls.ZDLabel();
            this.tableLayoutPanelMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.zdToggleButton1, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.zdButton9, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.zdButton13, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.zdButton7, 0, 4);
            this.tableLayoutPanelMain.Controls.Add(this.zdButton14, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.toolStripMain, 1, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 5;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692306F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.76923F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.76923F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.76923F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(416, 430);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // zdToggleButton1
            // 
            this.zdToggleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zdToggleButton1.Appearance = System.Windows.Forms.Appearance.Button;
            this.zdToggleButton1.AutoSize = true;
            this.zdToggleButton1.BackColor = System.Drawing.Color.AliceBlue;
            this.zdToggleButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdToggleButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdToggleButton1.Location = new System.Drawing.Point(3, 136);
            this.zdToggleButton1.MinimumSize = new System.Drawing.Size(0, 69);
            this.zdToggleButton1.Name = "zdToggleButton1";
            this.zdToggleButton1.Size = new System.Drawing.Size(202, 69);
            this.zdToggleButton1.TabIndex = 20;
            this.zdToggleButton1.Text = "Ручной режим";
            this.zdToggleButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.zdToggleButton1.UseVisualStyleBackColor = false;
            this.zdToggleButton1.CheckedChanged += new System.EventHandler(this.zdToggleButton1_CheckedChanged);
            // 
            // zdButton9
            // 
            this.zdButton9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zdButton9.BackColor = System.Drawing.Color.AliceBlue;
            this.zdButton9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdButton9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdButton9.Location = new System.Drawing.Point(211, 136);
            this.zdButton9.MinimumSize = new System.Drawing.Size(0, 69);
            this.zdButton9.Name = "zdButton9";
            this.zdButton9.Size = new System.Drawing.Size(202, 69);
            this.zdButton9.TabIndex = 23;
            this.zdButton9.Text = "Датчики";
            this.zdButton9.UseVisualStyleBackColor = false;
            this.zdButton9.Click += new System.EventHandler(this.zdButton9_Click_1);
            // 
            // zdButton13
            // 
            this.zdButton13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zdButton13.BackColor = System.Drawing.Color.AliceBlue;
            this.zdButton13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdButton13.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdButton13.Location = new System.Drawing.Point(3, 223);
            this.zdButton13.MinimumSize = new System.Drawing.Size(0, 69);
            this.zdButton13.Name = "zdButton13";
            this.zdButton13.Size = new System.Drawing.Size(202, 69);
            this.zdButton13.TabIndex = 18;
            this.zdButton13.Text = "График";
            this.zdButton13.UseVisualStyleBackColor = false;
            this.zdButton13.Click += new System.EventHandler(this.zdButton13_Click);
            // 
            // zdButton7
            // 
            this.zdButton7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zdButton7.BackColor = System.Drawing.Color.AliceBlue;
            this.tableLayoutPanelMain.SetColumnSpan(this.zdButton7, 2);
            this.zdButton7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdButton7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdButton7.Location = new System.Drawing.Point(3, 321);
            this.zdButton7.MinimumSize = new System.Drawing.Size(0, 69);
            this.zdButton7.Name = "zdButton7";
            this.zdButton7.Size = new System.Drawing.Size(410, 69);
            this.zdButton7.TabIndex = 25;
            this.zdButton7.Text = "Журнал операций";
            this.zdButton7.UseVisualStyleBackColor = false;
            this.zdButton7.Click += new System.EventHandler(this.zdButton7_Click_1);
            // 
            // zdButton14
            // 
            this.zdButton14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zdButton14.BackColor = System.Drawing.Color.AliceBlue;
            this.zdButton14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdButton14.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdButton14.Location = new System.Drawing.Point(211, 223);
            this.zdButton14.MinimumSize = new System.Drawing.Size(0, 69);
            this.zdButton14.Name = "zdButton14";
            this.zdButton14.Size = new System.Drawing.Size(202, 69);
            this.zdButton14.TabIndex = 19;
            this.zdButton14.Text = "Параметры";
            this.zdButton14.UseVisualStyleBackColor = false;
            this.zdButton14.Click += new System.EventHandler(this.zdButton14_Click);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripMain.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelUserInfo,
            this.toolStripSeparator1,
            this.toolStripButtonChangeUser,
            this.toolStripButtonLogOut});
            this.toolStripMain.Location = new System.Drawing.Point(265, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(151, 24);
            this.toolStripMain.TabIndex = 26;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripLabelUserInfo
            // 
            this.toolStripLabelUserInfo.Name = "toolStripLabelUserInfo";
            this.toolStripLabelUserInfo.Size = new System.Drawing.Size(72, 21);
            this.toolStripLabelUserInfo.Text = "Иванов В.В";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 24);
            // 
            // toolStripButtonChangeUser
            // 
            this.toolStripButtonChangeUser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonChangeUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonChangeUser.Name = "toolStripButtonChangeUser";
            this.toolStripButtonChangeUser.RightToLeftAutoMirrorImage = true;
            this.toolStripButtonChangeUser.Size = new System.Drawing.Size(61, 21);
            this.toolStripButtonChangeUser.Text = "Сменить";
            this.toolStripButtonChangeUser.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.toolStripButtonChangeUser.Click += new System.EventHandler(this.toolStripButtonChangeUser_Click);
            // 
            // toolStripButtonLogOut
            // 
            this.toolStripButtonLogOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonLogOut.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLogOut.Image")));
            this.toolStripButtonLogOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLogOut.Name = "toolStripButtonLogOut";
            this.toolStripButtonLogOut.Size = new System.Drawing.Size(50, 21);
            this.toolStripButtonLogOut.Text = "Выйти";
            this.toolStripButtonLogOut.Visible = false;
            this.toolStripButtonLogOut.Click += new System.EventHandler(this.toolStripButtonLogOut_Click);
            // 
            // zdButton1
            // 
            this.zdButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zdButton1.BackColor = System.Drawing.Color.AliceBlue;
            this.zdButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdButton1.Location = new System.Drawing.Point(3, 21);
            this.zdButton1.MinimumSize = new System.Drawing.Size(0, 39);
            this.zdButton1.Name = "zdButton1";
            this.zdButton1.Size = new System.Drawing.Size(136, 68);
            this.zdButton1.TabIndex = 7;
            this.zdButton1.Text = "Автоматический режим";
            this.zdButton1.UseVisualStyleBackColor = true;
            // 
            // zdButton2
            // 
            this.zdButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zdButton2.BackColor = System.Drawing.Color.AliceBlue;
            this.zdButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdButton2.Location = new System.Drawing.Point(3, 95);
            this.zdButton2.MinimumSize = new System.Drawing.Size(0, 39);
            this.zdButton2.Name = "zdButton2";
            this.zdButton2.Size = new System.Drawing.Size(136, 68);
            this.zdButton2.TabIndex = 8;
            this.zdButton2.Text = "Ручной режим";
            this.zdButton2.UseVisualStyleBackColor = true;
            // 
            // zdButton3
            // 
            this.zdButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zdButton3.BackColor = System.Drawing.Color.AliceBlue;
            this.zdButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdButton3.Location = new System.Drawing.Point(3, 169);
            this.zdButton3.MinimumSize = new System.Drawing.Size(0, 39);
            this.zdButton3.Name = "zdButton3";
            this.zdButton3.Size = new System.Drawing.Size(136, 68);
            this.zdButton3.TabIndex = 9;
            this.zdButton3.Text = "График";
            this.zdButton3.UseVisualStyleBackColor = true;
            // 
            // zdButton4
            // 
            this.zdButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zdButton4.BackColor = System.Drawing.Color.AliceBlue;
            this.zdButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdButton4.Location = new System.Drawing.Point(145, 21);
            this.zdButton4.MinimumSize = new System.Drawing.Size(0, 39);
            this.zdButton4.Name = "zdButton4";
            this.zdButton4.Size = new System.Drawing.Size(136, 68);
            this.zdButton4.TabIndex = 10;
            this.zdButton4.Text = "Выход в 0";
            this.zdButton4.UseVisualStyleBackColor = true;
            // 
            // zdButton5
            // 
            this.zdButton5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zdButton5.BackColor = System.Drawing.Color.AliceBlue;
            this.zdButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdButton5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdButton5.Location = new System.Drawing.Point(145, 95);
            this.zdButton5.MinimumSize = new System.Drawing.Size(0, 39);
            this.zdButton5.Name = "zdButton5";
            this.zdButton5.Size = new System.Drawing.Size(136, 68);
            this.zdButton5.TabIndex = 11;
            this.zdButton5.Text = "Датчики";
            this.zdButton5.UseVisualStyleBackColor = true;
            // 
            // zdButton6
            // 
            this.zdButton6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zdButton6.BackColor = System.Drawing.Color.AliceBlue;
            this.zdButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdButton6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdButton6.Location = new System.Drawing.Point(145, 169);
            this.zdButton6.MinimumSize = new System.Drawing.Size(0, 39);
            this.zdButton6.Name = "zdButton6";
            this.zdButton6.Size = new System.Drawing.Size(136, 68);
            this.zdButton6.TabIndex = 12;
            this.zdButton6.Text = "Параметры";
            this.zdButton6.UseVisualStyleBackColor = true;
            // 
            // zdLabel1
            // 
            this.zdLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zdLabel1.AutoSize = true;
            this.zdLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdLabel1.Location = new System.Drawing.Point(3, 0);
            this.zdLabel1.Name = "zdLabel1";
            this.zdLabel1.Size = new System.Drawing.Size(136, 18);
            this.zdLabel1.TabIndex = 13;
            this.zdLabel1.Text = "Главная";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 430);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ZDButton zdButton1;
        private Controls.ZDButton zdButton2;
        private Controls.ZDButton zdButton3;
        private Controls.ZDButton zdButton4;
        private Controls.ZDButton zdButton5;
        private Controls.ZDButton zdButton6;
        private Controls.ZDLabel zdLabel1;
        private Controls.ZDButton zdButton14;
        private Controls.ZDButton zdButton13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private Controls.ZDToggleButton zdToggleButton1;
        private Controls.ZDButton zdButton9;
        private Controls.ZDButton zdButton7;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripLabel toolStripLabelUserInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonChangeUser;
        private System.Windows.Forms.ToolStripButton toolStripButtonLogOut;
    }
}