using System;
using System.Linq;
using System.Threading.Tasks;
using Model;
using Ui.Common;

namespace Libra
{
    class FindSubmenu
    {
        [MenuItem(1, "Find book by author")]
        public async void FindByAuthor()
        {
            using (var repository = new LibraryRepository())
            {
                Console.WriteLine("Enter author name: ");
                var name = Console.ReadLine();

                var books = (await repository.FindBooksByAuthorName(name)).ToList();
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

        [MenuItem(2, "Find book by genre")]
        public async void FindByGenre()
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
                    var books = (await repository.FindByGenre((BookGenre)genreInt)).ToList();
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

        [MenuItem(3, "Find book by year")]
        public async Task FindByYear()
        {
            using (var repository = new LibraryRepository())
            {
                Console.WriteLine("Enter year: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int year))
                {
                    var books = (await repository.FindByYear(year)).ToList();
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
    }
}
