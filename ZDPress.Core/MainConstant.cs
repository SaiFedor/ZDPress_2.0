using ConnectionPLC;
using System.Configuration;
using System.Threading;



namespace ZDPress.Opc

{

    public class MainConstant
    {
        //public static readonly PLC plc = new PLC_ModbusTcp("192.168.0.10", 502, 20000, 0) { DelayReadAsync = 20 }; // Schneider
        //public static readonly PLC plc = new PLC_ModbusTcp("192.168.0.30", 502, 1968, 0, 123) { DelayReadAsync = 20 }; // Omron 1968 / 123 должно делится без остатка
        // Omron 1800 / 100 должно делится без остатка
        static Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      
        private static string ipPlc = configuration.AppSettings.Settings["IPPLC"].Value;
        // В этом проекте обязательно maxSizeRegReadCmd=100, т.к. все структуры в ПЛК размещены в адресах кратых 100 (что бы читать структуру целиком одним запросом)
        public static readonly PLC_ModbusTCP plc = new PLC_ModbusTCP(ipPlc, 502, 200, 300, 100, false) { DelayReadAsync = 20 };
        //public static readonly PLC plc = new PLC_ModbusTcp("192.168.0.20", 502, 12000, 0x3000, 125) { DelayReadAsync = 10 }; // Beckhoff
        public static CancellationTokenSource CancellationTokenSourcePLCread { get;  set; }

        public static bool DataBaseLocalIsConnected { get; set; }
        public static bool DataBaseRemoteIsConnected { get; set; }

       // public static Window MainWindow { get; } = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault() ?? throw new Exception();

        public const int MATRIX_LEN = 31;
        #region Паттерны регулярных выражений для кодов
        public const string DM_PATTERN_CAM = @"\u000201.{14}21.{6}\u001d93.{4}\r\n"; // только для молочки (без третьей группы) см. "Info/Из чего состоит код маркировки молочной продукции.docx"
        public const string DM_PATTERN = @"01.{14}21.{6}\u001d93.{4}"; // без символа начала строки (u0002), новая строка, перевод каретки
        /* 
        0104601751015655215+XMCT93tr70 масло вкуснотеево
        0104610003761806215,Wk9g93LA8K молоко ополье
        0104601761000825215fYdFS939D2E творог суздальский
        0104620006003704215_sB%t93VSRt залеский фермер
         это разделитель групп u001d
        вот пример полной строки: "\u00020104620006003704215psI\"U\u001d93Ra+p\r\n"

        ASCII коды 
        2 - STX - Start of Text - Начало текста \A \x02
        29 - FS - Разделитель групп
        13 - CR - Carriage Return - Возврат каретки \r
        10 - LF - Line Feed - Перевод строки \n

        // Так проходят ошибочные коды, бывают в 1, 2, 3 элементах попадают непечатные символы при плохом сканировании
        // if (buffer.Length >= 34 && buffer[0] == 2 && buffer[25] == 29 && buffer[32] == 13 && buffer[33] == 10)
     */
        #endregion

        public static void StartRead()
        {
            CancellationTokenSourcePLCread = plc.StartRead();
        }
    }
}
