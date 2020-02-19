using System.Text;
using System.Threading.Tasks;
using Ui.Common;
using Ui.Common.Interfaces;

namespace Libra
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuBuilder.Default.DetectMenuOn<MainMenu>()
                .AddDefaultExit()
                .Build()
                .Process();
        }
    }
}
