using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
    public sealed class Picture
    {
        [Key]
        public int Id { get; set; }
        public byte[] BinaryData { get; set; }
    }
}
