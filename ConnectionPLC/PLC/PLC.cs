using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ConnectionPLC.PLC;

public abstract class PLC : INotifyPropertyChanged, IDisposable
{
    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    protected readonly Memory<byte> memory;

    /// <summary>
    /// IP адрес (AmsNetID - для ADS соединения)
    /// </summary>
    public string IP { get; init; }

    /// <summary>
    /// Порт
    /// </summary>
    public int Port { get; init; }

    /// <summary>
    /// Размер данных в байтах, считываемых из ПЛК
    /// </summary>
    public int SizeDataRead { get; init; }

    /// <summary>
    /// Пауза между считываниями данных в мсек
    /// </summary>
    public int DelayReadAsync { get; set; } = 100;

    /// <summary>
    /// Соединение с ПЛК
    /// </summary>
    public bool IsConnected { get; protected set; }

    /// <summary>
    /// Происходит при обрыве и восстановлении связи
    /// в первом случае возвращет false, во втором true
    /// </summary>
    public abstract event Action<bool>? ReadStatusChangedEvent;

    /// <summary>
    /// происходит при смене статуса ПЛК (Run, Config,...)
    /// </summary>
    public abstract event Action<string>? PlcStateChangedEvent;

    string adsState = "";
    public string AdsState
    {
        get { return adsState; }
        set
        {
            adsState = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Происходит после каждого успешного считывания данных из ПЛК
    /// </summary>
    public abstract event Action? ReadComletedEvent;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="ip">IP адрес (AmsNetID - для ADS соединения)</param>
    /// <param name="port">Порт (801 - для ADS соединения (TwinCAT 2, 851 - TwinCAT 3); 502 - для Modbus)</param>
    /// <param name="sizeDataRead">Размер данных в байтах, считываемых из ПЛК методом Read и ReadAsync</param>
    public PLC(string ip, int port, int sizeDataRead)
    {
        IP = ip;
        Port = port;
        SizeDataRead = sizeDataRead;
        memory = new Memory<byte>(new byte[sizeDataRead]);
    }

    /// <summary>
    /// Запускает процесс чтения данных из ПЛК
    /// </summary>
    /// <returns>CancellationTokenSource - для останова чтения</returns>
    public abstract CancellationTokenSource StartRead();

    public abstract void Dispose();

    #region Чтение/запись данных из потока
    public virtual bool GetBOOL(int adr) => BitConverter.ToBoolean(memory.Slice(adr, 1).Span);

    public virtual byte GetBYTE(int adr) => memory.Slice(adr, 1).Span.ToArray()[0];
    public virtual ushort GetWORD(int adr) => BitConverter.ToUInt16(memory.Slice(adr, 2).Span);
    public virtual uint GetDWORD(int adr) => BitConverter.ToUInt32(memory.Slice(adr, 4).Span);
    public virtual ulong GetLWORD(int adr) => BitConverter.ToUInt64(memory.Slice(adr, 8).Span);

    public virtual sbyte GetSBYTE(int adr) => (sbyte)GetBYTE(adr);
    public virtual short GetINT(int adr) => BitConverter.ToInt16(memory.Slice(adr, 2).Span);
    public virtual int GetDINT(int adr) => BitConverter.ToInt32(memory.Slice(adr, 4).Span);
    public virtual long GetLINT(int adr) => BitConverter.ToInt64(memory.Slice(adr, 8).Span);

    public virtual float GetREAL(int adr) => BitConverter.ToSingle(memory.Slice(adr, 4).Span);
    public virtual double GetLREAL(int adr) => 0.0;//throw new NotImplementedException("Метод 'PLC.GetLREAL(int adr)' не определен"); // TODO так не соответсвует - BitConverter.ToDouble(memory.Slice(adr, 8).Span);

    public virtual DateTime GetDT(int adr) => new DateTime(1970, 1, 1).AddSeconds(BitConverter.ToUInt32(memory.Slice(adr, 4).Span));

    public virtual TimeSpan GetTIME(int adr) => TimeSpan.FromMilliseconds(BitConverter.ToUInt32(memory.Slice(adr, 4).Span));

    public virtual string GetString(int adr, int len) => new System.Text.ASCIIEncoding().GetString(memory.Slice(adr, len).Span); // так только латиница
    // а так и кириллица
    //public virtual string GetString(int adr, int len)
    //{
    //    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    //    var enc1251 = Encoding.GetEncoding("windows-1251");
    //    return enc1251.GetString(memory.Slice(adr, len).Span);
    //}

    // Возвращает пару (регист, результат запроса) из ПЛК
    // Вспомогательный метод для записи BOOL значений и отдельных битов из WORD в ModbusTcp
    // Перед записью необходимо получить актуальное значение регистра (из ПЛК, а не из "memory"), чтобы не изменить соседний байт или биты
    public virtual (ushort, bool) GetRegister(int adr) { throw new NotImplementedException($"Метод {nameof(PLC)}.{nameof(GetRegister)} не реализован"); }

    // Запись
    public abstract bool Write(int adr, object value);
    #endregion
}
