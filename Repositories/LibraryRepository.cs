using Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace Model
{
    public class LibraryRepository : IDisposable
    {
        private readonly LibraryContext libraryContext;

        public LibraryRepository()
        {
            libraryContext = new LibraryContext();
        }

        public void Dispose()
        {
            SaveChanges();
            libraryContext?.Dispose();
        }

        public IQueryable<Book> FindByAuthor(Author author)
            => libraryContext.Books.Where(b => b.Author == author);

        public IQueryable<Book> FindByGenre(BookGenre genre)
            => libraryContext.Books.Where(b => b.Genre == genre);

        public IQueryable<Book> FindByYear(int year)
            => libraryContext.Books.Where(b => b.Year == year);

        public IQueryable<Book> FindBooksByAuthorName(string name)
            => libraryContext.Books.Where(b => b.Author.FirstName.Contains(name) || b.Author.LastName.Contains(name));

        public IQueryable<Author> FindAuthorByName(string firstName, string middleName, string lastName, DateTime birthdate)
            => libraryContext.Authors.Where(a => a.FirstName == firstName && a.MiddleName == middleName && a.LastName == lastName && a.Birthday == birthdate);

        public void Add(Book book) => libraryContext.Books.Add(book);

        public void Remove(Book book) => libraryContext.Books.Remove(book);

        public Book RemoveById(Guid id)
        {
            var book = libraryContext.Books.FirstOrDefault(b => b.BookId == id);
            libraryContext.Books.Remove(book);
            return book;
        }

        public IEnumerable<BorrowedHistory> GetAllBorrowed(Client currentClient)
        {
            var client = libraryContext.Clients.Single(c => c.Id == currentClient.Id);
            return client.History;
        }

        public Client FindClient(string name)
        {
            return libraryContext.Clients.FirstOrDefault(c => c.Name == name);
        }

        public Client CreateClient(string name)
            => libraryContext.Clients.Add(new Client() { Name = name });

        public void SaveChanges() => libraryContext.SaveChanges();
    }
}
