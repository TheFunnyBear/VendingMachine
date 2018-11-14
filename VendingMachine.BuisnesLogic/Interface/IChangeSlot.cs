using System.Collections.Generic;
using VendingMachine.Model;

namespace VendingMachine.BuisnesLogic
{
    public interface IChangeSlot
    {
        List<Coin> Change { get; set; }
        bool IsChangeExist { get; }

        void Clear();
    }
}
