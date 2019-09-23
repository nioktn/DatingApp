using System.Threading.Tasks;

namespace DatingApp.API.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}