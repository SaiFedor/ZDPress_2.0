using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZDPress.UI.Reports
{
    public class ReportParameterAttribute : Attribute
    {
        public string ParameterName { get; private set; }
        public ReportParameterAttribute(string parameterName)
        {
           this.ParameterName = parameterName;
        }
    }
}
