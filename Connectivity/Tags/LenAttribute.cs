namespace Connectivity;
/// <summary>
/// Указывает длину строки в теге STRING
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class LenAttribute : Attribute
{
    public int Len { get; init; }
    public LenAttribute(int len)
    {
        Len = len;
    }
}