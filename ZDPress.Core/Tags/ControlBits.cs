using ConnectionPLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDPress.Opc
{
    public class ST_ControlBits : ClassTag
    {
        public ST_ControlBits(int adr, PLC_Base plc, string name = "", string unit = "") : base(adr, plc, name, unit) { }

        public override Dictionary<string, Tag> GetTagsFromRestore() => null;
        public WORD Bit_Ctrl; // Управляющие битовые переменные
        public class ParamBitCtrl
        {
            public static int ShowGraph => 1; // Запуск партии
            public static int PauseBatch => 2; // Пауза
            public static int StopBatch => 3; // Завершить партию
            public static int Conveyor => 4; // Конвейер (1-вкл/0-выкл)
            public static int Rejector => 5; // Отбраковщик (пуск по фронту)
            public static int Belt => 6; // Ремни (1-вкл/0-выкл)
            public static int Separator => 7; // Разделитель
            public static int Alarm => 7; // Сигнал аварии (от скады, например если пропала связь с БД)
            public static int SirenOff => 7; //  Выключение сирены
        }

    }
}
