namespace ZDPress.UI.Views
{
    partial class FunctionSelectForm
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
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.zdButton1 = new ZDPress.UI.Controls.ZDButton();
            this.zdButton2 = new ZDPress.UI.Controls.ZDButton();
            this.zdButton3 = new ZDPress.UI.Controls.ZDButton();
            this.zdLabel1 = new ZDPress.UI.Controls.ZDLabel();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.zdButton1, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.zdButton2, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.zdButton3, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.zdLabel1, 0, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(284, 261);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // zdButton1
            // 
            this.zdButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zdButton1.BackColor = System.Drawing.Color.AliceBlue;
            this.zdButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdButton1.Location = new System.Drawing.Point(3, 161);
            this.zdButton1.MinimumSize = new System.Drawing.Size(0, 69);
            this.zdButton1.Name = "zdButton1";
            this.zdButton1.Size = new System.Drawing.Size(136, 69);
            this.zdButton1.TabIndex = 0;
            this.zdButton1.Text = "Колесо";
            this.zdButton1.UseVisualStyleBackColor = false;
            this.zdButton1.Click += new System.EventHandler(this.zdButton1_Click);
            // 
            // zdButton2
            // 
            this.zdButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zdButton2.BackColor = System.Drawing.Color.AliceBlue;
            this.zdButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdButton2.Location = new System.Drawing.Point(145, 161);
            this.zdButton2.MinimumSize = new System.Drawing.Size(0, 69);
            this.zdButton2.Name = "zdButton2";
            this.zdButton2.Size = new System.Drawing.Size(136, 69);
            this.zdButton2.TabIndex = 1;
            this.zdButton2.Text = "Томозной диск";
            this.zdButton2.UseVisualStyleBackColor = false;
            this.zdButton2.Click += new System.EventHandler(this.zdButton2_Click);
            // 
            // zdButton3
            // 
            this.zdButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zdButton3.BackColor = System.Drawing.Color.AliceBlue;
            this.zdButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdButton3.Location = new System.Drawing.Point(145, 30);
            this.zdButton3.MinimumSize = new System.Drawing.Size(0, 69);
            this.zdButton3.Name = "zdButton3";
            this.zdButton3.Size = new System.Drawing.Size(136, 69);
            this.zdButton3.TabIndex = 2;
            this.zdButton3.Text = "Назад";
            this.zdButton3.UseVisualStyleBackColor = false;
            this.zdButton3.Click += new System.EventHandler(this.zdButton3_Click);
            // 
            // zdLabel1
            // 
            this.zdLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zdLabel1.AutoSize = true;
            this.zdLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdLabel1.Location = new System.Drawing.Point(3, 43);
            this.zdLabel1.Name = "zdLabel1";
            this.zdLabel1.Padding = new System.Windows.Forms.Padding(29, 0, 0, 0);
            this.zdLabel1.Size = new System.Drawing.Size(136, 44);
            this.zdLabel1.TabIndex = 3;
            this.zdLabel1.Text = "Выбор функции";
            // 
            // FunctionSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FunctionSelectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private Controls.ZDButton zdButton1;
        private Controls.ZDButton zdButton2;
        private Controls.ZDButton zdButton3;
        private Controls.ZDLabel zdLabel1;
    }
}