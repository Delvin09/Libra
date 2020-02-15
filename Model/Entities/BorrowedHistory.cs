using System;

namespace Model.Entities
{
    public class BorrowedHistory
    {
        public int Id { get; set; }
        public virtual Book Book { get; set; }
        public Client Client { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
