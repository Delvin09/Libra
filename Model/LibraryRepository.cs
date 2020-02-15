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

        public IEnumerable<Book> FindByAuthor(Author author)
            => libraryContext.Books.Where(b => b.Author == author).ToArray();

        public IEnumerable<Book> FindByGenre(BookGenre genre)
            => libraryContext.Books.Where(b => b.Genre == genre).ToArray();

        public IEnumerable<Book> FindByYear(int year)
            => libraryContext.Books.Where(b => b.Year == year).ToArray();

        public IEnumerable<Book> FindBooksByAuthorName(string name)
            => libraryContext.Books.Where(b => b.Author.FirstName.Contains(name) || b.Author.LastName.Contains(name));

        public IEnumerable<Author> FindAuthorByName(string firstName, string middleName, string lastName, DateTime birthdate)
            => libraryContext.Authors.Where(a => a.FirstName == firstName && a.MiddleName == middleName && a.LastName == lastName && a.Birthday == birthdate);

        public void Add(Book book) => libraryContext.Books.Add(book);

        public void Remove(Book book) => libraryContext.Books.Remove(book);

        public Book RemoveById(Guid id)
        {
            var book = libraryContext.Books.FirstOrDefault(b => b.BookId == id);
            libraryContext.Books.Remove(book);
            return book;
        }

        public void SaveChanges() => libraryContext.SaveChanges();
    }
}
