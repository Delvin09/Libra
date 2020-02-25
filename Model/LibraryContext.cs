using Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LibraryContext : DbContext
    {
        public LibraryContext()
            : base("LibraryDatabase")
        {
            //Database.Log = logInfo => Console.WriteLine(logInfo);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(book => book.Title).IsRequired();
            modelBuilder.Entity<Book>().HasIndex(book => book.Title);
            modelBuilder.Entity<Book>().Property(book => book.Title).HasMaxLength(200);

            modelBuilder.Entity<Book>().HasIndex(book => book.Genre);

            modelBuilder.Entity<Author>().Property(a => a.FirstName).IsRequired(); //NOT NULL
            modelBuilder.Entity<Author>().Property(a => a.LastName).IsRequired();
            modelBuilder.Entity<Author>().HasIndex(a => a.FirstName);
            modelBuilder.Entity<Author>().HasIndex(a => a.LastName);
            modelBuilder.Entity<Author>().Property(a => a.FirstName).HasMaxLength(100);
            modelBuilder.Entity<Author>().Property(a => a.MiddleName).HasMaxLength(100);
            modelBuilder.Entity<Author>().Property(a => a.LastName).HasMaxLength(100);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
