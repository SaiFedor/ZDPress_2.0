using ConnectionPLC.PLC;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Connectivity;

public abstract class Tag : INotifyPropertyChanged
{
    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    //public abstract event Action<object> ValueChanged;
    //public abstract event EventHandler<object> ValueChanged;

    /// <summary>
    /// Возвращает адрес тега
    /// </summary>
    public int Adr { get; protected set; }
    /// <summary>
    /// Указывает на то, что тэг - настройка
    /// </summary>
    public bool IsPersistent { get; set; }
    public abstract object GetValue();
}

/// <summary>
/// Абстрактный универсальный класс Tag(T) задаёт поведение тега
/// конструктор new: -> Tag(int adr, ADS_Reader adsReader), получает адрес и класс связи с ПЛК
/// </summary>
/// <typeparam name="T">тип тега - развёрнутый тип</typeparam>
public abstract class Tag<T> : Tag, IGetNextAdr, IObservable<T> where T : unmanaged //struct
{
    private readonly Action plcGetValue;
    protected T value;  // значение тега
    protected PLC plc;  // класс связи с ПЛК
    public bool InitTag { get; protected set; } // для вызова OnPropertyChanged при запуске приложения, если Value равно значению по умолчанию

    //public override event Action<object>? ValueChanged;
    public event EventHandler<T>? ValueChanged;

    /// <summary>
    /// Возвращает или задаёт значение тега,
    /// при изменении значения уведомляет клиентов
    /// </summary>
    public T Value
    {
        get { return this.value; }
        set
        {
            if (!this.value.Equals(value))
            {
                this.value = value;
                OnPropertyChanged();
                ObserversOnNext();
                ValueChanged?.Invoke(this, Value);
            }
            else if (!InitTag)
            {
                OnPropertyChanged();
                ObserversOnNext();
                ValueChanged?.Invoke(this, Value);
                InitTag = true;
            }
        }
    }

    #region IObservable<T>
    private readonly List<IObserver<T>> observers = new();

    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
        return new Unsubscriber<T>(observers, observer);
    }

    private class Unsubscriber<V> : IDisposable
    {
        private readonly List<IObserver<V>> _observers;
        private readonly IObserver<V> _observer;

        public Unsubscriber(List<IObserver<V>> observers, IObserver<V> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }

    private void ObserversOnNext()
    {
        foreach (var observer in observers)
        {
            observer.OnNext(value);
        }
    }
    #endregion

    /// <summary>
    /// Возвращает размер тега в байтах
    /// </summary>
    public abstract int Size { get; }

    /// <summary>
    /// Возвращает следующий адрес
    /// return: адрес + размер тега
    /// </summary>
    public int NextAdr => Adr + Size;

    /// <summary>
    /// Конструктор тега.
    /// Передаваемый адрес, не обязательно должен быть кратным 2 для INT, кратным 4 для REAL и т.п.,
    /// он переопределится в зависимосит от типа тега.
    /// </summary>
    /// <param name="adr">адрес тега</param>
    /// <param name="adsReader">класс связи с ПЛК</param>
    public Tag(int adr, PLC plc)
    {
        // Приведение адреса к числу кратному размеру тега
        int d, a = adr;
        do
        {
            Math.DivRem(a, Size, out d);
            a++;
        } while (d != 0);
        this.Adr = --a;

        // Счётчик тегов в проекте
        CounterTags.Count++;

        this.plc = plc;
        this.plc.ReadComletedEvent += PlcReadComleted;  // присоединяет обработчик к событию чтения класса PLC

        // https://habr.com/ru/post/480416/
        // Первый вариант
        //if (typeof(T) == typeof(bool))
        //{
        //    plcGetValue = () => Value = (T)(object)plc.GetBOOL(Adr);
        //}
        // Второй вариант
        plcGetValue = default(T) switch
        {
            bool => () => {bool v = plc.GetBOOL(Adr); Value = Unsafe.As<bool, T>(ref v); },
            byte => () => { byte v = plc.GetBYTE(Adr); Value = Unsafe.As<byte, T>(ref v); },
            ushort => () => { ushort v = plc.GetWORD(Adr); Value = Unsafe.As<ushort, T>(ref v); },
            uint => () => { uint v = plc.GetDWORD(Adr); Value = Unsafe.As<uint, T>(ref v); },
            ulong => () => { ulong v = plc.GetLWORD(Adr); Value = Unsafe.As<ulong, T>(ref v); },
            sbyte => () => { sbyte v = plc.GetSBYTE(Adr); Value = Unsafe.As<sbyte, T>(ref v); },
            short => () => { short v = plc.GetINT(Adr); Value = Unsafe.As<short, T>(ref v); },
            int => () => { int v = plc.GetDINT(Adr); Value = Unsafe.As<int, T>(ref v); },
            long => () => { long v = plc.GetLINT(Adr); Value = Unsafe.As<long, T>(ref v); },
            float => () => { float v = plc.GetREAL(Adr); Value = Unsafe.As<float, T>(ref v); },
            double => () => { double v = plc.GetLREAL(Adr); Value = Unsafe.As<double, T>(ref v); },
            DateTime => () => { DateTime v = plc.GetDT(Adr); Value = Unsafe.As<DateTime, T>(ref v); },
            _ => throw new NotImplementedException($"{this.GetType()} в конструкторе при инициализации Action:{nameof(plcGetValue)} не определн тип {typeof(T)}"),
        };
    }

    /// <summary>
    /// Обработчик события чтения класса PLC
    /// </summary>
    //protected abstract void PlcReadComleted();
    protected virtual void PlcReadComleted() => plcGetValue();

    /// <summary>
    /// Записывает значение агрумента в соответствующий тег ПЛК
    /// </summary>
    /// <param name="val">новое значение тега</param>
    /// <returns>true - при успешной записи</returns>
    //public abstract bool Write(T val);
    public bool Write(T val) => plc.Write(Adr, val);

    /// <summary>
    /// Неявное преобразование 
    /// </summary>
    /// <param name="t"></param>
    /// <returns>возвращает значение тега</returns>
    public static implicit operator T(Tag<T> t) => t.value;

    public override object GetValue() => Value as object;
}
