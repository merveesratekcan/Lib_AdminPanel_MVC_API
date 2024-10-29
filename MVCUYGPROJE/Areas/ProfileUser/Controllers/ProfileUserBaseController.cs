using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCUYGPROJE.Controllers;

namespace MVCUYGPROJE.Areas.ProfileUser.Controllers
{
    [Area("ProfileUser")]
    [Authorize(Roles = "ProfileUser")]
    public class ProfileUserBaseController : BaseController
    {
     
      
    }
}
