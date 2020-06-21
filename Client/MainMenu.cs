using Model;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Ui.Common;

namespace Client
{
    class MainMenu
    {
        [MenuItem(1, "Borrow book")]
        public async void BorrowBook()
        {
            using (var repository = new LibraryRepository())
            {
                Console.WriteLine("Enter book title: ");
                var title = Console.ReadLine();

                Console.WriteLine("Enter book year: ");
                var year = int.Parse(Console.ReadLine());

                var book = (await repository.FindByYear(year)).Where(b => b.Title == title).FirstOrDefault();
                if (book == null)
                    Console.WriteLine("Can't find any book");
                else if (book.Client != null)
                    Console.WriteLine("This book is borrowed");
                else
                {
                    var currentClient = await repository.FindClient(ClientService.CurrentClient.Name);
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
        public async void ReturnBook()
        {
            using (var repository = new LibraryRepository())
            {
                Console.WriteLine("Enter borrowed id: ");
                var id = int.Parse(Console.ReadLine());

                var history = (await repository.GetAllBorrowed(ClientService.CurrentClient)).Single(h => h.Id == id);
                history.ReturnDate = DateTime.Now;
            }
        }

        [MenuItem(3, "View borrowed books")]
        public async void ViewBorrowBook()
        {
            using (var repository = new LibraryRepository())
            {
                var borrowedBooks = (await repository.GetAllBorrowed(ClientService.CurrentClient))?.Where(h => h.ReturnDate == null);
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
        public async void ViewHistoryBorrowBook()
        {
            using (var repository = new LibraryRepository())
            {
                var borrowedBooks = (await repository.GetAllBorrowed(ClientService.CurrentClient))?.Where(h => h.ReturnDate != null);
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
    }
}
