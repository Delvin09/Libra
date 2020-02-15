using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class BorrowedHistory
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public Client Client { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Book> BorrowedBooks { get; set; }
        public List<BorrowedHistory> History { get; set; }
    }
}
