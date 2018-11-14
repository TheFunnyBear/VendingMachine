using System.ComponentModel.DataAnnotations;

namespace VendingMachine.Model
{
    public sealed class StoreItem
    {
        [Key]
        public int Id { get; set; }
        public Drink Drink { get; set; }
        public int Quantity { get; set; }
        public bool IsEdited { get; set; }
    }
}
