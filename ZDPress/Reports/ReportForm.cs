using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Reflection;
using System.Configuration;

namespace ZDPress.UI.Reports
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }


        public ReportDto ReportDto { get; set; }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            string rdlcPath = ConfigurationManager.AppSettings["ReportPath"];

            reportViewer1.LocalReport.ReportPath = rdlcPath;

            //reportViewer1.PrinterSettings.DefaultPageSettings.PaperSize = System.Drawing.Printing.PaperSize()

            SetReportParameters();
            
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            
            this.reportViewer1.RefreshReport();
        }

        private void SetReportParameters()
        {
            ReportDto.ChartDataMimeType = "image/png";


            var props = ReportDto.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                bool isDefined = Attribute.IsDefined(prop, typeof(ReportParameterAttribute));
                if (!isDefined)
                {
                    continue;
                }
                ReportParameterAttribute attr = (ReportParameterAttribute)prop.GetCustomAttributes(typeof(ReportParameterAttribute), false).First();
                string value = Convert.ToString(prop.GetValue(ReportDto, null));
                SetReportParametersInternal(attr.ParameterName, value);
            }
        }

        private void SetReportParametersInternal(string paramName, string paramValue)
        {
            ReportParameter rp = new ReportParameter();
            rp.Name = paramName;
            rp.Values.Add(paramValue);
            reportViewer1.LocalReport.SetParameters(rp);
        }
    }
}
