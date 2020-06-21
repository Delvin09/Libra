using DataStore;
using Model;
using System.IO;
using Ui.Common;
using System.Threading.Tasks;

namespace Libra
{
    public class ExportSubmenu
    {
        [MenuItem(1, "Export Books")]
        public async void ExportBooks()
        {
            using (var repository = new LibraryRepository())
            {
                var books = await repository.GetAllBooks();
                using (FileStream stream = new FileStream("books.csv", FileMode.Create, FileAccess.ReadWrite))
                    await CsvConvertor.Serialize(books, stream);
            }
        }

        [MenuItem(2, "Export Authors")]
        public void ExportAuthors()
        {

        }
    }
}