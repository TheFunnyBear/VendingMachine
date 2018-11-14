using System.Collections.Generic;
using VendingMachine.Model;

namespace VendingMachine.BuisnesLogic
{
    public sealed class ChangeSlot : IChangeSlot
    {
        public List<Coin> Change { get; set; }
        public bool IsChangeExist { get { return Change.Count > 0; } }

        public ChangeSlot()
        {
            Change = new List<Coin>();
        }

        public void Clear()
        {
            Change.Clear();
        }
    }
}
