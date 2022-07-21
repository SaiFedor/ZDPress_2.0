using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ZDPress.Dal.Entities;
using ZDPress.Opc;

namespace ZDPress.UI.ViewModels
{
    public class ParametersFormViewModel : INotifyPropertyChanged
    {

        public void SetPropertiesByParameters(List<OpcParameter> parameters)
        {
           
            WheelPosition = parameters.Any(p => p.ParameterName == OpcConsts.SetMaxPress) ? Convert.ToInt32(parameters.First(p => p.ParameterName == OpcConsts.SetMaxPress).ParameterValue) : 0;


            if (parameters.Any(p => p.ParameterName == OpcConsts.SpeedPress))
            {
               var speedPressValue = parameters.Any(p => p.ParameterName == OpcConsts.SpeedPress) ? parameters.First(p => p.ParameterName == OpcConsts.SpeedPress).ParameterValue : 0;
               System.Diagnostics.Trace.WriteLine(string.Format("speedPressValue:{0}", speedPressValue));
               System.Diagnostics.Trace.WriteLine(string.Format("Convert.ToDecimal(speedPressValue) / 10:{0}", Convert.ToDecimal(speedPressValue) / 10));
               SpeedPress = Convert.ToDecimal(speedPressValue) / 10; 
            }
             
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int EmphasisPlunger { get; set; }

        public int EmphasisTravers { get; set; }

        public int Instrument { get; set; }

        public int WheelPosition { get; set; }

        public decimal SpeedPress { get; set; }



        /// <summary>
        /// Параметры словарем
        /// (SpeedPress * 10)
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAsParametesDictionary() 
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add(OpcConsts.SpeedPress, ((int)(SpeedPress * 10)).ToString());
            parameters.Add(OpcConsts.SetMaxPress, WheelPosition.ToString());
            
            return parameters;
        }
    }
}
