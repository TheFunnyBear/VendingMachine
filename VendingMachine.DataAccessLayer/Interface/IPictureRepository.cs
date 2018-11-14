using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.Repository
{
    public interface IPictureRepository
    {
        Task<Picture> Get(int id);
        Task<bool> Add(Picture picture);
        Task<List<Picture>> GetList();
        Task<bool> Update(Picture picture, byte[] binaryData);
        Task<bool> Delete(Picture picture);
    }
}
