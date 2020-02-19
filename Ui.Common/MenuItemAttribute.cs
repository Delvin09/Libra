using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ui.Common
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class MenuItemAttribute : Attribute
    {
        public MenuItemAttribute(int num, string description)
        {
            Num = num;
            Title = description;
        }

        public int Num { get; }
        public string Title { get; }
        public int Order { get; set; } = 0;
    }
}
