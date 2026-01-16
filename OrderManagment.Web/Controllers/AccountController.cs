using Microsoft.AspNetCore.Mvc;
using OrderManagement.Web.Services;
using OrderManagement.Web.Models;


namespace OrderManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiClientService _api;

        public AccountController(ApiClientService api)
        {
            _api = api;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var token = await _api.LoginAsync(model);
            if (string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError("", "Credenciales inválidas");
                return View(model);
            }

            HttpContext.Session.SetString("JWT", token);
            return RedirectToAction("Index", "Orders");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }

}
