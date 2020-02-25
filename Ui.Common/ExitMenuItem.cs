using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ui.Common.Interfaces;

namespace Ui.Common
{
    internal class ExitMenuItem : MenuItem
    {
        public ExitMenuItem(int num = 0, int order = int.MaxValue, string title = "Exit", Func<Task> handler = null)
            : base(num, order, title, handler)
        {
        }

        public override Task<bool> Process() => Task.FromResult(true);
    }
}
