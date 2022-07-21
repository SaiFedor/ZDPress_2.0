using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZDPress.UI.Reports
{
    public class ReportDto
    {
        [ReportParameterAttribute("ChartData")]
        public string ImageAsBase64 { get; set; }

        [ReportParameterAttribute("ChartDataMimeType")]
        public string ChartDataMimeType { get; set; }

        [ReportParameterAttribute("NomerDiag")]
        public string NomerDiag { get; set; }

        [ReportParameterAttribute("NomerZavoda")]
        public string NomerZavoda { get; set; }

        [ReportParameterAttribute("NomerOsi")]
        public string NomerOsi { get; set; }

        [ReportParameterAttribute("TipKolesPar")]
        public string TipKolesPar { get; set; }

        [ReportParameterAttribute("Storona")]
        public string Storona { get; set; }

        [ReportParameterAttribute("NomerKolesa")]
        public string NomerKolesa { get; set; }

        [ReportParameterAttribute("DiametrPodsChasti")]
        public string DiametrPodsChasti { get; set; }

        [ReportParameterAttribute("DiametrOtvStupici")]
        public string DiametrOtvStupici { get; set; }

        [ReportParameterAttribute("DlinaStupici")]
        public string DlinaStupici { get; set; }

        [ReportParameterAttribute("Natag")]
        public string Natag { get; set; }

        [ReportParameterAttribute("DlinaSoprag")]
        public string DlinaSoprag { get; set; }

        [ReportParameterAttribute("UsilZapres100")]
        public string UsilZapres100 { get; set; }

        [ReportParameterAttribute("MaxUsilZapres")]
        public string MaxUsilZapres { get; set; }

        [ReportParameterAttribute("DlinaPramUch")]
        public string DlinaPramUch { get; set; }
    }
}
