using System;

namespace Ui.Common
{
    public class MenuItem : IPrintable, IProcessable
    {
        private Action _processHandler;

        public string Title { get; }
        public int Num { get; }

        public MenuItem(int num, string title, Action processHandler)
        {
            Title = title;
            Num = num;
            _processHandler = processHandler;
        }

        public void Print()
        {
            Console.WriteLine($"{Num}. {Title}");
        }

        public void Process()
        {
            _processHandler?.Invoke();
        }
    }
}
