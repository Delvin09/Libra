using System.Collections.Generic;

namespace Ui.Common.Interfaces
{
    public interface IMenu : IMenuItem
    {
        IEnumerable<IMenuItem> Items { get; }
    }
}
