using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ConnectionPLC {

    /// <summary>
    /// Хранит все тэги проекта
    /// </summary>
    public static class TagsData
    {
        /// <summary>
        /// Название приложения которое вызывает lib
        /// </summary>
        public static string AppName { get; private set; } = "PowerSCADA";

        /// <summary>
        /// Словарь со всеми тэгами проекта, где
        /// key - полное имя тэга (например "PowerSCADA.AIn.PESteam.ScaleMin"), value - ссылка на тэг
        /// Заполняяется вызовом метода PowerSCADA.TagDataBuilder.Build()
        /// </summary>
        public static Dictionary<string, Tag> Data { get; private set; } = new Dictionary<string, Tag>();

        /// <summary>
        /// Заполняет словарь со всеми тэгами проекта "Data"
        /// </summary>
        /// <param name="appName">Имя приложения</param>
        /// <param name="tags">Список из статических классов, содержащих классы тегов и отдельные теги в виде свойтсв и полей</param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public static void Build(string appName = null, List<Type> tags = null)
        {
            if (appName != null) AppName = appName;
            // Заполнить коллекцию один раз
            if (Data.Count != 0) throw new Exception("Метод можно выполнить один раз при запуске приложения");

            List<Type> typesClassWithTags;
            if (tags != null)
            {
                typesClassWithTags = tags;
            }
            else
            {
                typesClassWithTags = new List<Type>();
                var scadaAssembli = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(a => a.FullName?.Contains(AppName) ?? false) ?? throw new Exception($"Не найдена сборка с именем {AppName} при заполнении словаря со всеми тэгами проекта");
                foreach (var item in scadaAssembli.GetTypes())
                {
                    if (item.GetCustomAttributes(typeof(TagsDataAttribute), false).Any())
                    {
                        typesClassWithTags.Add(item);
                    }
                }
            }

            foreach (var type in typesClassWithTags)//tags)
            {
                List<MemberInfo> listMemInf = new List<MemberInfo>();
                listMemInf.AddRange(type.GetProperties());
                listMemInf.AddRange(type.GetFields());

                foreach (var memberInfo in listMemInf)
                {
                    var propOrField = new object();
                    switch (memberInfo.MemberType)
                    {
                        case MemberTypes.Property:
                            propOrField = ((PropertyInfo)memberInfo).GetValue(null);
                            break;
                        case MemberTypes.Field:
                             propOrField = ((PropertyInfo)memberInfo).GetValue(null);
                            break;
                        default:
                            break;
                    }

                    //var propOrField = memberInfo.MemberType switch
                    //{
                    //    MemberTypes.Property => ((PropertyInfo)memberInfo).GetValue(null),
                    //    MemberTypes.Field => ((FieldInfo)memberInfo).GetValue(null),
                    //    _ => throw new NotImplementedException(),
                    //};

                    switch (propOrField)
                    {
                        case ClassTag clT:
                            {
                                foreach (var v in clT.GetTagsFromTagsData())
                                {
                                    Data.Add($"{memberInfo.DeclaringType}.{memberInfo.Name}.{v.Key}", v.Value);
                                }
                                break;
                            }
                        case Tag tag:
                            {
                                Data.Add($"{memberInfo.DeclaringType}.{memberInfo.Name}", tag);
                                break;
                            }
                        // Для классов содержащих массивы из ClassTag, например для ST_DIn[] In из DIn (проверка на массив необходима, т.к. имеются List<ClassTag> которые не надо добавлять в Data)
                        case IEnumerable<ClassTag> arrClassTag when arrClassTag.GetType().IsArray:
                            {
                                string name = $"{type.FullName}.{memberInfo.Name}";
                                int t = 0;
                                foreach (ClassTag item in arrClassTag)
                                {
                                    foreach (var v in item.GetTagsFromTagsData())
                                    {
                                        Data.Add($"{name}[{t}].{v.Key}", v.Value); // тогда ключ в Data будет иметь вид: PowerSCADA.DIn.In[0].Filter
                                    }
                                    t++;
                                }
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает тэг по имени
        /// </summary>
        /// <param name="tagName">Полное имя тэга, например: "PowerSCADA.AIn.PESteam.ScaleMin" или "AIn.PESteam.ScaleMin"</param>
        /// <returns></returns>
        public static Tag GetTag(string tagName,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (Data.Count == 0) return null;
            if (string.IsNullOrEmpty(tagName)) throw new ArgumentNullException(nameof(tagName));
            return Data.FirstOrDefault((t) => t.Key == (tagName.StartsWith($"{AppName}.") ? tagName : $"{AppName}.{tagName}")).Value ??
                throw new KeyNotFoundException($"{sourceFilePath} строка {sourceLineNumber} метод {memberName}: В коллекции тегов {nameof(TagsData)} не найден тэг с именем {tagName}");
        }

        /// <summary>
        /// Возвращает словарь из всех Persistent тэгов проекта, где key - адрес тега, а value - значение тега приведенное к object 
        /// </summary>
        /// <returns>Dictionary<int, object> key - адрес тега, а value - значение тега приведенное к object</returns>
        public static Dictionary<int, object> GetPersistentTagsDictionary()
        {
            return Data.Where(t => t.Value.IsPersistent).ToDictionary(t => t.Value.Adr, t => t.Value.GetValue());
        }
    }
}