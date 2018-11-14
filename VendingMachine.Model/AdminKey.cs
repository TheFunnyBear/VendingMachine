using System.ComponentModel.DataAnnotations;

namespace VendingMachine.Model
{
    public sealed class AdminKey
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
    }
}
