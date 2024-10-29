using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCUYGPROJE.Controllers;

namespace MVCUYGPROJE.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles= "Admin")]
    public class AdminBaseController : BaseController
    {

      
    }
}
