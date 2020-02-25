using System.Threading.Tasks;

namespace Ui.Common.Interfaces
{
    public interface IMenuItem
    {
        int Num { get; }
        int Order { get; }
        string Title { get; }

        Task<bool> Process();
    }
}
