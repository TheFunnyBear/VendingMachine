using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using VendingMachine.Service;

namespace VendingMachine.Controllers
{
    public class ImageController : Controller
    {
        private readonly IPictureService _service;

        public ImageController(IPictureService pictureService)
        {
            _service = pictureService;
        }

        [HttpGet]
        public async Task<ActionResult> Show(int? id)
        {
            if (id != null)
            {
                var pictureItem = await _service.Get((int)id);
                var fileContent = pictureItem.BinaryData;
                var fileName = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.jpg";
                return File(fileContent, "image/jpg", fileName);
            }
            return null;
        }
    }
}