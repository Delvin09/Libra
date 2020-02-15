using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<BorrowedHistory> History { get; set; }
    }
}
