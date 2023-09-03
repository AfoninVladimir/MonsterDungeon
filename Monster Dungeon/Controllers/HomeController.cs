using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Monster_Dungeon.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Monster_Dungeon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        private async Task Authenticate(string userName)  //установка куки после того как пользователь авторизовался 
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string login, string password)
        {
            var user = new Entity.User();
            user.Login = login;
            user.Password = password;
            bool access = BL.UserBL.Authorization(user);

            if (access)
            {
                await Authenticate(user.Login);
                return RedirectToAction("Index", "Game");
            }
            else
            {
                ModelState.AddModelError("", "Неверный логин или пароль");
            }
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(string login, string password) 
        {
            var user = new Entity.User();
            user.Login = login;
            user.Password = password;
            BL.UserBL.AddOrUpdate(user);

            return RedirectToAction("Index");
        }
		public IActionResult LogOut()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}