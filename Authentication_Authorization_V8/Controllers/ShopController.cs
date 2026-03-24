using Authentication_Authorization_V8.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_Authorization_V8.Controllers
{
    public class ShopController : Controller
    {
        private readonly IBookService service;

        public ShopController(IBookService service)
        {
            this.service = service;
        }

        public IActionResult Index(string search)
        {
            var books = service.GetBooks();

            if (!string.IsNullOrEmpty(search))
            {
                books = books
                    .Where(b => b.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(books);
        }

        public ActionResult Details(int id)
        {
            return View(service.GetBookById(id));
        }
    }
}
