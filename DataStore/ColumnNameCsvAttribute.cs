using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ColumnNameCsvAttribute : Attribute
    {
        public ColumnNameCsvAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
