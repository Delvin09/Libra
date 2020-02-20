using System;
using System.Linq;
using Model;
using Ui.Common;

namespace Libra
{
    class MainMenu
    {
        [MenuItem(1, "Find")]
        public FindSubmenu FindSubmenu { get; } = new FindSubmenu();

        [MenuItem(2, "Add book")]
        public void AddBook()
        {
            using (var repository = new LibraryRepository())
            {
                Console.WriteLine("Enter book title: ");
                var title = Console.ReadLine();

                string yearStr; int year;
                do
                {
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

        [MenuItem(3, "Remove book")]
        public void RemoveBook()
        {
            using (var repository = new LibraryRepository())
            {
                Console.WriteLine("Enter Id of book: ");
                var input = Console.ReadLine();
                if (Guid.TryParse(input, out Guid bookId))
                {
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

        [MenuItem(4, "Export")]
        public ExportSubmenu ExportSubmenu { get; } = new ExportSubmenu();
    }
}
