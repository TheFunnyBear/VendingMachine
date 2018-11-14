using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.Repository
{
    public sealed class PictureRepository : IPictureRepository
    {
        private VendingMachineContext _context = new VendingMachineContext();

        public async Task<Picture> Get(int id)
        {
            return await _context.Pictures.SingleAsync(picture => picture.Id == id);
        }

        public async Task<bool> Add(Picture picture)
        {
            _context.Pictures.Add(picture);
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<List<Picture>> GetList()
        {
            return await _context.Pictures.ToListAsync();
        }

        public async Task<bool> Update(Picture picture, byte[] binaryData)
        {
            _context.Entry(picture).State = EntityState.Modified;
            picture.BinaryData = binaryData;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> Delete(Picture picture)
        {
            _context.Pictures.Remove(picture);
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

    }

}
