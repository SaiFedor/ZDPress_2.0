using System;

namespace ConnectionPLC
{
    /// <summary>
    /// Указывает длину строки в теге STRING
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class LenAttribute : Attribute
    {
        public int Len { get; set; }
        public LenAttribute(int len)
        {
            Len = len;
        }
    }
}