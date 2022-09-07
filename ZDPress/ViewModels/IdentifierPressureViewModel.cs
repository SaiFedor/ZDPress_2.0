using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using ZDPress.Opc;
using System.Drawing;

namespace ZDPress.UI.ViewModels
{
    public class IdentifierPressureViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName) 
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private System.Drawing.Color _plcConnectState;
        public System.Drawing.Color PlcConnectState
        {
            get
            {
                return _plcConnectState;
            }
            set
            {
                if (_plcConnectState != value)
                {
                    _plcConnectState = value;
                    OnPropertyChanged("PlcConnectState");
                }
            }
        }

        private int _DispPress1;
        public int DispPress1
        {
            get
            {
                return _DispPress1;
            }
            set
            {
                if (_DispPress1 != value)
                {
                    _DispPress1 = value;
                    OnPropertyChanged("DispPress1");
                }
            }
        }



        private int _DispPress2;
        public int DispPress2
        {
            get
            {
                return _DispPress2;
            }
            set
            {
                if (_DispPress2 != value)
                {
                    _DispPress2 = value;
                    OnPropertyChanged("DispPress2");
                }
            }
        }

        private int _DispPress3;
        public int DispPress3
        {
            get
            {
                return _DispPress3;
            }
            set
            {
                if (_DispPress3 != value)
                {
                    _DispPress3 = value;
                    OnPropertyChanged("DispPress3");
                }
            }
        }


        private string _AlarmBP1;
        public string AlarmBP1
        {
            get
            {
                return _AlarmBP1;
            }
            set
            {
                if (_AlarmBP1 != value)
                {
                    _AlarmBP1 = value;
                    OnPropertyChanged("AlarmBP1");
                }
            }
        }

        private string _AlarmBP2;
        public string AlarmBP2
        {
            get
            {
                return _AlarmBP2;
            }
            set
            {
                if (_AlarmBP2 != value)
                {
                    _AlarmBP2 = value;
                    OnPropertyChanged("AlarmBP2");
                }
            }
        }


        private string _AlarmBP3;
        public string AlarmBP3
        {
            get
            {
                return _AlarmBP3;
            }
            set
            {
                if (_AlarmBP3 != value)
                {
                    _AlarmBP3 = value;
                    OnPropertyChanged("AlarmBP3");
                }
            }
        }



        private List<string> _paramNames;
        public IdentifierPressureViewModel() 
        {
           
            _TimerForUpdate = new Timer();
            _TimerForUpdate.Tick += _TimerForUpdate_Tick;
        }

        void _TimerForUpdate_Tick(object sender, EventArgs e)
        {
            UpdateParameters(); 
        }

        private Timer _TimerForUpdate;


        public void RunAutoUpdateParameters(int interval) 
        {
            _TimerForUpdate.Interval = interval;

            if (!_TimerForUpdate.Enabled)
            {
                _TimerForUpdate.Start();
            }
        }

        public void StopAutoUpdateParameters()
        {
            if (_TimerForUpdate.Enabled)
            {
                _TimerForUpdate.Stop();
            }
        }

        private void UpdateParameters()
        {
            
            DispPress1 = Convert.ToInt32(Tags.DispPress1.Value);

            DispPress2 = Convert.ToInt32(Tags.DispPress2.Value);

            DispPress3 = Convert.ToInt32(Tags.DispPress3.Value);

            if (MainConstant.plc.client != null ? MainConstant.plc.client.Connected : false)
            {
                PlcConnectState = Color.Green;
            }
            else
            {
                PlcConnectState = Color.Red;
            }


            Trace.WriteLine(string.Format("DispPress1:{0}, DispPress2:{1}, DispPress3:{2}", DispPress1, DispPress2, DispPress3));
        }
    }
}
