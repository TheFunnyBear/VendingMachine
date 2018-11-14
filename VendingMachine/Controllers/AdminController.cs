using System.Threading.Tasks;
using System.Web.Mvc;
using VendingMachine.Model;
using VendingMachine.Service;

namespace VendingMachine.Controllers
{
    public sealed class AdminController : Controller
    {
        private readonly ILoginKeysService _loginKeysService;

        public AdminController(ILoginKeysService loginKeysService)
        {
            _loginKeysService = loginKeysService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            if (await _loginKeysService.IsLoginExpired())
            {
                return RedirectToAction("Index", "VendingMachine");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Login(AdminKey adminKey)
        {
            if (await _loginKeysService.IsAdminKeyValid(adminKey.Key))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "VendingMachine");
        }

    }
}
