using Authentication_Authorization_V8.Data;
using Authentication_Authorization_V8.Models;

namespace Authentication_Authorization_V8.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext db;

        public BookRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int AddBook(Book book)
        {
            book.IsActive = 1;
            db.Books.Add(book);
            return db.SaveChanges();
        }

        public int DeleteBook(int id)
        {
            int result = 0;
            var b = db.Books.Where(x => x.BookId == id).SingleOrDefault();

            if (b != null)
            {
                b.IsActive = 0;
                result = db.SaveChanges();
            }

            return result;
        }

        public Book GetBookById(int id)
        {
            return db.Books.Where(x => x.BookId == id).SingleOrDefault();
        }

        public List<Book> GetBooks()
        {
            var books = (from b in db.Books
                         where b.IsActive == 1
                         select b).ToList();

            return books;
        }

        public int UpdateBook(Book book)
        {
            int result = 0;

            var b = db.Books.Where(x => x.BookId == book.BookId).SingleOrDefault();

            if (b != null)
            {
                b.IsActive = 1;
                b.Title = book.Title;
                b.Author = book.Author;
                b.Price = book.Price;
                b.Description = book.Description;
                b.ImageUrl = book.ImageUrl;

                result = db.SaveChanges();
            }

            return result;
        }
    }
}
