using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.Repository
{

    public sealed class CashBoxRepository : ICashBoxRepository
    {
        private VendingMachineContext _context = new VendingMachineContext();

        public async Task<bool> Add(CoinSlot coinSlot)
        {
            _context.CashBox.Add(coinSlot);
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<int> Create()
        {
            var coinSlot = new CoinSlot()
            {
                CoinSlotType = new Coin()
                {
                    Name = "0 Рублей",
                    Nominal = new Money(),
                    PictureId = 0
                },
                IsEnable = false,
                Quantity = 0
            };

            _context.CashBox.Add(coinSlot);
            int x = await _context.SaveChangesAsync();
            return coinSlot.Id;
        }

        public async Task<bool> Delete(CoinSlot coinSlot)
        {
            _context.CashBox.Remove(coinSlot);
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<CoinSlot> Get(int id)
        {
            return await _context.CashBox.SingleAsync(coinSlot => coinSlot.Id == id);
        }

        public async Task<List<CoinSlot>> GetList()
        {
            return await _context.CashBox.ToListAsync();
        }

        public async Task<bool> UpdateIsEnable(int id, bool isEnable)
        {
            var coinSlot = await Get(id);
            _context.Entry(coinSlot).State = EntityState.Modified;
            coinSlot.IsEnable = isEnable;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> UpdateIsDisable(int id, bool isDisable)
        {
            var coinSlot = await Get(id);
            _context.Entry(coinSlot).State = EntityState.Modified;
            coinSlot.IsDisable = isDisable;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> UpdateName(int id, string name)
        {
            var coinSlot = await Get(id);
            _context.Entry(coinSlot).State = EntityState.Modified;
            coinSlot.CoinSlotType.Name = name;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> UpdateAmount(int id, decimal amount)
        {
            var coinSlot = await Get(id);
            _context.Entry(coinSlot).State = EntityState.Modified;
            coinSlot.CoinSlotType.Nominal.Amount = amount;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> UpdatePictureId(int id, int pictureId)
        {
            var coinSlot = await Get(id);
            _context.Entry(coinSlot).State = EntityState.Modified;
            coinSlot.CoinSlotType.PictureId = pictureId;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> UpdateQuantity(int id, int quantity)
        {
            var coinSlot = await Get(id);
            _context.Entry(coinSlot).State = EntityState.Modified;
            coinSlot.Quantity = quantity;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> Edit(int id)
        {
            var coinSlot = await Get(id);
            _context.Entry(coinSlot).State = EntityState.Modified;
            coinSlot.IsEdited = true;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> Apply(int id)
        {
            var coinSlot = await Get(id);
            _context.Entry(coinSlot).State = EntityState.Modified;
            coinSlot.IsEdited = false;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<string> Export()
        {
            var coinSlots = await GetList();

            var result = JsonConvert.SerializeObject(coinSlots, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });

            return result;
        }

        public async Task<bool> Import(string cashBoxJson)
        {
            var importedSlots = JsonConvert.DeserializeObject<List<CoinSlot>>(cashBoxJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });

            bool result = true;
            foreach (var slot in importedSlots)
            {
                if(await Add(slot) == false)
                {
                    result = false;
                }
            }

            return result;
        }
    }

}
