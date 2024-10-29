using Microsoft.AspNetCore.Mvc;

namespace MVCUYGPROJE.Areas.ProfileUser.Controllers
{
    public class HomeController : ProfileUserBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
