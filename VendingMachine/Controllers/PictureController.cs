using System.Threading.Tasks;
using System.Web.Mvc;
using VendingMachine.Model;
using VendingMachine.Service;

namespace VendingMachine.Controllers
{

    public sealed class PictureController : Controller
    {
        private IPictureService _service;

        public PictureController(IPictureService service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<JsonResult> Get(int id)
        {
            return Json(await _service.Get(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetList()
        {
            return Json(await _service.GetList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<bool> Post(Picture picture)
        {
            return await _service.Add(picture);
        }

        [HttpDelete]
        public async Task<bool> Delete(Picture picture)
        {
            return await _service.Delete(picture);
        }

        [HttpPost]
        public async Task<bool> Update(Picture picture, byte[] binaryData)
        {
            return await _service.Update(picture, binaryData);
        }
    }
}