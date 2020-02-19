using System.Text;
using System.Threading.Tasks;
using Ui.Common;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientService.Login();

            MenuBuilder.Default
                .DetectMenuOn<MainMenu>()
                .AddDefaultExit()
                .Build()
                .Process();
        }
    }
}
