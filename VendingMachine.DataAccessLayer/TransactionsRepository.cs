using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.Repository
{
    public sealed class TransactionsRepository : ITransactionsRepository
    {
        private VendingMachineContext _context = new VendingMachineContext();

        public async Task<bool> Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<Transaction> Get(int id)
        {
            return await _context.Transactions.SingleAsync(transaction => transaction.Id == id);
        }

        public async Task<List<Transaction>> GetList()
        {
            return await _context.Transactions.ToListAsync();
        }
    }

}
