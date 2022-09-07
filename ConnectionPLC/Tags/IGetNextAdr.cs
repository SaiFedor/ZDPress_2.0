namespace ConnectionPLC
{

    /// <summary>
    /// Задаёт свойство, которое возвращает адрес тега
    /// Необходим для создания "структур" из тегов
    /// </summary>
    public interface IGetNextAdr
    {
        /// <summary>
        /// Возвращает адрес тега
        /// </summary>
        int NextAdr { get; }
    }
}