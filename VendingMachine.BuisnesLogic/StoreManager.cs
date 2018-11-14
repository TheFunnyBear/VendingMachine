using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Model;
using VendingMachine.Service;

namespace VendingMachine.BuisnesLogic
{
    public sealed class StoreManager : IStoreManager
    {
        private readonly IStoreService _storeService;

        public StoreManager(IStoreService storeService)
        {
            Guard.Against.Null(() => storeService);

            _storeService = storeService;
        }

        public async Task Buy(Drink product)
        {
            Guard.Against.Null(() => product);

            if (await IsDrinkExis(product))
            {
                var storeItemForProduct = await GetStoreItemForProduct(product);
                storeItemForProduct.Quantity--;
                await _storeService.UpdateQuantity(storeItemForProduct.Id, storeItemForProduct.Quantity);
            }
        }

        public async Task<Money> GetPrice(Drink product)
        {
            Guard.Against.Null(() => product);

            var storeItemForProduct = await GetStoreItemForProduct(product);
            if (storeItemForProduct != null)
            {
                return storeItemForProduct.Drink.Price;
            }

            return null;
        }

        public async Task<Drink> GetStoreItemWithId(int id)
        {
            var storeItems = await _storeService.GetList();
            return storeItems.SingleOrDefault(storeItem => storeItem.Id == id).Drink;
        }

        public async Task<bool> IsDrinkExis(Drink product)
        {
            Guard.Against.Null(() => product);

            var storeItemForProduct = await GetStoreItemForProduct(product);
            if (storeItemForProduct != null)
            {
                return storeItemForProduct.Quantity > 0;
            }

            return false;
        }

        private async Task<StoreItem> GetStoreItemForProduct(Drink product)
        {
            Guard.Against.Null(() => product);
            var storeItems = await _storeService.GetList();
            return storeItems.SingleOrDefault(storeItem => storeItem.Drink.Equals(product));
        }
    }
}
