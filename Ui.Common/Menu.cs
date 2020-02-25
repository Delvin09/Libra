using System;
using System.Linq;
using System.Collections.Generic;
using Ui.Common.Interfaces;
using System.Threading.Tasks;

namespace Ui.Common
{
    internal class Menu : IMenu
    {
        private List<IMenuItem> _items = new List<IMenuItem>();

        public IEnumerable<IMenuItem> Items => _items;

        public int Num { get; }

        public string Title { get; }

        public int Order { get; }

        public Menu(int num = -1, int order = 0, string title = null, params IMenuItem[] items)
        {
            Num = num;
            Title = title;
            Order = order;
            _items.AddRange(items);
        }

        internal void AddItem(IMenuItem menuItem)
        {
            _items.Add(menuItem);
        }

        public async Task<bool> Process()
        {
            var isExit = false;
            while (!isExit)
            {
                Print();
                var input = Console.ReadLine();
                if (int.TryParse(input, out int num) && Items.Any(i => i.Num == num))
                {
                    foreach (var item in Items.Where(i => i.Num == num))
                    {
                        isExit = await item.Process();
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input! Press Enter to continue.");
                    Console.ReadLine();
                }

                Console.Clear();
            }
            return false;
        }

        public void Print()
        {
            Console.Clear();
            foreach (var item in _items.OrderBy(i => i.Order).ThenBy(i => i.Num))
                Console.WriteLine($"{item.Num}. {item.Title}");
        }
    }
}
