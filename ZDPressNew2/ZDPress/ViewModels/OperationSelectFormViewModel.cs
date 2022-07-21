
using System.ComponentModel;

namespace ZDPress.UI.ViewModels
{
    public class OperationSelectFormViewModel  : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool LeftWheel { get; set; }

        public bool RightWheel { get; set; }

        public bool AxisDimens { get; set; }


    }
}
