using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZDPress.Dal.Entities;
using ZDPress.Opc;

namespace ZDPress.UI.ViewModels
{
    public class ParametersFormViewModel : INotifyPropertyChanged
    {

        private int _speedPress;


        public int EmphasisPlunger { get; set; }

        public int EmphasisTravers { get; set; }

        public int Instrument { get; set; }

        public int WheelPosition { get; set; }

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

        public int SpeedPress
        {
            get
            {
                return _speedPress;
            }
            set
            {
                if (_speedPress != value)
                {
                    _speedPress = value;
                    OnPropertyChanged("SpeedPress");
                }
            }
        }

        private int _maxPress;


        public int MaxPress
        {
            get
            {
                return _maxPress;
            }
            set
            {
                if (_maxPress != value)
                {
                    _maxPress = value;
                    OnPropertyChanged("MaxPress");
                }
            }
        }

     
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public void SaveParameters()

        {

            short MaxPressRes;
            short speedPressRes;
        
            if (short.TryParse(SpeedPress.ToString(), out speedPressRes))
            {
                Tags.SpeedPress.Write(speedPressRes);
            }

            if (short.TryParse(MaxPress.ToString(), out MaxPressRes))
            {
                Tags.SetMaxPress.Write(MaxPressRes);
            }
        }

        public ParametersFormViewModel()
        {
            SpeedPress = Convert.ToInt32(Tags.SpeedPress.Value);
            MaxPress = Convert.ToInt32(Tags.SetMaxPress.Value);
            _TimerForUpdate = new Timer();
            _TimerForUpdate.Tick += _TimerForUpdate_Tick;
        }

        public event PropertyChangedEventHandler PropertyChanged;


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

            if (MainConstant.plc.client != null ? MainConstant.plc.client.Connected : false)
            {
                PlcConnectState = Color.Green;
            }
            else
            {
                PlcConnectState = Color.Red;
            }
        }
    }
}
