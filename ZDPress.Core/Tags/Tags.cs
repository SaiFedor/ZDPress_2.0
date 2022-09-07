using ConnectionPLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZDPress.Opc
{
    public class Tags
    {
        private static PLC_Base Plc => MainConstant.plc;

        public static ST_ControlBits Control_Bits { get; } = new ST_ControlBits(44, Plc);

        /// <summary>
        /// давление, по которому строится график
        /// </summary>
        public static WORD DispPress { get; } = new WORD(52, Plc);
        /// <summary>
        /// расстояние, по которому строится график
        /// </summary>
        public static WORD DlinaSopr { get; } = new WORD(0, Plc);
        public static WORD DispPress1 { get; } = new WORD(8, Plc);
        public static WORD DispPress2 { get; } = new WORD(12, Plc);
        public static WORD DispPress3 { get; } = new WORD(16, Plc);

        /// <summary>
        /// Максимальное давление прессования
        /// </summary>
        public static INT SetMaxPress { get; } = new INT(36, Plc);      //int
        /// <summary>
        /// Скорость прессования
        /// </summary>
        public static INT SpeedPress { get; set; } = new INT(32, Plc);           // int

        /// <summary>
        /// Авария датчиа давления прессования
        /// </summary>
        public static WORD AlarmBP1 { get; } = new WORD(60, Plc);       //int

        /// <summary>
        /// Авария датчиа давления прессования
        /// </summary>
        public static WORD AlarmBP2 { get; } = new WORD(62, Plc);        //int

        /// <summary>
        /// Авария датчиа давления прессования
        /// </summary>
        public static WORD AlarmBP3 { get; } = new WORD(64, Plc);        //int
    }
}
