using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.Service
{
    public interface ILoginKeysService
    {
        Task<bool> SetAdminKey(string adminKey);
        Task<bool> IsAdminKeyValid(string adminKey);
        Task<bool> IsLoginExpired();
        Task<AdminKey> GetAdminKey();
    }
}
