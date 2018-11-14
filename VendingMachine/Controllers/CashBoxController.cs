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
    public sealed class CashBoxController : Controller
    {
        private readonly ICashBoxService _cashBoxService;
        private readonly IPictureService _pictureService;
        private readonly IPictureHelper _pictureHelper;
        private readonly ILoginKeysService _loginKeysService;

        public CashBoxController(
            ICashBoxService cashBoxService,
            IPictureService pictureService,
            IPictureHelper pictureHelper,
            ILoginKeysService loginKeysService)
        {
            _cashBoxService = cashBoxService;
            _pictureService = pictureService;
            _pictureHelper = pictureHelper;
            _loginKeysService = loginKeysService;
        }

        [HttpPost]
        public async Task<bool> ChangeAdminKey(string adminKey)
        {
            if (!string.IsNullOrEmpty(adminKey))
            {
                var canSetKey = await _loginKeysService.SetAdminKey(adminKey);
                var isAdminKeyValid = await _loginKeysService.IsAdminKeyValid(adminKey);
                return canSetKey && isAdminKeyValid;
            }

            return false;
        }

        [HttpGet]
        public async Task<JsonResult> GetAdminKey()
        {
            return Json(new { value = await _loginKeysService.GetAdminKey() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> IsAdminKeyValid(string adminKey)
        {
            return Json(new { value = await _loginKeysService.IsAdminKeyValid(adminKey) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> IsLoginExpired()
        {
            return Json(new { value = await _loginKeysService.IsLoginExpired() }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public async Task<JsonResult> Get(int id)
        {
            return Json( new { value = await _cashBoxService.Get(id) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetList()
        {
            return Json( new { value = await _cashBoxService.GetList() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<FileStreamResult> Export()
        {
            var exportedCashBox = await _cashBoxService.Export();
            var exportedStream = new MemoryStream(Encoding.Unicode.GetBytes(exportedCashBox));

            var fileStreamResult = new FileStreamResult(exportedStream, "application/json")
            {

                FileDownloadName = $"coins_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm")}.json"
            };
            return fileStreamResult;
        }

        [HttpPost]
        public async Task<bool> Import(HttpPostedFileBase uploadJsonFile)
        {
            if (uploadJsonFile != null)
            {
                var reader = new StreamReader(uploadJsonFile.InputStream, Encoding.Unicode);
                var cashBoxJson = reader.ReadToEnd();
                return await _cashBoxService.Import(cashBoxJson);
            }

            return false;
        }

        [HttpPost]
        public async Task<bool> Post(CoinSlot coinSlot)
        {
            return await _cashBoxService.Add(coinSlot);
        }

        [HttpPost]
        public async Task<int> Create()
        {
            return await _cashBoxService.Create();
        }

        [HttpPost]
        public async Task<bool> Delete(int id)
        {
            var coinSlot = await _cashBoxService.Get(id);
            return await _cashBoxService.Delete(coinSlot);
        }

        [HttpPost]
        public async Task<bool> Edit(int id)
        {
            return await _cashBoxService.Edit(id);
        }

        [HttpPost]
        public async Task<bool> Apply(int id, string name, decimal amount, int quantity, bool isDisable, HttpPostedFileBase uploadImageFile)
        {
            int pictureId = 0;
            if (uploadImageFile != null)
            {
                var coinPicture = _pictureHelper.GetPictureFromPostedFile(uploadImageFile, PictureType.Coin);
                await _pictureService.Add(coinPicture);
                pictureId = coinPicture.Id;
                await UpdatePictureId(id, pictureId);
            }

            await UpdateName(id, name);
            await UpdateAmount(id, amount);
            await UpdateQuantity(id, quantity);
            await UpdateIsDisable(id, isDisable);

            return await _cashBoxService.Apply(id);
        }

        [HttpPost]
        public async Task<bool> ApplyProps(int id, string name, decimal amount, int quantity, bool isDisable)
        {
            await UpdateName(id, name);
            await UpdateAmount(id, amount);
            await UpdateQuantity(id, quantity);
            await UpdateIsDisable(id, isDisable);

            return await _cashBoxService.Apply(id);
        }


        [HttpPost]
        public async Task<bool> UpdateAmount(int id, decimal amount)
        {
            return await _cashBoxService.UpdateAmount(id, amount);
        }

        [HttpPost]
        public async Task<bool> UpdateIsEnable(int id, bool isEnable)
        {
            return await _cashBoxService.UpdateIsEnable(id, isEnable);
        }

        [HttpPost]
        public async Task<bool> UpdateIsDisable(int id, bool isDisable)
        {
            return await _cashBoxService.UpdateIsDisable(id, isDisable);
        }

        [HttpPost]
        public async Task<bool> UpdateName(int id, string name)
        {
            return await _cashBoxService.UpdateName(id, name);
        }

        [HttpPost]
        public async Task<bool> UpdatePictureId(int id, int pictureId)
        {
            return await _cashBoxService.UpdatePictureId(id, pictureId);
        }

        [HttpPost]
        public async Task<bool> UpdateQuantity(int id, int quantity)
        {
            return await _cashBoxService.UpdateQuantity(id, quantity);
        }

    }
}