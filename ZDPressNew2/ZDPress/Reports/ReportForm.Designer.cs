using System.Drawing.Printing;
using System.Linq;
namespace ZDPress.UI.Reports
{
    partial class ReportForm
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(751, 408);
            this.reportViewer1.TabIndex = 0;

            PageSettings pageSettings = new PageSettings();
            PrinterSettings printerSetting = new PrinterSettings();
            IQueryable<PaperSize> paperSizes = printerSetting.PaperSizes.Cast<PaperSize>().AsQueryable();

            PaperSize a4 = paperSizes.Where(paperSize => paperSize.Kind == PaperKind.A4).FirstOrDefault();
        
            pageSettings.PrinterSettings.DefaultPageSettings.PaperSize = a4;
            pageSettings.Margins.Bottom = 0;
            pageSettings.Margins.Left = 0;
            pageSettings.Margins.Right = 0;
            pageSettings.Margins.Top = 30;

  
            this.reportViewer1.SetPageSettings(pageSettings);

            var defSize = reportViewer1.PrinterSettings.DefaultPageSettings;


            
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 408);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReportForm";
            this.Text = "ReportForm";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }


}