using System.Reflection;

namespace DataStore
{
    internal class ColumnInfo
    {
        public MemberInfo Member { get; set; }
        public string ColumnName { get; set; }
    }
}
