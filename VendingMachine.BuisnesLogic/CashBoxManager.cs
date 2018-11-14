using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Model;
using VendingMachine.Service;

namespace VendingMachine.BuisnesLogic
{
    public sealed class CashBoxManager : ICashBoxManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ICashBoxService _cashBoxService;

        public CashBoxManager(ICashBoxService cashBoxService)
        {
            Guard.Against.Null(() => cashBoxService);

            _cashBoxService = cashBoxService;
        }

        public async Task AppendCoins(List<Coin> insertedCoins)
        {
            Guard.Against.Null(() => insertedCoins);
            Guard.Against.NullOrEmptyOrNullElements(() => insertedCoins);

            Logger.Info("AppendCoins invoked");
            var addedNominal = new Money();
            foreach (var insertedCoin in insertedCoins)
            {
                var coinSlots = await _cashBoxService.GetList();
                var slotForCoin = coinSlots.SingleOrDefault(slot => slot.CoinSlotType.Equals(insertedCoin));
                if (slotForCoin == null)
                {
                    throw new InvalidOperationException($"Can't find coin slot for [{insertedCoin}] in CashBox.");
                }
                slotForCoin.Quantity++;
                await _cashBoxService.UpdateQuantity(slotForCoin.Id, slotForCoin.Quantity);
                addedNominal += insertedCoin.Nominal;
                Logger.Info($"CashBox add coin [{insertedCoin}]");
            }
            Logger.Info($"AppendCoins completed. Added [{addedNominal}] to CashBox");
        }

        public async Task<List<Coin>> GetChange(Money change)
        {
            Guard.Against.Null(() => change);

            var changeCoins = new List<Coin>();
            Logger.Info($"GetChange invoked. Change is [{change}]");

            var coinSlots = await _cashBoxService.GetList();

            // Заблокированные монеты не выдаются
            // var nominalOrderedCoinSlots = coinSlots.Where(coinSlot => coinSlot.IsEnable)
            //     .OrderByDescending(coinSlot => coinSlot.CoinSlotType.Nominal.Amount);

            // Заблокированные монеты выдаются
             var nominalOrderedCoinSlots = coinSlots.OrderByDescending(coinSlot => coinSlot.CoinSlotType.Nominal.Amount);

            foreach (var coinSlot in nominalOrderedCoinSlots)
            {
                while (coinSlot.Quantity > 0 && coinSlot.CoinSlotType.Nominal <= change)
                {
                    Logger.Info($"GetChange Return to user [{coinSlot.CoinSlotType}].");
                    coinSlot.Quantity--;
                    await _cashBoxService.UpdateQuantity(coinSlot.Id, coinSlot.Quantity);
                    change -= coinSlot.CoinSlotType.Nominal;
                    changeCoins.Add(coinSlot.CoinSlotType);
                }

                if (change.IsZerro())
                {
                    break;
                }
            }

            if (!change.IsZerro())
            {
                Logger.Warn($"Can't get a change [{change}]");
                throw new InvalidOperationException($"Can't get a change [{change}]");
            }

            Logger.Info($"GetChange completed.");
            return changeCoins;
        }

        public async Task<bool> IsCoinEnable(Coin coin)
        {
            Guard.Against.Null(() => coin);
            var coinSlots = await _cashBoxService.GetList();
            var slotForCoin = coinSlots.SingleOrDefault(slot => slot.CoinSlotType.Equals(coin));
            return slotForCoin != null ? slotForCoin.IsEnable : false;
        }

    }
}
