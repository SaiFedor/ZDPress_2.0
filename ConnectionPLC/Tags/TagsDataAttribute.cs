using System;

namespace ConnectionPLC
{
    /// <summary>
    /// Указывает что класс содержит теги и структуры тегов, для создания коллекции всех тегов TagsData
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TagsDataAttribute : Attribute
    {
    }
}