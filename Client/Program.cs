using Model;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ui.Common;

using ClientModel = Model.Entities.Client;

namespace Client
{
    class Program
    {
        private static ClientModel _currentClient;

        [MenuItem(1, "Borrow book")]
        static void BorrowBook()
        {
            using (var repository = new LibraryRepository())
            {
                Console.WriteLine("Enter book title: ");
                var title = Console.ReadLine();

                Console.WriteLine("Enter book year: ");
                var year = int.Parse(Console.ReadLine());

                var book = repository.FindByYear(year).Where(b => b.Title == title).FirstOrDefault();
                if (book == null)
                    Console.WriteLine("Can't find any book");
                else if (book.Client != null)
                    Console.WriteLine("This book is borrowed");
                else
                {
                    var currentClient = repository.FindClient(_currentClient.Name);
                    book.Client = currentClient;
                    currentClient.History = new List<BorrowedHistory>();
                    currentClient.History.Add(new BorrowedHistory()
                    {
                        Client = currentClient,
                        Book = book,
                        BorrowDate = DateTime.Now
                    });
                }
            }
        }

        [MenuItem(2, "Return book")]
        static void ReturnBook()
        {
            using (var repository = new LibraryRepository())
            {
                Console.WriteLine("Enter borrowed id: ");
                var id = int.Parse(Console.ReadLine());

                var history = repository.GetAllBorrowed(_currentClient).Single(h => h.Id == id);
                history.ReturnDate = DateTime.Now;
            }
        }

        [MenuItem(3, "View borrowed books")]
        static void ViewBorrowBook()
        {
            using (var repository = new LibraryRepository())
            {
                var borrowedBooks = repository.GetAllBorrowed(_currentClient)?.Where(h => h.ReturnDate == null);
                if (borrowedBooks != null && borrowedBooks.Any())
                {
                    foreach (var item in borrowedBooks)
                    {
                        Console.WriteLine($"{item.Id}\t{item.BorrowDate}\t{item.Book.Title}");
                    }
                }
                else
                {
                    Console.WriteLine("No borrowed books");
                }
            }
            Console.ReadLine();
        }

        [MenuItem(4, "View history of borrowed books")]
        static void ViewHistoryBorrowBook()
        {
            using (var repository = new LibraryRepository())
            {
                var borrowedBooks = repository.GetAllBorrowed(_currentClient)?.Where(h => h.ReturnDate != null);
                if (borrowedBooks != null && borrowedBooks.Any())
                {
                    foreach (var item in borrowedBooks)
                    {
                        Console.WriteLine($"{item.Id}\t{item.BorrowDate}\t{item.ReturnDate}\t{item.Book.Title}");
                    }
                }
                else
                {
                    Console.WriteLine("No borrowed books");
                }
            }

            Console.ReadLine();
        }

        private static ClientModel FindClient(string name)
        {
            using (var repository = new LibraryRepository())
            {
                return repository.FindClient(name);
            }
        }

        private static ClientModel CreateClient(string clientName)
        {
            using (var repository = new LibraryRepository())
            {
                return repository.CreateClient(clientName);
            }
        }

        private static void Login()
        {
            Console.WriteLine("Enter your name: ");
            var clientName = Console.ReadLine();
            var client = FindClient(clientName);
            if (client != null)
                _currentClient = client;
            else
                _currentClient = CreateClient(clientName);
        }

        static void Main(string[] args)
        {
            Login();

            MenuBuilder.Default
                .DetectMenuOn<Program>()
                .Build();

            //var menuItems = new MenuItem[] {
            //    new MenuItem(1, "Borrow book", BorrowBook),
            //    new MenuItem(2, "Return book", ReturnBook),
            //    new MenuItem(3, "View borrowed books", ViewBorrowBook),
            //    new MenuItem(4, "View history of borrowed books", ViewHistoryBorrowBook),
            //};

            //var menu = new Menu(menuItems);
            //menu.Process();
        }
    }
}
