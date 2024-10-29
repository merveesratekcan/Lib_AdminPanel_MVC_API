using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MVCUYGPROJE.Controllers
{
    public class BaseController : Controller
    {
        //Bu kodu unutma-kalıp olarak kullan
        protected INotyfService notyfService => HttpContext.RequestServices
            .GetService(typeof(INotyfService)) as INotyfService;

        protected void NotifySuccess(string messages)
        {
            notyfService.Success(messages);
            
        }
        protected void NotifyError(string messages)
        {
            notyfService.Error(messages);
        }

    }
}
