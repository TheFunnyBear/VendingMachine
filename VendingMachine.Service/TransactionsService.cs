using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Model;
using VendingMachine.Repository;

namespace VendingMachine.Service
{
    public sealed class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionsService(
            ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }

        public async Task<bool> Add(Transaction transaction)
        {
            return await _transactionsRepository.Add(transaction);
        }

        public async Task<Transaction> Get(int id)
        {
            return await _transactionsRepository.Get(id);
        }

        public async Task<List<Transaction>> GetList()
        {
            return await _transactionsRepository.GetList();
        }
    }
}
