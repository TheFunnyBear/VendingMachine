using System.Collections.Generic;
using VendingMachine.Model;

namespace VendingMachine.BuisnesLogic
{
    public sealed class BasketSlot : IBasketSlot
    {
        public List<Drink> Basket { get; set; }
        public bool IsBasketNotEmpty { get { return Basket.Count > 0; } }

        public BasketSlot()
        {
            Basket = new List<Drink>();
        }

        public void Clear()
        {
            Basket.Clear();
        }

    }
}
