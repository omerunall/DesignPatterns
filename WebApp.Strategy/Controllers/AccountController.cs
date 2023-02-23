using BaseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BaseProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string eMail, string password)
        {
            var hasUser = await _userManager.FindByEmailAsync(eMail);

            if (hasUser == null) return View();

            var signınResult = await _signInManager.PasswordSignInAsync(hasUser, password, true, false);

            if (!signınResult.Succeeded) return View();

            return RedirectToAction(nameof(HomeController.Index), "Home");


        }
        public async Task<IActionResult> LogOut()
        {

            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
