using Microsoft.AspNetCore.Mvc;

namespace MVCUYGPROJE.Areas.Admin.Controllers;

public class HomeController : AdminBaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
