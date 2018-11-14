using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.Repository
{
    public interface ILoginKeysRepository
    {
        Task<bool> SetAdminKey(string adminKey);
        Task<bool> IsAdminKeyValid(string adminKey);
        Task<bool> IsLoginExpired();
        Task<AdminKey> GetAdminKey();
    }
}
