using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCUYGPROJE.Models;

namespace MVCUYGPROJE.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                Console.WriteLine("Email veya Sifre Hatali!!!");
                return View(model);
            }
            var checkpass = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            //ispersistent = false, LockoutOnFailure = falseispersistent
            //false, false => Kullanıcı 3 defa yanlış girdiğinde hesap kilitlenmez.
            //true, true => Kullanıcı 3 defa yanlış girdiğinde hesap kilitlenir.
            //true, false => Kullanıcı 3 defa yanlış girdiğinde hesap kilitlenir.
            //false, true => Kullanıcı 3 defa yanlış girdiğinde hesap kilitlenmez.
            if (!checkpass.Succeeded)
            {
               Console.WriteLine("Email veya Sifre Hatali!!!");
                return View(model);
            }
            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole==null)
            {
                Console.WriteLine("Email veya Sifre Hatali!!!");
                return View(model);
            }
            return RedirectToAction("Index", "Home", new { Area = userRole[0].ToString()});
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
