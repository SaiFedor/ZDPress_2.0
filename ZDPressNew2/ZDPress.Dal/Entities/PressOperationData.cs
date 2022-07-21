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

        public int Id { get; set; }

        /// <summary>
        /// давление, по которому строится график
        /// </summary>
        public decimal DispPress { get; set; }

        /// <summary>
        /// Parent Id
        /// </summary>
        public int PressOperationId { get; set; }

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


        public static PressOperationData ConvertToPressDataItem(List<OpcParameter> parameters)
        {
            PressOperationData item = new PressOperationData();            
            parameters.ForEach(p => InitInternal(p, item));
            return item;
        }

        private static void InitInternal(OpcParameter parameter, PressOperationData item)
        {
            if (parameter == null)
            {
                return;
            }
            dynamic val = Convert.ChangeType(parameter.ParameterValue, parameter.ParameterType);

            if (parameter.ParameterName == OpcConsts.DispPress)
            {
                item.DispPress = val;
            }
            if (parameter.ParameterName == OpcConsts.DlinaSopr)
            {
                item.DlinaSopr = val;
            }
            
            if (parameter.ParameterName == OpcConsts.ShowGraph)
            {
                item.ShowGraph = val == 1;
            }
        }
    }
}
