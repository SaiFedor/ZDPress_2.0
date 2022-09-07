using System;
using System.Collections.Generic;
using ZDPress.Opc;
using System.ComponentModel;

namespace ZDPress.Dal.Entities
{
    /// <summary>
    /// То, что приходит с опц сервера в виде PressOperationData.
    /// </summary>
    ///
    public class PressOperationData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Int64 Id { get; set; }

        /// <summary>
        /// давление, по которому строится график
        /// </summary>
        public decimal DispPress { get; set; }

        /// <summary>
        /// Parent Id
        /// </summary>
        public Int64 PressOperationId { get; set; }

        /// <summary>
        /// расстояние, по которому строится график
        /// </summary>
        public int DlinaSopr { get; set; }

        /// <summary>
        /// Начало прессования
        /// </summary>
        public bool ShowGraph { get; set; }


        public DateTime DateInsert { get; set; }

        /// <summary>
        /// exception
        /// </summary>
        public Exception Exception { get; set; }


        public static PressOperationData ConvertToPressDataItem()
        {
            PressOperationData item = new PressOperationData();
            //parameters.ForEach(p => InitInternal(p, item));

            item.DispPress = Tags.DispPress.Value;

            item.DlinaSopr = Tags.DlinaSopr.Value;

            item.ShowGraph = Tags.Control_Bits.Bit_Ctrl.GetBit(0);
            return item;
        }
    }
}
