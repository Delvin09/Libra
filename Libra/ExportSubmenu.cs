using DataStore;
using Model;
using System.IO;
using Ui.Common;

namespace Libra
{
    public class ExportSubmenu
    {
        [MenuItem(1, "Export Books")]
        public void ExportBooks()
        {
            using (var repository = new LibraryRepository())
            {
                var books = repository.GetAllBooks();
                using (FileStream stream = new FileStream("books.csv", FileMode.Create, FileAccess.ReadWrite))
                    CsvConvertor.Serialize(books, stream);
            }
        }

        [MenuItem(2, "Export Authors")]
        public void ExportAuthors()
        {

        }
    }
}