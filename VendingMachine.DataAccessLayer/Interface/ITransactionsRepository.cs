using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.Repository
{
    public interface ITransactionsRepository
    {
        Task<Transaction> Get(int id);
        Task<bool> Add(Transaction transaction);
        Task<List<Transaction>> GetList();
    }
}
