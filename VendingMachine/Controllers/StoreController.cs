using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VendingMachine.Helpers;
using VendingMachine.Model;
using VendingMachine.Service;

namespace VendingMachine.Controllers
{

    public sealed class StoreController : Controller
    {
        private readonly IPictureService _pictureService;
        private readonly IPictureHelper _pictureHelper;
        private readonly IStoreService _storeService;

        public StoreController(
            IPictureService pictureService,
            IPictureHelper pictureHelper,
            IStoreService storeService)
        {
            _storeService = storeService;
            _pictureService = pictureService;
            _pictureHelper = pictureHelper;
        }

        [HttpPost]
        public async Task<bool> Post(StoreItem storeItem)
        {
            return await _storeService.Add(storeItem);
        }

        [HttpPost]
        public async Task<bool> Apply(int id, string name, decimal amount, int quantity, HttpPostedFileBase uploadImageFile)
        {
            int pictureId = 0;
            if (uploadImageFile != null)
            {
                var coinPicture = _pictureHelper.GetPictureFromPostedFile(uploadImageFile, PictureType.Product);
                await _pictureService.Add(coinPicture);
                pictureId = coinPicture.Id;
                await UpdatePictureId(id, pictureId);
            }

            await UpdateName(id, name);
            await UpdateAmount(id, amount);
            await UpdateQuantity(id, quantity);

            return await _storeService.Apply(id);
        }

        
        [HttpPost]
        public async Task<bool> ApplyProps(int id, string name, decimal amount, int quantity)
        {
            await UpdateName(id, name);
            await UpdateAmount(id, amount);
            await UpdateQuantity(id, quantity);

            return await _storeService.Apply(id);
        }

        [HttpPost]
        public async Task<int> Create()
        {
            return await _storeService.Create();
        }

        [HttpPost]
        public async Task<bool> Delete(int id)
        {
            var coinSlot = await _storeService.Get(id);
            return await _storeService.Delete(coinSlot);
        }

        [HttpPost]
        public async Task<bool> Edit(int id)
        {
            return await _storeService.Edit(id);
        }

        [HttpGet]
        public async Task<FileStreamResult> Export()
        {
            var exportedStoreItems = await _storeService.Export();
            var exportedStream = new MemoryStream(Encoding.Unicode.GetBytes(exportedStoreItems));

            var fileStreamResult = new FileStreamResult(exportedStream, "application/json")
            {

                FileDownloadName = $"drinks_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm")}.json"
            };
            return fileStreamResult;
        }

        [HttpGet]
        public async Task<JsonResult> Get(int id)
        {
            return Json(new { value = await _storeService.Get(id) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetList()
        {
            return Json(new { value = await _storeService.GetList() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<bool> Import(HttpPostedFileBase uploadJsonFile)
        {
            if (uploadJsonFile != null)
            {
                var reader = new StreamReader(uploadJsonFile.InputStream, Encoding.Unicode);
                var cashBoxJson = reader.ReadToEnd();
                return await _storeService.Import(cashBoxJson);
            }

            return false;
        }

        [HttpPost]
        public async Task<bool> UpdateAmount(int id, decimal amount)
        {
            return await _storeService.UpdateAmount(id, amount);
        }


        [HttpPost]
        public async Task<bool> UpdateName(int id, string name)
        {
            return await _storeService.UpdateName(id, name);
        }

        [HttpPost]
        public async Task<bool> UpdatePictureId(int id, int pictureId)
        {
            return await _storeService.UpdatePictureId(id, pictureId);
        }

        [HttpPost]
        public async Task<bool> UpdateQuantity(int id, int quantity)
        {
            return await _storeService.UpdateQuantity(id, quantity);
        }

    }
}