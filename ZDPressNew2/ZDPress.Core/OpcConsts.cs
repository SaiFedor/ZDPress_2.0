namespace ZDPress.Opc
{
    public static class OpcConsts
    {
        /// <summary>
        /// давление, по которому строится график
        /// </summary>
        public static string ShowGraph = "PLC.PLC.ShowGraph";
        /// <summary>
        /// давление, по которому строится график
        /// </summary>
        public static string DispPress = "PLC.PLC.DispPress";
        /// <summary>
        /// давление, по которому строится график    датчик
        /// </summary>
        public static string DispPress1 = "PLC.PLC.DispPress1";
        /// <summary>
        /// давление, по которому строится график    датчик
        /// </summary>
        public static string DispPress2 = "PLC.PLC.DispPress2";
        /// <summary>
        /// давление, по которому строится график          датчик
        /// </summary>
        public static string DispPress3 = "PLC.PLC.DispPress3";

        /// <summary>
        /// расстояние, по которому строится график
        /// </summary>
        public static string DlinaSopr = "PLC.PLC.DlinaSopr";
        /// <summary>
        /// Битовые переменные
        /// </summary>
        public static string Bits = "PLC.PLC.Bits";
        /// <summary>
        /// Посадка колеса
        /// </summary>
        public static string SetMaxPress = "PLC.PLC.SetMaxPress";      //int
        /// <summary>
        /// Скорость прессования
        /// </summary>
        public static string SpeedPress = "PLC.PLC.SpeedPress";           // int

        /// <summary>
        /// Авария датчиа давления прессования
        /// </summary>
        public static string AlarmBP1 = "PLC.PLC.AlarmBP1";        //int

        /// <summary>
        /// Авария датчиа давления прессования
        /// </summary>
        public static string AlarmBP2 = "PLC.PLC.AlarmBP2";        //int

        /// <summary>
        /// Авария датчиа давления прессования
        /// </summary>
        public static string AlarmBP3 = "PLC.PLC.AlarmBP3";        //int

        /// <summary>
        /// Текст аварии
        /// </summary>
        public static string AlarmText = "Авария датчика";
    }
}
