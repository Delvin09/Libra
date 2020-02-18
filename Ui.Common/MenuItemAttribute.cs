using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ui.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class MenuItemAttribute : Attribute
    {
        public MenuItemAttribute(int num, string description)
        {
            Num = num;
            Description = description;
        }

        public int Num { get; }
        public string Description { get; }
    }

    public interface IMenuHandler
    {
        void Handle();
    }
}
