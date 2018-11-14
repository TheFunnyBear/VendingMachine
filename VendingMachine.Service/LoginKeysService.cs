using System.Threading.Tasks;
using VendingMachine.Model;
using VendingMachine.Repository;

namespace VendingMachine.Service
{
    public sealed class LoginKeysService : ILoginKeysService
    {
        private readonly ILoginKeysRepository _loginKeysRepository;

        public LoginKeysService(
            ILoginKeysRepository loginKeysRepository)
        {
            _loginKeysRepository = loginKeysRepository;
        }

        public async Task<AdminKey> GetAdminKey()
        {
            return await _loginKeysRepository.GetAdminKey();
        }

        public async Task<bool> IsAdminKeyValid(string adminKey)
        {
            return await _loginKeysRepository.IsAdminKeyValid(adminKey);
        }

        public async Task<bool> IsLoginExpired()
        {
            return await _loginKeysRepository.IsLoginExpired();
        }

        public async Task<bool> SetAdminKey(string adminKey)
        {
            return await _loginKeysRepository.SetAdminKey(adminKey);
        }
    }
}
