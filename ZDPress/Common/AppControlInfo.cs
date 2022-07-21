using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDPress.UI.Common
{
    public class AppControlInfo
    {
        public string ParentName { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsReadOnly { get; set; }
    }
}
