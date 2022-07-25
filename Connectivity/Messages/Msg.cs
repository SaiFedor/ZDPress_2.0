namespace Connectivity.Messages;

/// <summary>
/// Запись: содержит информацию о сообщении системы (дата/время, статус, текст) 
/// Используется на главном экране в таблице текущих сообщений
/// </summary>
public record Msg(DateTime DateTime, string Text, MsgStatus Status)
{
    private bool confirm;
    public event Action<Msg>? ConfirmChanged; // Событие, возникает при квитировании сообщения

    /// <summary>
    /// При измененеии, если value=true, то вызывает событие , которое удаляет данное сообщение из таблицы сообщений
    /// </summary>
    public bool Confirm
    {
        get => confirm;
        set { confirm = value; if (value) ConfirmChanged?.Invoke(this); }
    }
}
