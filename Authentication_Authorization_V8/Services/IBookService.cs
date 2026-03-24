using Authentication_Authorization_V8.Models;

namespace Authentication_Authorization_V8.Services
{
    public interface IBookService
    {
        List<Book> GetBooks();

        Book GetBookById(int id);

        int AddBook(Book book);

        int UpdateBook(Book book);

        int DeleteBook(int id);
    }
}
