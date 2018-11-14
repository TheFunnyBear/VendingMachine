using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.Repository
{
    public sealed class StoreRepository : IStoreRepository
    {
        private VendingMachineContext _context = new VendingMachineContext();

        public async Task<bool> Add(StoreItem storeItem)
        {
            _context.StoreItems.Add(storeItem);
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> Apply(int id)
        {
            var storeItem = await Get(id);
            _context.Entry(storeItem).State = EntityState.Modified;
            storeItem.IsEdited = false;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<int> Create()
        {
            var storeItem = new StoreItem()
            {
                Drink = new Drink()
                {
                    Name = "Новый напиток",
                    Price = new Money(),
                    PictureId = 0
                },
                Quantity = 0
            };

            _context.StoreItems.Add(storeItem);
            int x = await _context.SaveChangesAsync();
            return storeItem.Id;
        }

        public async Task<bool> Delete(StoreItem storeItem)
        {
            _context.StoreItems.Remove(storeItem);
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> Edit(int id)
        {
            var storeItem = await Get(id);
            _context.Entry(storeItem).State = EntityState.Modified;
            storeItem.IsEdited = true;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<string> Export()
        {
            var storeItems = await GetList();

            var result = JsonConvert.SerializeObject(storeItems, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });

            return result;
        }

        public async Task<StoreItem> Get(int id)
        {
            return await _context.StoreItems.SingleAsync(storeItem => storeItem.Id == id);
        }

        public async Task<List<StoreItem>> GetList()
        {
            return await _context.StoreItems.ToListAsync();
        }

        public async Task<bool> Import(string storeItemsJson)
        {
            var importedStoreItems = JsonConvert.DeserializeObject<List<StoreItem>>(storeItemsJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });

            bool result = true;
            foreach (var storeItem in importedStoreItems)
            {
                if (await Add(storeItem) == false)
                {
                    result = false;
                }
            }

            return result;
        }

        public async Task<bool> UpdateAmount(int id, decimal amount)
        {
            var storeItem = await Get(id);
            _context.Entry(storeItem).State = EntityState.Modified;
            storeItem.Drink.Price.Amount = amount;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> UpdateName(int id, string name)
        {
            var storeItem = await Get(id);
            _context.Entry(storeItem).State = EntityState.Modified;
            storeItem.Drink.Name = name;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> UpdatePictureId(int id, int pictureId)
        {
            var storeItem = await Get(id);
            _context.Entry(storeItem).State = EntityState.Modified;
            storeItem.Drink.PictureId = pictureId;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> UpdateQuantity(int id, int quantity)
        {
            var storeItem = await Get(id);
            _context.Entry(storeItem).State = EntityState.Modified;
            storeItem.Quantity = quantity;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }
    }

}
