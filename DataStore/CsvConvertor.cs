using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DataStore
{
    public class CsvConvertor
    {
        public static readonly string Delimiter = ",";

        public static async Task Serialize<T>(IEnumerable<T> obj, Stream stream)
        {
            var type = typeof(T);

            var property = type.GetProperties().Where(t => IsPrimitive(t.PropertyType) && t.GetCustomAttribute<IgnoreCsvAttribute>() == null);
            var fields = type.GetFields().Where(t => IsPrimitive(t.FieldType) && t.GetCustomAttribute<IgnoreCsvAttribute>() == null);

            var columnList = new List<ColumnInfo>();
            columnList.AddRange(property.Select(p => new ColumnInfo { ColumnName = GetColumnName(p), Member = p })
                .Concat(fields.Select(f => new ColumnInfo { ColumnName = GetColumnName(f), Member = f })));

            var writer = new StreamWriter(stream);

            await AddHeader(columnList, writer);
            await AddRows(obj, columnList, writer);

            await writer.FlushAsync();
        }

        private static async Task AddHeader(List<ColumnInfo> columnList, StreamWriter writer)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < columnList.Count; i++)
            {
                if (i > 0)
                    stringBuilder.Append(Delimiter);
                stringBuilder.Append(columnList[i].ColumnName);
            }

            await writer.WriteLineAsync(stringBuilder.ToString());
        }

        private static async Task AddRows<T>(IEnumerable<T> obj, List<ColumnInfo> columnList, StreamWriter writer)
        {
            foreach (var item in obj)
            {
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < columnList.Count; i++)
                {
                    string value = string.Empty;
                    var memberInfo = columnList[i].Member;
                    if (memberInfo is PropertyInfo propertyInfo)
                    {
                        value = propertyInfo.GetValue(item).ToString();
                    }
                    if (memberInfo is FieldInfo fieldInfo)
                    {
                        value = fieldInfo.GetValue(item).ToString();
                    }

                    if (i > 0)
                        stringBuilder.Append(Delimiter);
                    stringBuilder.Append(value);
                }

                await writer.WriteLineAsync(stringBuilder.ToString());
            }
        }

        private static bool IsPrimitive(Type type)
        {
            return type.IsPrimitive || type.IsEnum || type.Name == "String";
        }

        private static string GetColumnName(MemberInfo memberInfo)
        {
            string name = memberInfo.Name;
            var nameAttribute = memberInfo.GetCustomAttribute<ColumnNameCsvAttribute>();
            if (nameAttribute != null)
                name = nameAttribute.Name;

            if (name.Contains(" "))
                name = $"\"{name}\"";
            return name;
        }
    }
}
