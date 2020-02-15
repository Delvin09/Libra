using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Libra
{
    interface IPrintable
    {
        void Print();
    }

    interface IProcessable
    {
        void Process();
    }

    class Menu : IPrintable, IProcessable
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

    class MenuItem : IPrintable, IProcessable
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

    class Program
    {
        static void FindByAuthor()
        {
            using (var repository = new LibraryRepository())
            {
                Console.WriteLine("Enter author name: ");
                var name = Console.ReadLine();

                var books = repository.FindBooksByAuthorName(name).ToList();
                if (books.Count > 0)
                {
                    foreach (var b in books)
                    {
                        Console.WriteLine($"{b.BookId}\t{b.Title}\t{b.Genre}\t{b.Year}\t{b.Author}");
                    }
                }
                else
                    Console.WriteLine("Sorry, nothing found.");
                Console.ReadLine();
            }
        }

        static void FindByGenre()
        {
            using (var repository = new LibraryRepository())
            {
                Console.WriteLine("Choose genre: ");

                var i = 0;
                foreach (var g in Enum.GetNames(typeof(BookGenre)))
                    Console.WriteLine($"{i++}. {g}");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int genreInt))
                {
                    var books = repository.FindByGenre((BookGenre)genreInt).ToList();
                    if (books.Count > 0)
                    {
                        foreach (var b in books)
                        {
                            Console.WriteLine($"{b.BookId}\t{b.Title}\t{b.Author}\t{b.Year}");
                        }
                    }
                    else
                        Console.WriteLine("Sorry, nothing found.");
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }

                Console.ReadLine();
            }
        }

        static void FindByYear()
        {
            using (var repository = new LibraryRepository())
            {
                Console.WriteLine("Enter year: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int year))
                {
                    var books = repository.FindByYear(year).ToList();
                    if (books.Count > 0)
                    {
                        foreach (var b in books)
                        {
                            Console.WriteLine($"{b.BookId}\t{b.Title}\t{b.Genre}\t{b.Author}");
                        }
                    }
                    else
                        Console.WriteLine("Sorry, nothing found.");
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }

                Console.ReadLine();
            }
        }

        static void AddBook()
        {
            using (var repository = new LibraryRepository())
            {
                Console.WriteLine("Enter book title: ");
                var title = Console.ReadLine();

                string yearStr; int year;
                do {
                    Console.WriteLine("Enter book year: ");
                    yearStr = Console.ReadLine();
                } while (!int.TryParse(yearStr, out year));

                string genreStr; BookGenre genre;
                do
                {
                    Console.WriteLine("Enter book genre: ");
                    genreStr = Console.ReadLine();
                } while (!Enum.TryParse(genreStr, out genre));

                Console.WriteLine("Enter author name: ");
                var authorNames = Console.ReadLine();
                string firstName, midName = string.Empty, lastName;
                var names = authorNames.Split(' ');
                if (names.Length > 2)
                {
                    firstName = names[0];
                    midName = names[1];
                    lastName = names[2];
                }
                else
                {
                    firstName = names[0];
                    lastName = names[1];
                }

                string bdStr; DateTime bd;
                do
                {
                    Console.WriteLine("Enter author birth date: ");
                    bdStr = Console.ReadLine();
                }
                while (!DateTime.TryParse(bdStr, out bd));

                var existingAuthor = repository.FindAuthorByName(firstName, midName, lastName, bd).FirstOrDefault();
                if (existingAuthor == null)
                {
                    existingAuthor = new Author()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        MiddleName = midName,
                        Birthday = bd
                    };
                }

                repository.Add(new Book()
                {
                    BookId = Guid.NewGuid(),
                    Client = null,
                    Title = title,
                    Year = year,
                    Genre = genre,
                    Author = existingAuthor
                });
            }
        }

        static void RemoveBook()
        {
            using (var repository = new LibraryRepository())
            {
                Console.WriteLine("Enter Id of book: ");
                var input = Console.ReadLine();
                if (Guid.TryParse(input, out Guid bookId)) {
                    if (repository.RemoveById(bookId) != null)
                    {
                        Console.WriteLine("Book removed success!");
                    }
                    else
                    {
                        Console.WriteLine("Can't find any book with this id!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }

                Console.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            var menuItems = new MenuItem[] {
                new MenuItem(1, "Find book by author", FindByAuthor),
                new MenuItem(2, "Find book by genre", FindByGenre),
                new MenuItem(3, "Find book by year", FindByYear),
                new MenuItem(4, "Add book", AddBook),
                new MenuItem(5, "Remove book", RemoveBook)
            };

            var menu = new Menu(menuItems);
            menu.Process();
        }
    }
}
