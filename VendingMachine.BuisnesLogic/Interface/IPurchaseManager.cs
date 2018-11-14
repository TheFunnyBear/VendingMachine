using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.BuisnesLogic
{
    public interface IPurchaseManager
    {
        Task InsertCoin(Coin coin);
        Task<bool> CanInsertCoin(Coin coin);

        Task<bool> CanBuyProduct(Drink product);
        Task Buy(Drink drink);
        Task<Drink> Buy(int id);

        bool CanGetChange(out Money sendToPhone);
        Task<List<Coin>> GetChange();
        List<Coin> GetChange(string phoneNumber);

        Money CalcCoins(IEnumerable<Coin> coins);
        Money GetBalance();
    }
}
