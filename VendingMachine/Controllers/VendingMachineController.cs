using System.Threading.Tasks;
using System.Web.Mvc;
using VendingMachine.BuisnesLogic;
using VendingMachine.Model;
using VendingMachine.Service;

namespace VendingMachine.Controllers
{
    public sealed class VendingMachineController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly ICashBoxService _cashBoxService;
        private readonly IPurchaseManager _purchaseManager;
        private readonly IChangeSlot _changeSlot;
        private readonly IBasketSlot _basketSlot;

        public VendingMachineController(
             IStoreService storeService,
             ICashBoxService cashBoxService,
             IPurchaseManager purchaseManager,
             IChangeSlot changeSlot,
             IBasketSlot basketSlot)
        {
            _storeService = storeService;
            _cashBoxService = cashBoxService;
            _purchaseManager = purchaseManager;
            _changeSlot = changeSlot;
            _basketSlot = basketSlot;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetBalance()
        {
            var currentBalance = _purchaseManager.GetBalance();
            return Json(new { balance = currentBalance }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetStore()
        {
            var store = await _storeService.GetList();
            return Json(new { store = store }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetCoinSlots()
        {
            var cashBox = await _cashBoxService.GetList();
            return Json(new { cashBox = cashBox }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> TakeChange()
        {
            var change = await _purchaseManager.GetChange();
            _changeSlot.Change.AddRange(change);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult ClearChange()
        {
            _changeSlot.Clear();
            return Json(new { success = true });
        }

        public ActionResult GetChangeCoins()
        {
            return Json(new { change = _changeSlot }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> InsetCoin(Coin coin)
        {
            await _purchaseManager.InsertCoin(coin);
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<ActionResult> Buy(int id)
        {
            var drink =  await _purchaseManager.Buy(id);
            _basketSlot.Basket.Add(drink);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult ClearBasket()
        {
            _basketSlot.Clear();
            return Json(new { success = true });
        }

        public ActionResult GetBasket()
        {
            return Json(new { basket = _basketSlot }, JsonRequestBehavior.AllowGet);
        }

    }
}