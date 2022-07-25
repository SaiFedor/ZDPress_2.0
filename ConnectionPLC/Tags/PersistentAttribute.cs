namespace Connectivity;
/// <summary>
/// Помечает типы тегов (INT, REAL,...) как настройки, для сохранения их значений на диск
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class PersistentAttribute : Attribute
{
}
