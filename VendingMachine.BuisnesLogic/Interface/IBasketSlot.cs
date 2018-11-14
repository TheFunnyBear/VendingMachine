using System.Collections.Generic;
using VendingMachine.Model;

namespace VendingMachine.BuisnesLogic
{
    public interface IBasketSlot
    {
        List<Drink> Basket { get; set; }
        bool IsBasketNotEmpty { get; }

        void Clear();
    }
}
