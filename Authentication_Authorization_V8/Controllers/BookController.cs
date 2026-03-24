using Authentication_Authorization_V8.Models;
using Authentication_Authorization_V8.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

public class BookController : Controller
{
    private readonly IBookService service;
    private Microsoft.AspNetCore.Hosting.IHostingEnvironment env;

    public BookController(IBookService service, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
    {
        this.service = service;
        this.env = env;
    }

    // GET: BookController
    public ActionResult Index()
    {
        return View(service.GetBooks());
    }

    // GET: BookController/Details/5
    public ActionResult Details(int id)
    {
        var model = service.GetBookById(id);
        return View(model);
    }

    // GET: BookController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: BookController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Book book, IFormFile file)
    {
        try
        {
            using (var fs = new FileStream(env.WebRootPath + "\\images\\" + file.FileName, FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fs);
            }

            book.ImageUrl = "~/images/" + file.FileName;

            int result = service.AddBook(book);

            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View();
        }
    }

    // GET: BookController/Edit/5
    public ActionResult Edit(int id)
    {
        var model = service.GetBookById(id);
        TempData["imageurl"] = model.ImageUrl;
        TempData.Keep("imageurl");
        return View(model);
    }

    // POST: BookController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Book book, IFormFile file)
    {
        try
        {
            TempData.Keep("imageurl");

            if (file != null)
            {
                using (var fs = new FileStream(env.WebRootPath + "\\images\\" + file.FileName, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fs);
                }

                book.ImageUrl = "~/images/" + file.FileName;

                string oldImage = TempData.Peek("imageurl").ToString();
                string[] strs = oldImage.Split("/");
                string path = env.WebRootPath + "\\images\\" + strs[2];
                System.IO.File.Delete(path);
            }
            else
            {
                book.ImageUrl = TempData.Peek("imageurl").ToString();
            }

            int result = service.UpdateBook(book);

            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View();
        }
    }

    // GET: BookController/Delete/5
    public IActionResult Delete(int id)
    {
        var model = service.GetBookById(id);
        return View(model);
    }

    // POST: BookController/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    public IActionResult DeleteConfirm(int id)
    {
        try
        {
            int result = service.DeleteBook(id);

            if (result > 0)
            {
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
            return View();
        }
    }
}