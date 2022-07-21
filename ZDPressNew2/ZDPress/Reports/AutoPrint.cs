using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZDPress.UI.Reports
{
    public class AutoPrint : IDisposable
    {
        private int _mCurrentPageIndex;

        private IList<Stream> _mStreams;

        public ReportDto ReportDto { get; set; }

        public void Dispose()
        {
            if (_mStreams == null) return;

            foreach (Stream stream in _mStreams)
            {
                stream.Close();
            }

            _mStreams = null;
        }


        public AutoPrint() 
        {
        }

        public AutoPrint(ReportDto reportDto)
        {
            ReportDto = reportDto;
        }

        // Create a local report for Report.rdlc, load the data,
        //    export the report to an .emf file, and print it.
        public void Run()
        {
            string reportPath = ConfigurationManager.AppSettings["ReportPath"];

            LocalReport report = new LocalReport { ReportPath = reportPath };

            SetReportParameters(report);

            Export(report);

            Print();
        }

        private void SetReportParameters(LocalReport report)
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

                SetReportParametersInternal(report, attr.ParameterName, value);
            }
        }

        private void SetReportParametersInternal(LocalReport report, string paramName, string paramValue)
        {
            ReportParameter rp = new ReportParameter();
            rp.Name = paramName;
            rp.Values.Add(paramValue);
            report.SetParameters(rp);
        }


        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.05in</PageWidth>
                <PageHeight>5.70in</PageHeight>
                <MarginTop>0.0in</MarginTop>
                <MarginLeft>0.0in</MarginLeft>
                <MarginRight>0in</MarginRight>
                <MarginBottom>0in</MarginBottom>
            </DeviceInfo>";

            Warning[] warnings;

            _mStreams = new List<Stream>();

            report.Render("Image", deviceInfo, CreateStream, out warnings);

            foreach (Stream stream in _mStreams)
            {
                stream.Position = 0;
            }
        }


        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();

            _mStreams.Add(stream);

            return stream;
        }


        private void Print()
        {
            if (_mStreams == null || _mStreams.Count == 0)
            {
                throw new Exception("Error: no stream to print.");
            }

            
            string printerName = ConfigurationManager.AppSettings["PrinterName"];

            PrintDocument printDoc = new PrintDocument();

            PrinterSettings printerSettings = new PrinterSettings
            {
                PrinterName = printerName
            };

            printDoc.PrinterSettings = printerSettings;

            
                if (printDoc.PrinterSettings.IsValid)
                {
                    printDoc.PrintPage += PrintPage;

                    _mCurrentPageIndex = 0;

                    printDoc.Print();
                }
                else
                {
                    MessageBox.Show(@"Ошибка печати! Принтер не найден!");
                }                
        }


        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(_mStreams[_mCurrentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            _mCurrentPageIndex++;

            ev.HasMorePages = _mCurrentPageIndex < _mStreams.Count;
        }
    }
}
