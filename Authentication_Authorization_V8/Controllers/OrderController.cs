using Authentication_Authorization_V8.Models;
using Authentication_Authorization_V8.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Authentication_Authorization_V8.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly ICartService cartService;
        private readonly IBookService bookService;
        private readonly IOrderDetailsService orderDetailsService;

        public OrderController(IOrderService orderService, ICartService cartService,IBookService bookService,IOrderDetailsService orderDetailsService)
        {
            this.orderService = orderService;
            this.cartService = cartService;
            this.bookService = bookService;
            this.orderDetailsService = orderDetailsService;
        }

        

        public ActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(orderService.GetOrdersByUser(userId));
        }

        [HttpPost]
        public IActionResult Create()
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var cartItems = cartService.GetCartByUser(userId);

                decimal total = 0;

                foreach (var item in cartItems)
                {
                    total += item.Quantity * item.Book.Price;
                }

                Order order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    TotalAmount = total
                };

                int result = orderService.PlaceOrder(order);

                // 🔥 ADDED START
                if (result > 0)
                {
                    foreach (var item in cartItems)
                    {
                        OrderDetails detail = new OrderDetails()
                        {
                            OrderId = order.OrderId,
                            BookId = item.BookId,
                            Quantity = item.Quantity,
                            Price = item.Book.Price,
                            IsActive = 1
                        };

                        orderDetailsService.AddOrderDetails(detail);
                    }
                }
                // 🔥 ADDED END

                if (result > 0)
                {
                    cartService.ClearCart(userId);
                    TempData["SuccessMessage"] = "Your order is successfully placed";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index", "Cart");
            }
        }
        [HttpPost]
        public IActionResult DirectOrder(int BookId, int Quantity)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var book = bookService.GetBookById(BookId);

                if (book == null)
                {
                    return RedirectToAction("Index", "Shop");
                }

                decimal total = book.Price * Quantity;

                Order order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    TotalAmount = total
                };

                orderService.PlaceOrder(order);
                TempData["SuccessMessage"] = "Your order is successfully placed";

                // 🔥 ADDED START
                OrderDetails detail = new OrderDetails()
                {
                    OrderId = order.OrderId,
                    BookId = BookId,
                    Quantity = Quantity,
                    Price = book.Price,
                    IsActive = 1
                };

                orderDetailsService.AddOrderDetails(detail);
                // 🔥 ADDED END


                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Shop");
            }
        }
        public IActionResult Details(int id)
        {
            var details = orderDetailsService.GetOrderDetails(id);
            return View(details);
        }
    }
}
