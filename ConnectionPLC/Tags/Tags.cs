using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ConnectionPLC
{

    public static class Info
    {
        // Список всех реализованных стандартных типов данных стандарта IEC61131-3
        /*
        BYTE	0	255	8 bit
        WORD	0	65,535	16 bit
        DWORD	0	4,294,967,295	32 bit
        LWORD	0	264-1	64 bit
        SINT	–128	127	8 bit
        USINT	0	255	8 bit
        INT	    –32,768	32,767	16 bit
        UINT	0	65,535	16 bit
        DINT	–2,147,483,648	2,147,483,647	32 bit
        UDINT	0	4,294,967,295	32 bit
        LINT	–263	263-1	64 bit
        ULINT	0	264-1	64 bit

        REAL	-3.402823e+38	3.402823e+38	32 bit
        LREAL	-1.7976931348623158e+308	1.7976931348623158e+308	64 bit

        DATE_AND_TIME (DT)	0 (1970-01-01, 00:00:00)	4294967295 (2106-02-07,06:28:15)	32 bit
        https://help.codesys.com/webapp/_cds_struct_reference_datatypes;product=codesys;version=3.5.13.0#standard-data-types
        https://product-help.schneider-electric.com/Machine%20Expert/V1.1/en/SoMProg/SoMProg/Data_Types/Data_Types-3.htm

         */
        public static List<Type> TagTypesList { get; set; } = new List<Type>()
    {
        typeof(BOOL),

        typeof(BYTE),
        typeof(WORD),
        typeof(DWORD),
        typeof(LWORD),

        typeof(SINT),
        typeof(USINT),
        typeof(INT),
        typeof(UINT),
        typeof(DINT),
        typeof(UDINT),
        typeof(LINT),
        typeof(ULINT),

        typeof(REAL),
        typeof(LREAL),

        typeof(DATETIME),

        typeof(STRING),
    };
    }

    public sealed class BOOL : Tag<bool>
    {
        public BOOL(int adr, PLC_Base plc) : base(adr, plc) { }
        public override int Size => 1;
        //protected override void PlcReadComleted() => Value = plc.GetBOOL(Adr);
    }

    public sealed class BYTE : Tag<byte>
    {
        public BYTE(int adr, PLC_Base plc) : base(adr, plc) { }
        public override int Size => 1;
        //protected override void PlcReadComleted() => Value = plc.GetBYTE(Adr);
    }

    /// <summary>
    /// Тег типа WORD, с дополнительной функциональностью:
    /// - получение значения бита по его номеру
    /// - запись одного бита
    /// - дополнительные свойства Bit0, Biy1,...,Bit15 для привязки к битам
    /// </summary>
    public sealed class WORD : Tag<ushort>
    {
        readonly IEnumerable<PropertyInfo> bitProp;

        public WORD(int adr, PLC_Base plc) : base(adr, plc)
        {
            this.plc.ReadComletedEvent += () => CheckBits();
            #region Построение списка свойст bitProp из Bit0, Bit1,...,Bit15
            // если буду использовать первый вариант, простой, то удалить
            Regex regex = new Regex(@"\d{1,2}");
            string n = nameof(Bit0).Replace("0", "");
            bitProp = this.GetType().GetProperties().Where(p => p.Name.StartsWith(n)).OrderBy(p => int.Parse(regex.Match(p.Name).Value));
            #endregion
        }
        public override int Size => 2;
        //protected override void PlcReadComleted() => Value = plc.GetWORD(Adr);

        public bool Bit0 { get; set; }
        public bool Bit1 { get; set; }
        public bool Bit2 { get; set; }
        public bool Bit3 { get; set; }
        public bool Bit4 { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit7 { get; set; }
        public bool Bit8 { get; set; }
        public bool Bit9 { get; set; }
        public bool Bit10 { get; set; }
        public bool Bit11 { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }

        private void CheckBits()
        {
            bool b;
            #region Второй вариант
            int i = 0;
            foreach (var bProp in bitProp)
            {
                b = GetBit(i++);
                if (b != (bool)bProp.GetValue(this))
                {
                    bProp.SetValue(this, b);
                    OnPropertyChanged(bProp.Name);
                }
            }
            #endregion
            #region Первый вариант
            // Думаю этот способ предпочтительней, но верхний красивей
            //b = GetBit(0); if (b != Bit0) { Bit0 = b; OnPropertyChanged($"Bit0"); }
            //b = GetBit(1); if (b != Bit1) { Bit1 = b; OnPropertyChanged($"Bit1"); }
            //b = GetBit(2); if (b != Bit2) { Bit2 = b; OnPropertyChanged($"Bit2"); }
            //b = GetBit(3); if (b != Bit3) { Bit3 = b; OnPropertyChanged($"Bit3"); }
            //b = GetBit(4); if (b != Bit4) { Bit4 = b; OnPropertyChanged($"Bit4"); }
            //b = GetBit(5); if (b != Bit5) { Bit5 = b; OnPropertyChanged($"Bit5"); }
            //b = GetBit(6); if (b != Bit6) { Bit6 = b; OnPropertyChanged($"Bit6"); }
            //b = GetBit(7); if (b != Bit7) { Bit7 = b; OnPropertyChanged($"Bit7"); }
            //b = GetBit(8); if (b != Bit8) { Bit8 = b; OnPropertyChanged($"Bit8"); }
            //b = GetBit(9); if (b != Bit9) { Bit9 = b; OnPropertyChanged($"Bit9"); }
            //b = GetBit(10); if (b != Bit10) { Bit10 = b; OnPropertyChanged($"Bit10"); }
            //b = GetBit(11); if (b != Bit11) { Bit11 = b; OnPropertyChanged($"Bit11"); }
            //b = GetBit(12); if (b != Bit12) { Bit12 = b; OnPropertyChanged($"Bit12"); }
            //b = GetBit(13); if (b != Bit13) { Bit13 = b; OnPropertyChanged($"Bit13"); }
            //b = GetBit(14); if (b != Bit14) { Bit14 = b; OnPropertyChanged($"Bit14"); }
            //b = GetBit(15); if (b != Bit15) { Bit15 = b; OnPropertyChanged($"Bit15"); } 
            #endregion
        }

        /// <summary>
        /// Возвращает значение бита
        /// </summary>
        /// <param name="bit">Номер бита (0-15)</param>
        /// <param name="word">переменная типа ushort</param>
        /// <returns>true or false</returns>
        /// <exception cref="ArgumentOutOfRangeException">номер бита должен быть в диапазоне 0-31</exception>
        public static bool GetBit(int bit, ushort word)
        {
            if (bit < 0 || bit > 15) throw new ArgumentOutOfRangeException(nameof(bit));
            return ((word >> bit) & 1) == 1;
            // Первый вариант
            //uint m = word;
            //m <<= 31 - bit;
            //m >>= 31;
            //return m == 1;
        }

        /// <summary>
        /// Возвращает значение бита
        /// </summary>
        /// <param name="bit">Номер бита (0-15)</param>
        /// <returns>true or false</returns>
        public bool GetBit(int bit) => GetBit(bit, Value);

        /// <summary>
        /// Записывает противоположное значение бита
        /// </summary>
        /// <param name="numBit">Номер бита (0-15)</param>
        /// <returns></returns>
        public bool Write(int numBit) => WriteBit(numBit, null);

        /// <summary>
        /// Записывает в бит заданное значение
        /// </summary>
        /// <param name="numBit">Номер бита (0-15)</param>
        /// <param name="value">Записываемое значение бита</param>
        /// <returns></returns>
        public bool Write(int numBit, bool value) => WriteBit(numBit, value);

        private bool WriteBit(int numBit, bool? value)
        {
            (ushort currValue, bool readOk) = plc.GetRegister(Adr);
            if (readOk)
            {
                bool v = value ?? !GetBit(numBit, currValue);
                uint m = 1;
                m <<= numBit;
                uint uVal;
                if (!v)
                {
                    m = ~m;
                    uVal = currValue & m;
                }
                else
                {
                    uVal = currValue | m;
                }
                return base.Write((ushort)uVal);
            }
            return false;
        }
    }

    public sealed class DWORD : Tag<uint>
    {
        public DWORD(int adr, PLC_Base plc) : base(adr, plc) { }
        public override int Size => 4;
        //protected override void PlcReadComleted() => Value = plc.GetDWORD(Adr);

        /// <summary>
        /// Возвращает значение бита
        /// </summary>
        /// <param name="bit">Номер бита (0-31)</param>
        /// <param name="dword">переменная типа uint</param>
        /// <returns>true or false</returns>
        /// <exception cref="ArgumentOutOfRangeException">номер бита должен быть в диапазоне 0-31</exception>
        public static bool GetBit(int bit, uint dword)
        {
            if (bit < 0 || bit > 31) throw new ArgumentOutOfRangeException(nameof(bit));
            return ((dword >> bit) & 1) == 1;
        }

        /// <summary>
        /// Возвращает значение бита
        /// </summary>
        /// <param name="bit">Номер бита (0-31)</param>
        /// <returns>true or false</returns>
        public bool GetBit(int bit) => GetBit(bit, Value);
    }

    public sealed class LWORD : Tag<ulong>
    {
        public LWORD(int adr, PLC_Base plc) : base(adr, plc) { }
        public override int Size => 8;
        //protected override void PlcReadComleted() => Value = plc.GetULINT(Adr);
    }

    public sealed class SINT : Tag<sbyte>
    {
        public SINT(int adr, PLC_Base plc) : base(adr, plc) { }
        public override int Size => 1;
        //protected override void PlcReadComleted() => Value = plc.GetINT(Adr);
    }

    public sealed class USINT : Tag<byte>
    {
        public USINT(int adr, PLC_Base plc) : base(adr, plc) { }
        public override int Size => 1;
        //protected override void PlcReadComleted() => Value = plc.GetINT(Adr);
    }

    public sealed class INT : Tag<short>
    {
        public INT(int adr, PLC_Base plc) : base(adr, plc) { }
        public override int Size => 2;
        //protected override void PlcReadComleted() => Value = plc.GetINT(Adr);
    }

    public sealed class UINT : Tag<ushort>
    {
        public UINT(int adr, PLC_Base plc) : base(adr, plc) { }
        public override int Size => 2;
        //protected override void PlcReadComleted() => Value = plc.GetWORD(Adr);
    }

    public sealed class DINT : Tag<int>
    {
        public DINT(int adr, PLC_Base plc) : base(adr, plc) { }
        public override int Size => 4;
        //protected override void PlcReadComleted() => Value = plc.GetWORD(Adr);
    }

    public sealed class UDINT : Tag<uint>
    {
        public UDINT(int adr, PLC_Base plc) : base(adr, plc) { }
        public override int Size => 4;
        //protected override void PlcReadComleted() => Value = plc.GetWORD(Adr);
    }

    public sealed class LINT : Tag<long>
    {
        public LINT(int adr, PLC_Base plc) : base(adr, plc) { }
        public override int Size => 8;
        //protected override void PlcReadComleted() => Value = plc.GetULINT(Adr);
    }

    public sealed class ULINT : Tag<ulong>
    {
        public ULINT(int adr, PLC_Base plc) : base(adr, plc) { }
        public override int Size => 8;
        //protected override void PlcReadComleted() => Value = plc.GetULINT(Adr);
    }

    public sealed class REAL : Tag<float>
    {
        public REAL(int adr, PLC_Base plc) : base(adr, plc) { }
        public override int Size => 4;
        //protected override void PlcReadComleted() => Value = plc.GetREAL(Adr);
    }

    public sealed class LREAL : Tag<double>
    {
        public LREAL(int adr, PLC_Base plc) : base(adr, plc) { }
        public override int Size => 8;
        //protected override void PlcReadComleted() => Value = plc.GetREAL(Adr);
    }

    public sealed class DATETIME : Tag<DateTime>
    {
        public DATETIME(int adr, PLC_Base plc) : base(adr, plc) { }
        public override int Size => 4;
        //protected override void PlcReadComleted() => Value = plc.GetDT(Adr);
    }

    public sealed class STRING : Tag, IGetNextAdr
    {
        private readonly PLC_Base Plc;
        private string value = "";
        public int Size { get; set; }
        public bool InitTag { get; private set; } // для вызова OnPropertyChanged при запуске приложения, если Value равно значению по умолчанию

        //public override event Action<object>? ValueChanged;
        public event EventHandler<string> ValueChanged;

        public string Value
        {
            get { return value; }
            set
            {
                if (!this.value.Equals(value))
                {
                    this.value = value;
                    OnPropertyChanged();
                    ValueChanged?.Invoke(this, Value);
                }
                else if (!InitTag)
                {
                    OnPropertyChanged();
                    ValueChanged?.Invoke(this, Value);
                    InitTag = true;
                }
            }
        }

        /// <summary>
        /// Возвращает следующий адрес
        /// return: адрес + размер тега
        /// </summary>
        public int NextAdr => Adr + Size;

        public STRING(int adr, PLC_Base plc, int len)
        {
            Adr = adr;
            Size = len;
            Plc = plc;
            plc.ReadComletedEvent += AdsReader_ReadEvent;  // присоединяет обработчик к событию чтения класса ADS_Reader
        }

        private void AdsReader_ReadEvent()
        {
            Value = Plc.GetString(Adr, Size);
        }

        public override object GetValue() => Value as object;

        public static implicit operator string(STRING t) => t.value;
    }
}