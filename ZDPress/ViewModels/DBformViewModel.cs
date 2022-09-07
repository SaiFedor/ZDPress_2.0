using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZDPress.Dal.Entities;
using ZDPress.Opc;

namespace ZDPress.UI.ViewModels
{
    public class DBformViewModel : INotifyPropertyChanged
    {

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

        private DateTime _startCleareDate;
        public DateTime StartCleareDate
        {
            get
            {
                return _startCleareDate;
            }
            set
            {
                if (_startCleareDate != value)
                {
                    _startCleareDate = value;
                    OnPropertyChanged("StartCleareDate");
                }
            }
        }

        private DateTime _endClearDate;
        public DateTime EndClearDate
        {
            get
            {
                return _endClearDate;
            }
            set
            {
                if (_endClearDate != value)
                {
                    _endClearDate = value;
                    OnPropertyChanged("EndClearDate");
                }
            }
        }

        private DateTime _startArchiveDate;
        public DateTime StartArchiveDate
        {
            get
            {
                return _startArchiveDate;
            }
            set
            {
                if (_startArchiveDate != value)
                {
                    _startArchiveDate = value;
                    OnPropertyChanged("StartArchiveDate");
                }
            }
        }

        private DateTime _endArchiveDate;
        public DateTime EndArchiveDate
        {
            get
            {
                return _endArchiveDate;
            }
            set
            {
                if (_endArchiveDate != value)
                {
                    _endArchiveDate = value;
                    OnPropertyChanged("EndArchiveDate");
                }
            }
        }

        private string _saveFilePath;
        public string saveFilePath
        {
            get
            {
                return _saveFilePath;
            }
            set
            {
                if (_saveFilePath != value)
                {
                    _saveFilePath = value;
                    OnPropertyChanged("SaveFilePath");
                }
            }
        }

        private string _restoreFilePath;
        public string RestoreFilePath
        {
            get
            {
                return _restoreFilePath;
            }
            set
            {
                if (_restoreFilePath != value)
                {
                    _restoreFilePath = value;
                    OnPropertyChanged("RestoreFilePath");
                }
            }
        }

        private string _registerFilePath;
        public string RegisterFilePath
        {
            get
            {
                return _registerFilePath;
            }
            set
            {
                if (_registerFilePath != value)
                {
                    _registerFilePath = value;
                    OnPropertyChanged("RegisterFilePath");
                }
            }
        }

        private string _passportFilePath;
        public string PassportFilePath
        {
            get
            {
                return _passportFilePath;
            }
            set
            {
                if (_passportFilePath != value)
                {
                    _passportFilePath = value;
                    OnPropertyChanged("PassportFilePath");
                }
            }
        }

        private string _registerArchivePath;
        public string RegisterArchivePath
        {
            get
            {
                return _registerArchivePath;
            }
            set
            {
                if (_registerArchivePath != value)
                {
                    _registerArchivePath = value;
                    OnPropertyChanged("RegisterArchivePath");
                }
            }
        }

        public DBformViewModel()
        {
            _TimerForUpdate = new Timer();
            _TimerForUpdate.Tick += _TimerForUpdate_Tick;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
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
