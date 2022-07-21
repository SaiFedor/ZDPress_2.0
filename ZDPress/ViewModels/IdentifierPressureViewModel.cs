using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDPress.Opc;
using System.Diagnostics;

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
            _paramNames = new List<string>
            {
                //OpcConsts.DispPress1,
                //OpcConsts.DispPress2,
                //OpcConsts.DispPress3,
                //OpcConsts.AlarmBP1
                //,OpcConsts.Bits
            };

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
            //OpcResponderSingleton.Instance.ViewItems(_paramNames);
            // Test DEV
            List<OpcParameter> parameters = OpcResponderSingleton.Instance.ProcessParameters(OpcResponderSingleton.Instance.Parameters);

            DispPress1 = parameters.Any(p => p.ParameterName == OpcConsts.DispPress1) ? Convert.ToInt32(parameters.First(p => p.ParameterName == OpcConsts.DispPress1).ParameterValue) : 0;

            DispPress2 = parameters.Any(p => p.ParameterName == OpcConsts.DispPress2) ? Convert.ToInt32(parameters.First(p => p.ParameterName == OpcConsts.DispPress2).ParameterValue) : 0;

            DispPress3 = parameters.Any(p => p.ParameterName == OpcConsts.DispPress3) ? Convert.ToInt32(parameters.First(p => p.ParameterName == OpcConsts.DispPress3).ParameterValue) : 0;

            AlarmBP1 = parameters.Any(p => p.ParameterName == OpcConsts.AlarmBP1) && Convert.ToInt32(parameters.First(p => p.ParameterName == OpcConsts.AlarmBP1).ParameterValue) == 1 ? OpcConsts.AlarmText : string.Empty;

            AlarmBP2 = parameters.Any(p => p.ParameterName == OpcConsts.AlarmBP2) && Convert.ToInt32(parameters.First(p => p.ParameterName == OpcConsts.AlarmBP2).ParameterValue) == 1 ? OpcConsts.AlarmText : string.Empty;

            AlarmBP3 = parameters.Any(p => p.ParameterName == OpcConsts.AlarmBP3) && Convert.ToInt32(parameters.First(p => p.ParameterName == OpcConsts.AlarmBP3).ParameterValue) == 1 ? OpcConsts.AlarmText : string.Empty;
            /*
            int bits = parameters.Any(p => p.ParameterName == OpcConsts.Bits) ? Convert.ToInt32(parameters.First(p => p.ParameterName == OpcConsts.Bits).ParameterValue) : 0;


            BitParameters bp = (BitParameters)bits;

            
            AlarmBP1 = bp.HasFlag(BitParameters.AlarmDispPress1) ? "Ошибка" : string.Empty;


            DispPress2Err = bp.HasFlag(BitParameters.AlarmDispPress2) ? "Ошибка" : string.Empty;


            DispPress3Err = bp.HasFlag(BitParameters.AlarmDispPress3) ? "Ошибка" : string.Empty;
            */

            Trace.WriteLine(string.Format("DispPress1:{0}, DispPress2:{1}, DispPress3:{2}", DispPress1, DispPress2, DispPress3));
        }
    }
}
