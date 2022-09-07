using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ZDPress.Dal.Entities
{
    /// <summary>
    /// Операция пресования.
    /// </summary>
    public class PressOperation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName) 
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool IsNew
        {
            get { return Id == 0; }
        }
        /// <summary>
        /// Ид.
        /// </summary>
        public Int64 Id { get; set; }
        /// <summary>
        /// Номер завода.
        /// </summary>
        public string FactoryNumber { get; set; }
        /// <summary>
        /// дата и время начала пресования
        /// </summary>
        public DateTime OperationStart { get; set; }
        /// <summary>
        /// дата и время конца пресования
        /// </summary>
        public DateTime? OperationStop { get; set; }
        /// <summary>
        /// Номер колеса вводится пользователем
        /// </summary>
        public string WheelNumber { get; set; }
        /// <summary>
        /// Номер диаграммы	вводится пользователем
        /// </summary>
        public string DiagramNumber { get; set; }
        /// <summary>
        /// Номер оси вводится пользователем
        /// </summary>
        public string AxisNumber { get; set; }
        /// <summary>
        /// Тип колёсной пары вводится пользователем
        /// </summary>
        public string WheelType { get; set; }
        /// <summary>
        /// Сторона вводится пользователем
        /// </summary>
        public string Side { get; set; }
        /// <summary>
        /// Диаметр подст. Части вводится пользователем
        /// </summary>
        private decimal _DWheel { get; set; }
        public decimal DWheel 
        {
            get { return _DWheel; }
            set
            {
                if (value != _DWheel)
                {
                    _DWheel = value;
                }
            }
        }
        /// <summary>
        /// Диаметр отв. Ступицы. вводится пользователем
        /// </summary>
        public decimal DAxis { get; set; }
        /// <summary>
        /// Длина ступицы вводится пользователем
        /// </summary>
        public int LengthStup { get; set; }
        /// <summary>
        /// Натяг (рассчитывается)
        /// </summary>
        public decimal Natiag 
        {
            get { return DWheel - DAxis; }
        }
        /// <summary>
        /// Длина сопряжения (рассчитывается)
        /// </summary>
        public int LengthSopriazh { get; set; }

        /// <summary>
        /// Усилие запрессовки на 100мм	(рассчитывается)
        /// [Максимальное усилие запрессовки] / [Диаметр подступичной части]) * [100].                                                                                                                               
        /// </summary>
        public decimal Power100Mm
        {
            get
            {
                var res = DWheel != 0 ? ((MaxPower / DWheel) * 100): 0;
                return res;
            }
        }

        /// <summary>
        /// Макс. Усилие запрессовки (рассчитывается)
        /// </summary>
        private decimal _maxPower;
        public decimal MaxPower
        {
            get { return _maxPower; }
            set { _maxPower = value; OnPropertyChanged("MaxPower"); }
        }
        /// <summary>
        /// Длина прямых участков рассчитывается
        /// </summary>
        public int LengthLines { get; set; }


        public int TotalRows { get; set; } 

        public BindingList<PressOperationData> PressOperationData;

    }
}
