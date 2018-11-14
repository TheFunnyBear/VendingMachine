using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.Repository
{
    public interface IStoreRepository
    {
        Task<StoreItem> Get(int id);
        Task<int> Create();
        Task<bool> Add(StoreItem storeItem);
        Task<List<StoreItem>> GetList();
        Task<bool> Edit(int id);
        Task<bool> Apply(int id);
        Task<bool> Delete(StoreItem storeItem);
        Task<string> Export();
        Task<bool> Import(string storeItemsJson);
        Task<bool> UpdateAmount(int id, decimal amount);
        Task<bool> UpdateName(int id, string name);
        Task<bool> UpdatePictureId(int id, int pictureId);
        Task<bool> UpdateQuantity(int id, int quantity);
    }
}
