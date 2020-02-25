using Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class LibraryRepository : IDisposable
    {
        private readonly LibraryContext libraryContext;

        public LibraryRepository()
        {
            libraryContext = new LibraryContext();
        }

        public async void Dispose()
        {
            await SaveChanges();
            libraryContext?.Dispose();
        }

        public async Task<IEnumerable<Book>> FindByAuthor(Author author)
        {
            return await libraryContext.Books
                .Where(b => b.Author == author)
                .ToListAsync();
        }

        public async Task<IQueryable<Book>> FindByGenre(BookGenre genre)
        {
            return await Task.Run(() => libraryContext.Books.Where(b => b.Genre == genre));
        }

        public async Task<IQueryable<Book>> FindByYear(int year)
        {
            return await Task.Run(() => libraryContext.Books.Where(b => b.Year == year));
        }

        public async Task<IEnumerable<Book>> FindBooksByAuthorName(string name)
            => await libraryContext.Books
                .Where(b => b.Author.FirstName.Contains(name) || b.Author.LastName.Contains(name))
                .ToListAsync();

        public async Task<IEnumerable<Book>> GetAllBooks() => await libraryContext.Books.ToListAsync();

        public async Task<IEnumerable<Author>> FindAuthorByName(string firstName, string middleName, string lastName, DateTime birthdate)
            => await libraryContext.Authors
            .Where(a => a.FirstName == firstName && a.MiddleName == middleName && a.LastName == lastName && a.Birthday == birthdate)
            .ToListAsync();

        public void Add(Book book) => libraryContext.Books.Add(book);

        public void Remove(Book book) => libraryContext.Books.Remove(book);

        public async Task<Book> RemoveById(Guid id)
        {
            var book = await libraryContext.Books.FirstOrDefaultAsync(b => b.BookId == id);
            libraryContext.Books.Remove(book);
            return book;
        }

        public async Task<IEnumerable<BorrowedHistory>> GetAllBorrowed(Client currentClient)
        {
            var client = await libraryContext.Clients.SingleAsync(c => c.Id == currentClient.Id);
            return client.History;
        }

        public async Task<Client> FindClient(string name)
        {
            return await libraryContext.Clients.FirstOrDefaultAsync(c => c.Name == name);
        }

        public Client CreateClient(string name)
            => libraryContext.Clients.Add(new Client() { Name = name });

        public Task SaveChanges() => libraryContext.SaveChangesAsync();
    }
}
