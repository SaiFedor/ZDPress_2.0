using System;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using ZDPress.UI.Common;

namespace ZDPress.UI
{
    public static class Program
    {
        public static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        public static Common.AppContext PressContext { get; set; }

        public static OpcLayer OpcLayer { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            PressContext = new Common.AppContext
            {
                CurrentUser = new AppUser
                {
                    Login = AppUser.User,
                    Role = ControlBasedRightsManager.InitializeRoleByLogin(AppUser.User)
                }
            };

            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);

            XmlConfigurator.Configure();

            OpcLayer = new OpcLayer();

            OpcLayer.StartWork();

            Application.Run(new ShellForm());
        }
    }
}
