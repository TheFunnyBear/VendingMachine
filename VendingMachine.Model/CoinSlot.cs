using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.Model
{
    public sealed class CoinSlot
    {
        [Key]
        public int Id { get; set; }
        public Coin CoinSlotType { get; set; }
        public int Quantity { get; set; }
        public bool IsEnable { get; set; }
        public bool IsEdited { get; set; }
        [NotMapped]
        public bool IsDisable { get { return !IsEnable; } set { IsEnable = !value; } }
    }
}
