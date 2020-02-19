using System;
using Ui.Common.Interfaces;

namespace Ui.Common
{
    internal class MenuItem : IMenuItem
    {
        private Action _processHandler;

        public string Title { get; }

        public int Num { get; }

        public int Order { get; }

        public MenuItem(int num, int order, string title, Action processHandler)
        {
            Title = title;
            Num = num;
            Order = order;
            _processHandler = processHandler;
        }

        public virtual bool Process()
        {
            _processHandler?.Invoke();
            return false;
        }
    }
}
