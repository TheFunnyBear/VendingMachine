using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Model;
using VendingMachine.Repository;

namespace VendingMachine.Service
{
    public sealed class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;

        public PictureService(
            IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        public async Task<bool> Add(Picture picture)
        {
            return await _pictureRepository.Add(picture);
        }

        public async Task<bool> Delete(Picture picture)
        {
            return await _pictureRepository.Delete(picture);
        }

        public async Task<Picture> Get(int id)
        {
            return await _pictureRepository.Get(id);
        }

        public async Task<List<Picture>> GetList()
        {
            return await _pictureRepository.GetList();
        }

        public async Task<bool> Update(Picture picture, byte[] binaryData)
        {
            return await _pictureRepository.Update(picture, binaryData);
        }
    }
}
