using System.Data.Entity;
using System.Linq;
using System.Text;
using VendingMachine.Model;

namespace VendingMachine.Repository
{
    public class VendingMachineContext : DbContext
    {
        public VendingMachineContext() : base("name=DefaultConnection")
        {

        }

        public IDbSet<Picture> Pictures { get; set; }
        public IDbSet<CoinSlot> CashBox { get; set; }
        public IDbSet<StoreItem> StoreItems { get; set; }
        public IDbSet<Transaction> Transactions { get; set; }
        public IDbSet<AdminKey> LoginKeys { get; set; }
        public IDbSet<AdminEvent> AdminEvents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
