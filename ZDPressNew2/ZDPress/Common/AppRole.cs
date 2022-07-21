using System.Collections.Generic;

namespace ZDPress.UI.Common
{
    public class AppRole
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<AppControlInfo> AppControls { get; set; }
    }
}
