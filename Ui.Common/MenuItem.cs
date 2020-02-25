using System;
using System.Threading.Tasks;
using Ui.Common.Interfaces;

namespace Ui.Common
{
    internal class MenuItem : IMenuItem
    {
        private Func<Task> _processHandler;

        public string Title { get; }

        public int Num { get; }

        public int Order { get; }

        public MenuItem(int num, int order, string title, Func<Task> processHandler)
        {
            Title = title;
            Num = num;
            Order = order;
            _processHandler = processHandler;
        }

        public virtual async Task<bool> Process()
        {
            await _processHandler?.Invoke();
            return false;
        }
    }
}
