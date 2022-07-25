using System.ComponentModel;

namespace Connectivity.Messages;

public static class MsgTable
{
    /// <summary>
    /// Список всех текущих сообщений системы для привязки к DataGrid (таблица текущих сообщений)
    /// </summary>
    public static BindingList<Msg> Msgs { get; private set; } = new();
}
