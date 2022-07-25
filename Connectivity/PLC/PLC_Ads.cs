using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Text;
using TwinCAT.Ads;

namespace Connectivity;

/// <summary>
/// Обеспечивает связь c ПЛК Beckhoff по ADS
/// </summary>
public class PLC_Ads : PLC
{
    //readonly Memory<byte> memory;
    readonly AdsClient adsClientR;
    readonly AdsClient adsClientW;
    readonly AmsAddress amsAddress;
    const uint IND_GR = 0x4020; // indexGroup 0x4020 http://infosys.beckhoff.com/content/1033/tcadsdeviceplc/html/tcadsdeviceplc_indexprocimage.htm?id=79124218146136235237

    public override event Action? ReadComletedEvent;
    public override event Action<bool>? ReadStatusChangedEvent;
    public override event Action<string>? PlcStateChangedEvent;

    /// <summary>
    /// Происходит после вызова метода Connect экземпляра TcAdsClient,
    /// если не было выброшено исключение
    /// </summary>
    public event Action<bool>? ConnectedEvent;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="amsNetId">AmsNetId: 192.168.1.X.1.1</param>
    /// <param name="port">порт (801 - для ADS соединения (TwinCAT 2, 851 - TwinCAT 3); 502 для BC </param>
    /// <param name="sizeDataRead">размер данных в байтах, считываемых из ПЛК методом Read и ReadAsync</param>
    public PLC_Ads(string amsNetId, int port = 801, int sizeDataRead = 24000) : base(amsNetId, port, sizeDataRead)
    {
        //memory = new Memory<byte>(new byte[sizeDataRead]);
        amsAddress = new AmsAddress(amsNetId, port);
        AdsClientSettings sett = new(1000);
        adsClientR = new AdsClient(sett);
        adsClientW = new AdsClient(sett);
        adsClientR.ConnectionStateChanged += TcAdsClientRead_ConnectionStateChanged;
        adsClientR.AdsStateChanged += TcAdsClientRead_AdsStateChanged;
        adsClientR.AdsNotification += TcAdsClientRead_AdsNotification;
    }

    private void TcAdsClientRead_ConnectionStateChanged(object? sender, TwinCAT.ConnectionStateChangedEventArgs e)
    {
        ConnectedEvent?.Invoke(e.NewState == TwinCAT.ConnectionState.Connected);
    }

    private void TcAdsClientRead_AdsStateChanged(object? sender, AdsStateChangedEventArgs e)
    {
        AdsState = e.State.AdsState.ToString();
        PlcStateChangedEvent?.Invoke(AdsState);
    }

    private void TcAdsClientRead_AdsNotification(object? sender, AdsNotificationEventArgs e)
    {
        ;// throw new NotImplementedException();
    }

    public bool Connect()
    {
        try
        {
            adsClientR.Connect(amsAddress);
            adsClientW.Connect(amsAddress);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in {nameof(PLC_Ads)}.{nameof(Connect)}: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> ConnectAsync(CancellationToken token)
    {
        try
        {
            await adsClientR.ConnectAndWaitAsync(amsAddress, token);
            await adsClientW.ConnectAndWaitAsync(amsAddress, token);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in {nameof(PLC_Ads)}.{nameof(Connect)}: {ex.Message}");
            return false;
        }
    }

    protected virtual void OnReadComleted()
    {
        ReadComletedEvent?.Invoke();
    }

    /// <summary>
    /// Запускает процесс чтения данных из ПЛК
    /// </summary>
    /// <returns>CancellationTokenSource - для останова чтения</returns>
    public override CancellationTokenSource StartRead()
    {
        CancellationTokenSource cancelTokenSource = new();
        SynchronizationContext? uiContext = SynchronizationContext.Current;
        Thread pollThread = new(() => PollThread(uiContext, cancelTokenSource.Token));
        pollThread.Start();
        return cancelTokenSource;
    }

    private void PollThread(object? uiContext, CancellationToken token)
    {
        SynchronizationContext context = uiContext as SynchronizationContext ??
            throw new InvalidOperationException($"{nameof(PLC_Ads)}.{nameof(StartRead)}: SynchronizationContext.Current не может быть Null");
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
            if (adsClientR.IsConnected)
            {
                AdsErrorCode adsErrorCode = adsClientR.TryRead(IND_GR, 0, memory, out int readBytes);
                if (adsErrorCode == 0)
                {
                    context.Post(ReadComleted, null);
                    readStatus(true);
                }
                else
                {
                    readStatus(false);
                }
            }
            else
            {
                readStatus(Connect());
            }
            //Thread.Sleep(DelayReadAsync);
            try
            {
                Task.Delay(DelayReadAsync, token).Wait(token);
            }
            catch { }
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
        byte[] bArr = value switch
        {
            bool => BitConverter.GetBytes((bool)value),
            byte => BitConverter.GetBytes((byte)value),
            short => BitConverter.GetBytes((short)value),
            ushort => BitConverter.GetBytes((ushort)value),
            uint => BitConverter.GetBytes((uint)value),
            float => BitConverter.GetBytes((float)value),
            DateTime => BitConverter.GetBytes((UInt32)((DateTime)value).Subtract(new DateTime(1970, 1, 1)).TotalSeconds),
            TimeSpan => BitConverter.GetBytes((UInt32)((TimeSpan)value).TotalMilliseconds),
            _ => throw new ArgumentException($"{this.GetType()}.{nameof(Write)} запись значения типа {value.GetType()} не реализована")
        };
        AdsErrorCode errCode;
        try
        {
            errCode = adsClientW.TryWrite(IND_GR, (uint)adr, new ReadOnlyMemory<byte>(bArr));
        }
        catch
        {
            errCode = AdsErrorCode.ClientError;
        }
        return errCode == AdsErrorCode.NoError;
    }
    #endregion

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
            adsClientR?.Dispose(); // после того как сделал ConnectAsync(), сдесь выбрасывается исключение???
            adsClientW?.Dispose();
        }
        disposed = true;
    }
    #endregion
}
