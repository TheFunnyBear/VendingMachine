using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.BuisnesLogic
{
    public interface IStoreManager
    {
        Task<bool> IsDrinkExis(Drink product);
        Task<Money> GetPrice(Drink product);
        Task Buy(Drink product);
        Task<Drink> GetStoreItemWithId(int id);
    }
}
