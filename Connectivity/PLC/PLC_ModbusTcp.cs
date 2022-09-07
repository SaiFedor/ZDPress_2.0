//using Connectivity.Crc;
using System;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using NModbus;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.InteropServices;


namespace Connectivity {
    /// <summary>
    /// Обеспечивает связь c ПЛК Modbus Tcp
    /// </summary>
    public class PLC_ModbusTcp : PLC
    {
        private TcpClient? client;
        private IModbusMaster? master;
        private readonly object locker = new Object();
        private readonly int numberReadRegisters; // общее количество считываемых регистров
        private readonly byte[] data;
        private readonly ushort startAddress; // READ_M - WRITE_M PLC memory range (%M field). Offset is byte offset.
        private readonly ushort maxSizeRegReadCmd; // количество регистров считываемых за раз (Max: Schneider, Beckhoff - 125; Omron - 123)
        private readonly bool checkCRC;

        public override event Action? ReadComletedEvent;
        public override event Action<bool>? ReadStatusChangedEvent;
        public override event Action<string>? PlcStateChangedEvent;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ip">Ip: 192.168.0.1</param>
        /// <param name="port">порт 502</param>
        /// <param name="sizeDataRead">размер данных в байтах, считываемых из ПЛК методом Read и ReadAsync (должно быть кратно maxSizeRegReadCmd)</param>
        /// <param name="startAddress">Смещение для области памяти %М</param>
        /// <param name="maxSizeRegReadCmd">Количество регистров считываемых за раз (Max: Schneider, Beckhoff - 125; Omron - 123)</param>
        public PLC_ModbusTcp(string ip, int port = 502, int sizeDataRead = 24000, ushort startAddress = 0x3000, ushort maxSizeRegReadCmd = 125, bool checkCRC = false) : base(ip, port, sizeDataRead)
        {
            IP = ip;
            Port = port;
            SizeDataRead = sizeDataRead;
            numberReadRegisters = sizeDataRead / 2;
            this.startAddress = startAddress;
            this.maxSizeRegReadCmd = maxSizeRegReadCmd;
            this.checkCRC = checkCRC;
            data = new byte[sizeDataRead];
        }

