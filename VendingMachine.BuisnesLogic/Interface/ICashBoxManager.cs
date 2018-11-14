using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.BuisnesLogic
{
    public interface ICashBoxManager
    {
        Task<bool> IsCoinEnable(Coin coin);
        Task AppendCoins(List<Coin> insertedCoins);
        Task<List<Coin>> GetChange(Money change);
    }
}
