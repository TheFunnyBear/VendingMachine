using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Model;
using VendingMachine.Repository;

namespace VendingMachine.Service
{
    public sealed class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(
            IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<bool> Add(StoreItem storeItem)
        {
            return await _storeRepository.Add(storeItem);
        }

        public async Task<bool> Apply(int id)
        {
            return await _storeRepository.Apply(id);
        }

        public async Task<int> Create()
        {
            return await _storeRepository.Create();
        }

        public async Task<bool> Delete(StoreItem storeItem)
        {
            return await _storeRepository.Delete(storeItem);
        }

        public async Task<bool> Edit(int id)
        {
            return await _storeRepository.Edit(id);
        }

        public async Task<string> Export()
        {
            return await _storeRepository.Export();
        }

        public async Task<StoreItem> Get(int id)
        {
            return await _storeRepository.Get(id);
        }

        public async Task<List<StoreItem>> GetList()
        {
            return await _storeRepository.GetList();
        }

        public async Task<bool> Import(string storeItemsJson)
        {
            return await _storeRepository.Import(storeItemsJson);
        }

        public async Task<bool> UpdateAmount(int id, decimal amount)
        {
            return await _storeRepository.UpdateAmount(id, amount);
        }

        public async Task<bool> UpdateName(int id, string name)
        {
            return await _storeRepository.UpdateName(id, name);
        }

        public async Task<bool> UpdatePictureId(int id, int pictureId)
        {
            return await _storeRepository.UpdatePictureId(id, pictureId);
        }

        public async Task<bool> UpdateQuantity(int id, int quantity)
        {
            return await _storeRepository.UpdateQuantity(id, quantity);
        }
    }
}
