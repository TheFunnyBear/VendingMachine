using NLog;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Model;
using VendingMachine.Service;

namespace VendingMachine.BuisnesLogic
{
    public sealed class PurchaseManager : IPurchaseManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ICashBoxService _cashBoxService;
        private readonly ICashBoxManager _cashBoxManager;
        private readonly IStoreManager _storeManager;
        private readonly ITransactionsService _transactionsService;
        private Transaction _currentTrunsuction { get; set; } = new Transaction();

        public PurchaseManager(
            ICashBoxService cashBoxService,
            ICashBoxManager cashBoxManager,
            IStoreManager storeManager,
            ITransactionsService transactionsService
            )
        {
            Guard.Against.Null(() => cashBoxService);
            Guard.Against.Null(() => cashBoxManager);
            Guard.Against.Null(() => storeManager);
            Guard.Against.Null(() => transactionsService);

            _cashBoxService = cashBoxService;
            _cashBoxManager = cashBoxManager;
            _storeManager = storeManager;
            _transactionsService = transactionsService;
        }

        public async Task<bool> CanInsertCoin(Coin coin)
        {
            Guard.Against.Null(() => coin);

            return await _cashBoxManager.IsCoinEnable(coin);
        }

        public async Task InsertCoin(Coin coin)
        {
            Guard.Against.Null(() => coin);

            if (await CanInsertCoin(coin))
            {
                _currentTrunsuction.InsertedCoins.Add(coin);
            }
            else
            {
                Logger.Error($"Can't insert coin [{coin}].");
            }
        }

        public async Task<bool> CanBuyProduct(Drink product)
        {
            Guard.Against.Null(() => product);
            var isDrinktExis = await _storeManager.IsDrinkExis(product);
            var price = await _storeManager.GetPrice(product);
            var balance = _currentTrunsuction.GetBalance();

            return isDrinktExis && (balance >= price);
        }

        public async Task Buy(Drink product)
        {
            Guard.Against.Null(() => product);

            if (await CanBuyProduct(product))
            {
                await _storeManager.Buy(product);
                _currentTrunsuction.PurchasedProducts.Add(product);
            }
            else
            {
                Logger.Error($"Can't buy product [{product}].");
            }
        }

        public async Task<Drink> Buy(int id)
        {
            var product = await _storeManager.GetStoreItemWithId(id);

            if (await CanBuyProduct(product))
            {
                await _storeManager.Buy(product);
                _currentTrunsuction.PurchasedProducts.Add(product);

                await _cashBoxManager.AppendCoins(_currentTrunsuction.InsertedCoins);
                var changeCoins = await _cashBoxManager.GetChange(_currentTrunsuction.GetBalance());
                _currentTrunsuction.ChangeCoins.AddRange(changeCoins);
                await _transactionsService.Add(_currentTrunsuction);
                _currentTrunsuction = new Transaction();
                foreach (var changeCoin in changeCoins)
                {
                    await InsertCoin(changeCoin);
                }
            }
            else
            {
                Logger.Error($"Can't buy product [{product}].");
            }

            return product;
        }

        public bool CanGetChange(out Money sendToPhone)
        {
            throw new System.NotImplementedException();
        }

        public List<Coin> GetChange(string phoneNumber)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Coin>> GetChange()
        {
            if (_currentTrunsuction.InsertedCoins.Count == 0)
            {
                return new List<Coin>();
            }

            await _cashBoxManager.AppendCoins(_currentTrunsuction.InsertedCoins);
            var changeCoins = await _cashBoxManager.GetChange(_currentTrunsuction.GetBalance());
            _currentTrunsuction.ChangeCoins.AddRange(changeCoins);
            await _transactionsService.Add(_currentTrunsuction);
            _currentTrunsuction = new Transaction();
            return changeCoins;
        }

        public Money CalcCoins(IEnumerable<Coin> coins)
        {
            Guard.Against.Null(() => coins);

            var result = new Money();
            foreach (var coin in coins)
            {
                result += coin.Nominal;
            }
            return result;
        }

        public Money GetBalance()
        {
            return _currentTrunsuction.GetBalance();
        }
    }
}
