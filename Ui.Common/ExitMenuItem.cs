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
        public ExitMenuItem(int num = 0, int order = int.MaxValue, string title = "Exit", Action handler = null)
            : base(num, order, title, handler)
        {
        }

        public override bool Process() => true;
    }
}
