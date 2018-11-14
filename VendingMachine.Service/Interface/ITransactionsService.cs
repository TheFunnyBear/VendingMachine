using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.Service
{
    public interface ITransactionsService
    {
        Task<Transaction> Get(int id);
        Task<bool> Add(Transaction transaction);
        Task<List<Transaction>> GetList();
    }
}
