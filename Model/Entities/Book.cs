using DataStore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public virtual Author Author { get; set; }
        public int Year { get; set; }

        [ColumnNameCsv("New Genre")]
        public BookGenre Genre { get; set; }

        public Client Client { get; set; }
    }
}
