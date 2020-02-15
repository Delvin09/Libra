using System;
using System.Linq;
using System.Collections.Generic;

namespace Ui.Common
{
    public class Menu : IPrintable, IProcessable
    {
        private List<MenuItem> _items = new List<MenuItem>();
        public IEnumerable<MenuItem> Items
        {
            get => _items;
        }

        public Menu(params MenuItem[] items)
        {
            _items.AddRange(items);
        }

        public void Process()
        {
            while (true)
            {
                Print();
                var input = Console.ReadLine();
                if (int.TryParse(input, out int num) && Items.Any(i => i.Num == num))
                {
                    foreach (var item in Items.Where(i => i.Num == num))
                    {
                        item.Process();
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input! Press Enter to continue.");
                    Console.ReadLine();
                }

                Console.Clear();
            }
        }

        public void Print()
        {
            foreach (var item in _items)
                item.Print();
        }
    }
}
