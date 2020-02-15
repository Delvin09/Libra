using System;

namespace Model
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public override string ToString()
        {
            return $"{AuthorId}\t{FirstName}\t{MiddleName}\t{LastName}";
        }
    }
}