using Microsoft.AspNetCore.Mvc;
using OrderManagement.Web.Exceptions;
using OrderManagement.Web.Models;
using OrderManagement.Web.Services;

namespace OrderManagement.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApiClientService _api;

        public OrdersController(ApiClientService api)
        {
            _api = api;
        }

        private bool IsLogged()
            => !string.IsNullOrEmpty(HttpContext.Session.GetString("JWT"));

        public async Task<IActionResult> Index()
        {
            if (!IsLogged())
                return RedirectToAction("Login", "Account");

            var orders = await _api.GetOrdersAsync();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsLogged())
                return RedirectToAction("Login", "Account");

            var model = new CreateOrderViewModel();
            model.Details.Add(new CreateOrderDetailViewModel());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            if (!IsLogged())
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _api.CreateOrderAsync(model);
                TempData["Success"] = "Orden creada correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (BusinessException ex)
            {
                // Regla de negocio
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            if (!IsLogged())
                return RedirectToAction("Login", "Account");

            var order = await _api.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, OrderViewModel model)
        {
            if (!IsLogged())
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return View(model);

            await _api.UpdateOrderAsync(id, model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (!IsLogged())
                return RedirectToAction("Login", "Account");

            var order = await _api.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (!IsLogged())
                return RedirectToAction("Login", "Account");

            var order = await _api.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return View(order);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (!IsLogged())
                return RedirectToAction("Login", "Account");

            await _api.DeleteOrderAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
