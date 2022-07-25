using ConnectionPLC.PLC;
using System.Reflection;

namespace Connectivity;

public abstract class ClassTag
{
    public string Name { get; set; }
    public string Unit { get; set; }
    private static int nextAdr = 0;
    public static int NextAdr => nextAdr;
    private readonly int adress;

    public ClassTag(int adr, PLC plc, string name, string unit = "")
    {
        Name = name;
        Unit = unit;
        adress = adr;
        string exMes = $"new {nameof(ClassTag)} - {Name}";

        FieldInfo[] fields = this.GetType().GetFields();
        if (fields.Length == 0) throw new Exception($"{exMes}: не содержит ни одного поля имеющего тип наследника от {nameof(Tag)}");

        for (int i = 0; i < fields.Length; i++)
        {
            int adrr = i == 0 ? adr : (fields[i - 1].GetValue(this) as IGetNextAdr ?? throw new Exception(exMes)).NextAdr;
            InitField(fields[i], adrr);
            if (fields[i].GetCustomAttributes(typeof(PersistentAttribute), true).Any())
            {
                var tag = fields[i].GetValue(this) as Tag ?? throw new Exception(exMes);
                tag.IsPersistent = true;
            }
        }

        nextAdr = (fields[^1].GetValue(this) as IGetNextAdr ?? throw new Exception(exMes)).NextAdr;

        void InitField(FieldInfo fieldInfo, int adr)
        {
            Type t = fieldInfo.FieldType;

            if (t == typeof(BOOL))
                fieldInfo.SetValue(this, new BOOL(adr, plc));

            else if (t == typeof(BYTE))
                fieldInfo.SetValue(this, new BYTE(adr, plc));
            else if (t == typeof(WORD))
                fieldInfo.SetValue(this, new WORD(adr, plc));
            else if (t == typeof(DWORD))
                fieldInfo.SetValue(this, new DWORD(adr, plc));
            else if (t == typeof(LWORD))
                fieldInfo.SetValue(this, new LWORD(adr, plc));

            else if (t == typeof(SINT))
                fieldInfo.SetValue(this, new SINT(adr, plc));
            else if (t == typeof(USINT))
                fieldInfo.SetValue(this, new USINT(adr, plc));
            else if (t == typeof(INT))
                fieldInfo.SetValue(this, new INT(adr, plc));
            else if (t == typeof(UINT))
                fieldInfo.SetValue(this, new UINT(adr, plc));
            else if (t == typeof(DINT))
                fieldInfo.SetValue(this, new DINT(adr, plc));
            else if (t == typeof(UDINT))
                fieldInfo.SetValue(this, new UDINT(adr, plc));
            else if (t == typeof(LINT))
                fieldInfo.SetValue(this, new LINT(adr, plc));
            else if (t == typeof(ULINT))
                fieldInfo.SetValue(this, new ULINT(adr, plc));

            else if (t == typeof(REAL))
                fieldInfo.SetValue(this, new REAL(adr, plc));
            else if (t == typeof(LREAL))
                fieldInfo.SetValue(this, new LREAL(adr, plc));

            else if (t == typeof(DATETIME))
                fieldInfo.SetValue(this, new DATETIME(adr, plc));

            else if (t == typeof(STRING))
            {
                int len;
                if (fieldInfo.GetCustomAttributes(typeof(LenAttribute), true).FirstOrDefault() is LenAttribute lenAttribute)
                {
                    len = lenAttribute.Len;
                }
                else
                {
                    throw new Exception($"{exMes}: нет атрибута {nameof(LenAttribute)} у {nameof(STRING)}. Длина строки не определена");
                }
                fieldInfo.SetValue(this, new STRING(adr, plc, len));
            }
            else
                throw new Exception($"{exMes} {nameof(InitField)}: тег типа {t} не определен");
        }
    }

    public virtual Dictionary<string, Tag> GetTagsFromRestore()
    {
        Dictionary<string, Tag> dictionary = new();
        foreach (FieldInfo item in this.GetType().GetFields())
        {
            if (item.FieldType.IsSubclassOf(typeof(Tag)))
            {
                dictionary.Add(item.Name, GetTag(item));
            }
        }
        return dictionary;
    }

    public Dictionary<int, object> GetTagsFromRestoreAtr()
    {
        Dictionary<int, object> dictionary = new();
        foreach (FieldInfo item in this.GetType().GetFields())
        {
            if (item.FieldType.IsSubclassOf(typeof(Tag)) && item.GetCustomAttributes(typeof(PersistentAttribute), true).Any())
            {
                var tag = GetTag(item);
                dictionary.Add(tag.Adr, tag.GetValue());
            }
        }
        return dictionary;
    }

    public virtual Dictionary<string, Tag> GetTagsFromTagsData()
    {
        Dictionary<string, Tag> dict = new();
        foreach (FieldInfo item in this.GetType().GetFields())
        {
            if (item.FieldType.IsSubclassOf(typeof(Tag)))
            {
                dict.Add(item.Name, GetTag(item));
            }
        }
        return dict;
    }

    private Tag GetTag(FieldInfo fieldInfo) => fieldInfo.GetValue(this) as Tag ?? throw new ArgumentException($"{nameof(fieldInfo)}{fieldInfo} не является наследником от {nameof(Tag)}");

    public override int GetHashCode() => adress.GetHashCode();
}
