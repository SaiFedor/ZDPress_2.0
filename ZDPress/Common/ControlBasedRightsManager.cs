using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ZDPress.UI.Common
{
    public class ControlBasedRightsManager
    {
        /// <summary>
        /// По имени роли вернет роль с разрешениями.
        /// </summary>
        /// <param name="login"></param>
        /// <returns>AppRole</returns>
        public static AppRole InitializeRoleByLogin(string login)
        {
            if (login == AppUser.User)
            {
                return new AppRole()
                {
                    Name = AppUser.User,
                    AppControls = new List<AppControlInfo>()
                    {
                        new AppControlInfo()
                        {
                            ParentName = "panelRight",
                            Name = "textBoxNomerDiagrammi",
                            IsReadOnly = true
                        },
                        new AppControlInfo()
                        {
                            ParentName = "panelRight",
                            Name = "textBoxNomerZavoda",
                            IsReadOnly = true
                        },
                        new AppControlInfo()
                        {
                            ParentName = "panelRight",
                            Name = "textBoxNomerOsi",
                            IsReadOnly = true
                        },
                         new AppControlInfo()
                        {
                            ParentName = "panelRight",
                            Name = "comboBoxTipColesPary",
                            IsDisabled = true
                        },
                         new AppControlInfo()
                        {
                            ParentName = "panelRight",
                            Name = "comboBoxStorona",
                            IsDisabled = true
                        },
                        new AppControlInfo()
                        {
                            ParentName = "panelRight",
                            Name = "textBoxNomerKlesa",
                            IsReadOnly = true
                        },
                         new AppControlInfo()
                        {
                            ParentName = "panelBottom",
                            Name = "textBoxPods",
                            IsReadOnly = true
                        },

                           new AppControlInfo()
                        {
                            ParentName = "panelBottom",
                            Name = "textBoxOtv",
                            IsReadOnly = true
                        },
                           new AppControlInfo()
                        {
                            ParentName = "panelBottom",
                            Name = "textBoxDlin",
                            IsReadOnly = true
                        },
                              new AppControlInfo()
                        {
                            ParentName = "panelBottom",
                            Name = "textBoxNatag",
                            IsReadOnly = true
                        },
                         new AppControlInfo()
                        {
                            ParentName = "panelBottom",
                            Name = "textBoxSopr",
                            IsReadOnly = true
                        },
                    }
                };
            }

           return null;
        }

        private  static readonly List<string> ExcludeControlNames = new List<string>()
        {
            "textBoxNatag", "textBoxSopr", "textBoxUsil100", "textBoxMaxUsil", "textBoxDlinaPramUch"
        };

        public static void ConfigureControlsByRole(Form form, AppRole appRole)
        {
            List<AppControlInfo> toHide = new List<AppControlInfo>();

            List<AppControlInfo> toDisable = new List<AppControlInfo>();

            List<AppControlInfo> toReadOnly = new List<AppControlInfo>();

            if (appRole != null)
            {
                string controlHolderName = form.Name;

                IEnumerable<AppControlInfo> controls = appRole.AppControls.Where(appControl => string.Compare(appControl.ParentName, controlHolderName, StringComparison.CurrentCultureIgnoreCase) == 0);

                foreach (AppControlInfo appControl in controls)
                {
                    if (appControl.IsHidden)
                    {
                        toHide.Add(appControl);
                    }

                    if (appControl.IsDisabled)
                    {
                        toDisable.Add(appControl);
                    }

                    if (appControl.IsReadOnly)
                    {
                        toReadOnly.Add(appControl);
                    }
                }
            }

            ConfigFormRecursive(form.Controls, toHide, toDisable, toReadOnly, appRole);
        }

        private static void ConfigFormRecursive(IEnumerable controls, List<AppControlInfo> toHide, List<AppControlInfo> toDisable, List<AppControlInfo> toReadOnly, AppRole appRole)
        {
            if (controls == null) throw new ArgumentNullException("controls");

            foreach (Control control in controls.Cast<Control>().Where(control => !SkipControl(control)))
            {
                RestoreStateControl(control);

                ApplyRigths(toHide, toDisable, toReadOnly, appRole, control);
            }
        }

        private static void ApplyRigths(List<AppControlInfo> toHide, List<AppControlInfo> toDisable, List<AppControlInfo> toReadOnly, AppRole appRole, Control control)
        {
            foreach (AppControlInfo toHideItem in toHide)
            {
                bool namesEqual = string.Compare(control.Name, toHideItem.Name, StringComparison.CurrentCultureIgnoreCase) == 0;

                if (!namesEqual) continue;
                
                control.Visible = false;
            }

            foreach (AppControlInfo toHideItem in toDisable)
            {
                bool namesEqual = string.Compare(control.Name, toHideItem.Name, StringComparison.CurrentCultureIgnoreCase) == 0;

                if (!namesEqual) continue;

                control.Enabled = false;
            }

            TextBox textBox = (control as TextBox);

            MaskedTextBox maskedTextBox = (control as MaskedTextBox);

            if (textBox != null || maskedTextBox != null)
            {
                foreach (AppControlInfo toHideItem in toReadOnly)
                {

                    bool namesEqual = string.Compare(control.Name, toHideItem.Name, StringComparison.CurrentCultureIgnoreCase) == 0;

                    if (!namesEqual) continue;

                    if (textBox != null)
                    {
                        textBox.ReadOnly = true;
                    }

                    if (maskedTextBox != null)
                    {
                        maskedTextBox.ReadOnly = true;
                    }
                }
            }



            if (control.HasChildren)
            {
                toHide = appRole == null
                    ? new List<AppControlInfo>()
                    : appRole.AppControls.Where(
                        appControl =>
                            string.Compare(appControl.ParentName, control.Name, StringComparison.CurrentCultureIgnoreCase) == 0 &&
                            appControl.IsHidden).ToList();

                toDisable = appRole == null
                    ? new List<AppControlInfo>()
                    : appRole.AppControls.Where(
                        appControl =>
                            string.Compare(appControl.ParentName, control.Name, StringComparison.CurrentCultureIgnoreCase) == 0 &&
                            appControl.IsDisabled).ToList();

                toReadOnly = appRole == null
                    ? new List<AppControlInfo>()
                    : appRole.AppControls.Where(
                        appControl =>
                            string.Compare(appControl.ParentName, control.Name, StringComparison.CurrentCultureIgnoreCase) == 0 &&
                            appControl.IsReadOnly).ToList();


                ConfigFormRecursive(control.Controls, toHide, toDisable, toReadOnly, appRole);
            }
        }

        private static void RestoreStateControl(Control control)
        {
            control.Visible = true;

            control.Enabled = true;

            TextBox textBox = control as TextBox;

            MaskedTextBox maskedTextBox = control as MaskedTextBox;

            if (textBox != null || maskedTextBox != null)
            {
                if (textBox != null)
                {
                    textBox.ReadOnly = false;
                }

                if (maskedTextBox != null)
                {
                    maskedTextBox.ReadOnly = false;
                }
            }
        }

        private static bool SkipControl(Control control)
        {
            bool isInExcludeNames = ExcludeControlNames.Any(c => c == control.Name);

            return isInExcludeNames;
        }
    }
}
