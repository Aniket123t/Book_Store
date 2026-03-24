using Authentication_Authorization_V8.Models;
using Authentication_Authorization_V8.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Authentication_Authorization_V8.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService service;

        public CartController(ICartService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(service.GetCartByUser(userId));
        }

        [HttpPost]
        
        public IActionResult Create(int BookId, int Quantity)
        {
            var userId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);

            Cart cart = new Cart()
            {
                BookId = BookId,
                Quantity = Quantity,
                UserId = userId,
                IsActive = 1
            };

            service.AddToCart(cart);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cart cart)
        {
            try
            {
                int result = service.UpdateCart(cart);

                if (result > 0)
                    return RedirectToAction("Index");
                else
                    return View(cart);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            service.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            try
            {
                int result = service.RemoveFromCart(id);

                if (result > 0)
                    return RedirectToAction("Index");
                else
                    return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }
    }
}
