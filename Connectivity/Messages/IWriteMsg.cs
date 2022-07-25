namespace Connectivity.Messages;

public interface IWriteMsg
{
    /// <summary>
    /// Записывает сообщение в БД
    /// </summary>
    /// <param name="msgText">Сообщение</param>
    /// <param name="msgStatus">Статус сообщения</param>
    /// <param name="msgDateTime">Дата/время возникновения события</param>
    /// <param name="millisecond">Миллисекунды события</param>
    /// <param name="microsecond">Микросекунды события</param>
    public void WriteMsg(string msgText, string msgStatus = "Служебное", DateTime? msgDateTime = null, int millisecond = 0, int microsecond = 0);
}
