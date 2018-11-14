using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.Service
{
    public interface ICashBoxService
    {
        Task<CoinSlot> Get(int id);
        Task<int> Create();
        Task<bool> Add(CoinSlot coinSlot);
        Task<List<CoinSlot>> GetList();
        Task<bool> UpdateQuantity(int id, int quantity);
        Task<bool> UpdateIsEnable(int id, bool isEnable);
        Task<bool> UpdateIsDisable(int id, bool isDisable);
        Task<bool> UpdateName(int id, string name);
        Task<bool> UpdateAmount(int id, decimal amount);
        Task<bool> UpdatePictureId(int id, int pictureId);
        Task<bool> Delete(CoinSlot coinSlot);
        Task<bool> Edit(int id);
        Task<bool> Apply(int id);
        Task<string> Export();
        Task<bool> Import(string cashBoxJson);
    }
}