        public bool Connect()
        {
            try
            {
                client = new TcpClient (IP, Port);
                //client.SendTimeout = 1000;
                //client.ReceiveTimeout = 1000;
                var factory = new ModbusFactory();
                master = factory.CreateMaster(client);
                return client?.Connected ?? false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in {nameof(PLC_ModbusTcp)}.{nameof(Connect)}: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Запускает процесс чтения данных из ПЛК
        /// </summary>
        /// <returns>CancellationTokenSource - для останова чтения</returns>
        public override CancellationTokenSource StartRead()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            SynchronizationContext? uiContext = SynchronizationContext.Current;
            Thread pollThread = new Thread(() => PollThread(uiContext, cancelTokenSource.Token));
            pollThread.Start();
            return cancelTokenSource;
        }

        private void PollThread(object? uiContext, CancellationToken token)
        {
            SynchronizationContext context = uiContext as SynchronizationContext ??
                throw new InvalidOperationException($"{nameof(PLC_ModbusTcp)}.{nameof(StartRead)}: SynchronizationContext.Current не может быть Null");
            bool con = false;
            bool appStart = true;
            void readStatus(bool conNew)
            {
                if (conNew != con || appStart)
                {
                    con = conNew;
                    context.Post(ReadStatusChanged, con);
                }
                appStart = false;
            }
            while (!token.IsCancellationRequested)
            {
                if (client?.Connected ?? false && master != null)
                {
                    int k = 0;
                    for (int i = 0; i < numberReadRegisters; i += maxSizeRegReadCmd)
                    {
                        if (token.IsCancellationRequested)
                            break;

                        ushort[]? inputs = null;
                        lock (locker)
                        {
                            try
                            {
                                inputs = master?.ReadHoldingRegisters(0, (ushort)(startAddress + i), maxSizeRegReadCmd);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }
                        }
                        if (inputs?.Length == maxSizeRegReadCmd)
                        {
                            for (int j = 0; j < maxSizeRegReadCmd; j++)
                            {
                                var ar = BitConverter.GetBytes(inputs[j]);
                                data[k++] = ar[0];
                                data[k++] = ar[1];
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (k == SizeDataRead)
                    {
                        var span = memory[..].Span;
                        data.AsSpan().CopyTo(span);

                        #region Вычисление CRC
                        bool correctCRC = true;
                        if (checkCRC)
                        {
                            ushort[] crcReceived = new ushort[10];
                            ushort[] crcCalculated = new ushort[10];

                            for (int i = 0; i < 9; i++)
                            {
                                crcReceived[i] = BitConverter.ToUInt16(memory.Slice(1000 + (i * 2), 2).Span);
                            }
                            int t = 0;
                            for (int i = 0; i < 1800; i += 200)
                            {
                                var bytes = memory.Slice(i, 200).Span.ToArray();
                                // Меняю местми байты, т.к. BitConverter.GetBytes(inputs[j]) переворачивает их
                                for (int v = 0; v < bytes.Length; v += 2)
                                {
                                    (bytes[v + 1], bytes[v]) = (bytes[v], bytes[v + 1]);
                                }
                                // Классы из Connectivity.Crc
                                //var crc = CrcAlgorithm.CreateCrc16Modbus();
                                //crc.Append(bytes);
                                //var ds = crc.ToUInt64();
                                //crcCalculated[t++] = (ushort)ds;
                                crcCalculated[t++] = Crc16.ComputeChecksum(bytes);
                            }

                            correctCRC = true;
                            if (correctCRC)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    correctCRC &= crcReceived[i] == crcCalculated[i];
                                }
                            }
                            else
                            {
                                Debug.WriteLine("Error CRC");
                            }
                        }
                        #endregion

                        if (correctCRC)
                        {
                            context.Post(ReadComleted, null);
                        }
                        readStatus(true);
                    }
                }
                else
                {
                    readStatus(Connect());
                }
                Thread.Sleep(DelayReadAsync);
                //try
                //{
                //    Task.Delay(DelayReadAsync, token).Wait(token);
                //}
                //catch { }
            }
        }

        private void ReadComleted(object? state)
        {
            ReadComletedEvent?.Invoke();
        }

        private void ReadStatusChanged(object? state)
        {
            IsConnected = (state is bool st) && st;
            ReadStatusChangedEvent?.Invoke(IsConnected);
        }

        #region Чтение данных из потока
        //public override bool GetBOOL(int adr) => BitConverter.ToBoolean(memory.Slice(adr, 1).Span);

        //public override byte GetBYTE(int adr) => memory.Slice(adr, 1).Span.ToArray()[0];

        //public override short GetINT(int adr) => BitConverter.ToInt16(memory.Slice(adr, 2).Span);

        //public override UInt16 GetWORD(int adr) => BitConverter.ToUInt16(memory.Slice(adr, 2).Span);

        //public override UInt32 GetDWORD(int adr) => BitConverter.ToUInt32(memory.Slice(adr, 4).Span);

        //public override float GetREAL(int adr) => BitConverter.ToSingle(memory.Slice(adr, 4).Span);

        //public override DateTime GetDT(int adr) => new DateTime(1970, 1, 1).AddSeconds(BitConverter.ToUInt32(memory.Slice(adr, 4).Span));

        //public override TimeSpan GetTIME(int adr) => TimeSpan.FromMilliseconds(BitConverter.ToUInt32(memory.Slice(adr, 4).Span));

        ////public override string GetString(int adr, int len) => new System.Text.ASCIIEncoding().GetString(memory.Slice(adr, len).Span); // так только латиница
        //public override string GetString(int adr, int len)
        //{
        //    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        //    var enc1251 = Encoding.GetEncoding("windows-1251");
        //    return enc1251.GetString(memory.Slice(adr, len).Span);
        //}
        #endregion

        public override (ushort, bool) GetRegister(int adr)
        {
            ushort[]? bytes = null;
            bool readOk = false;
            lock (locker)
            {
                try
                {
                    bytes = master?.ReadHoldingRegisters(0, (ushort)(startAddress + adr / 2), 1);
                    readOk = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"GetRegister Ошибка: {ex.Message}");
                }
            }
            return (bytes?.Length > 0 ? bytes[0] : default, readOk);
        }

        #region Запись данных в ПЛК
        /// <summary>
        /// Записывает значение типа bool, short, ushort, uint float или DateTime приведенное к object в ПЛК по заданному адресу,
        /// возвращает true в случае удачной записи.
        /// </summary>
        /// <param name="adr">адрес для записи</param>
        /// <param name="value">записываемое значение</param>
        /// <returns>true в случае удачной записи</returns>
        public override bool Write(int adr, object value)
        {
            bool result = false;
            switch (value)
            {
                case bool boolVal:
                case byte byteVal:
                    WriteBoolByte(adr, value);
                    break;
                case short shortVal:
                    ushort uVal = unchecked((ushort)shortVal);
                    result = WriteSingleRegister(adr, uVal);
                    break;
                case ushort ushortVal:
                    result = WriteSingleRegister(adr, ushortVal);
                    break;
                case uint:
                case float:
                case DateTime:
                case TimeSpan:
                    result = WriteUintFloatDt(value);
                    break;
                default:
                    throw new ArgumentException($"{this.GetType()}.{nameof(Write)} запись значения типа {value.GetType()} не реализована");
            }
            return result;

            bool WriteBoolByte(int adr, object value)
            {
                bool writeOk = false;
                int nByte = 0;
                if (adr % 2 != 0)
                {
                    adr--;
                    nByte = 1;
                }
                (ushort reg, bool ok) = GetRegister(adr);
                if (ok)
                {
                    var bytes = BitConverter.GetBytes(reg);
                    bytes[nByte] = value switch
                    {
                        bool boolVal => (byte)(boolVal ? 1 : 0),
                        byte byteVal => byteVal,
                        _ => throw new ArgumentException($"{nameof(WriteBoolByte)}:{nameof(value)}"),
                    };
                    var writeRegister = BitConverter.ToUInt16(bytes);
                    writeOk = WriteSingleRegister(adr, writeRegister);
                }
                return writeOk;
            }

            bool WriteUintFloatDt(object value)
            {
                var arr = value switch
                {
                    uint uintVal => BitConverter.GetBytes(uintVal),
                    float floatVal => BitConverter.GetBytes(floatVal),
                    DateTime dtVal => BitConverter.GetBytes((UInt32)(dtVal).Subtract(new DateTime(1970, 1, 1)).TotalSeconds),
                    TimeSpan => BitConverter.GetBytes((UInt32)((TimeSpan)value).TotalMilliseconds),
                    _ => throw new ArgumentException($"{nameof(WriteUintFloatDt)}:{nameof(value)}"),
                };
                ushort[] vals = new ushort[2];
                vals[0] = BitConverter.ToUInt16(arr, 0);
                vals[1] = BitConverter.ToUInt16(arr, 2);
                return WriteSingleRegister(adr, 0, vals);
            }
        }
        #endregion
        /*
        public bool WriteBit(WORD tag, int numBit, bool value)
        {
            if (master == null) return false;
            uint uVal = tag.Value;
            uVal >>= numBit;
            if (value)
            {
                uVal |= 1;
            }
            else
            {
                uVal &= 1;
            }
            uVal <<= numBit;
            return tag.Write((ushort)uVal);
        }
        */
        private bool WriteSingleRegister(int mbAddress, ushort value, ushort[]? values = null)
        {
            if (master == null) return false;
            bool writeOk = false;
            // Использую по инерции байтову адресацию, надо перевести в регистровую
            ushort registerAddress = (ushort)(startAddress + mbAddress / 2);
            lock (locker)
            {
                try
                {
                    if (values != null)
                    {
                        master?.WriteMultipleRegisters(0, registerAddress, values);
                    }
                    else
                    {
                        master?.WriteSingleRegister(0, registerAddress, value);
                    }
                    writeOk = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return writeOk;
        }

        #region Dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Instantiate a SafeHandle instance.
        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                client?.Close();
                master?.Dispose();
            }
            disposed = true;
        }
        #endregion
    }

    internal static class Crc16
    {
        const ushort polynomial = 0xA001;
        //const ushort polynomial = 0x8005;
        static readonly ushort[] table = new ushort[256];

        public static ushort ComputeChecksum(byte[] bytes)
        {
            ushort crc = 0xFFFF;
            for (int i = 0; i < bytes.Length; ++i)
            {
                byte index = (byte)(crc ^ bytes[i]);
                crc = (ushort)((crc >> 8) ^ table[index]);
            }
            return crc;
        }

        static Crc16()
        {
            ushort value;
            ushort temp;
            for (ushort i = 0; i < table.Length; ++i)
            {
                value = 0;
                temp = i;
                for (byte j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (ushort)((value >> 1) ^ polynomial);
                    }
                    else
                    {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                table[i] = value;
            }
        }
    }

}