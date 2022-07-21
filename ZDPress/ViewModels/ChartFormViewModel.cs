using System.ComponentModel;
using ZDPress.Dal;
using ZDPress.Dal.Entities;

namespace ZDPress.UI.ViewModels
{
    public class ChartFormViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool _canSaveOperation;
        public bool CanSaveOperation
        {
            get
            {
                return _canSaveOperation;
            }
            set
            {
                if (_canSaveOperation != value)
                {
                    _canSaveOperation = value;
                    OnPropertyChanged("CanSaveOperation");
                }

            }
        }

        public ZdPressDal Dal { get; set; }

        public PressOperation PressOperation { get; set; }

        public ChartFormViewModel() 
        {
            Dal = new ZdPressDal();
        }

        public string ImageAsBase64 { get; set; }

        public string ChartDataMimeType { get; set; }

        public void UpdateMaxSoprFromDb(int operationId) 
        {
            PressOperation.LengthSopriazh = Dal.GetMaxSopr(operationId);
        }


        public void UpdateMaxUsilZapreFromDb(int operationId)
        {
            PressOperation.MaxPower = Dal.GetMaxZapress(operationId);
        }


        public void UpdateDlinaPramUchFromDb(int operationId)
        {
            PressOperation.LengthLines = Dal.GetDlinaPramUch(operationId);
        }


    }
}
