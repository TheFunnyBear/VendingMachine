using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Model;
using VendingMachine.Repository;

namespace VendingMachine.Service
{
    public sealed class CashBoxService : ICashBoxService
    {
        private readonly ICashBoxRepository _cashBoxRepository;

        public CashBoxService(
            ICashBoxRepository cashBoxRepository)
        {
            _cashBoxRepository = cashBoxRepository;
        }

        public async Task<bool> Add(CoinSlot coinSlot)
        {
            return await _cashBoxRepository.Add(coinSlot);
        }

        public async Task<bool> Apply(int id)
        {
            return await _cashBoxRepository.Apply(id);
        }

        public async Task<int> Create()
        {
            return await _cashBoxRepository.Create();
        }

        public async Task<bool> Delete(CoinSlot coinSlot)
        {
            return await _cashBoxRepository.Delete(coinSlot);
        }

        public async Task<bool> Edit(int id)
        {
            return await _cashBoxRepository.Edit(id);
        }

        public async Task<string> Export()
        {
            return await _cashBoxRepository.Export();
        }

        public async Task<CoinSlot> Get(int id)
        {
            return await _cashBoxRepository.Get(id);
        }

        public async Task<List<CoinSlot>> GetList()
        {
            return await _cashBoxRepository.GetList();
        }

        public async Task<bool> Import(string cashBoxJson)
        {
            return await _cashBoxRepository.Import(cashBoxJson);
        }

        public async Task<bool> UpdateAmount(int id, decimal amount)
        {
            return await _cashBoxRepository.UpdateAmount(id, amount);
        }

        public async Task<bool> UpdateIsDisable(int id, bool isDisable)
        {
            return await _cashBoxRepository.UpdateIsDisable(id, isDisable);
        }

        public async Task<bool> UpdateIsEnable(int id, bool isEnable)
        {
            return await _cashBoxRepository.UpdateIsEnable(id, isEnable);
        }

        public async Task<bool> UpdateName(int id, string name)
        {
            return await _cashBoxRepository.UpdateName(id, name);
        }

        public async Task<bool> UpdatePictureId(int id, int pictureId)
        {
            return await _cashBoxRepository.UpdatePictureId(id, pictureId);
        }

        public async Task<bool> UpdateQuantity(int id, int quantity)
        {
            return await _cashBoxRepository.UpdateQuantity(id, quantity);
        }
    }
}
