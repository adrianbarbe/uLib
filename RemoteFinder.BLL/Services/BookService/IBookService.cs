using RemoteFinder.Models;

namespace RemoteFinder.BLL.Services.BookService;

public interface IBookService
{
    List<BookBase> GetAll();
    
    BookBase GetOne(int id);

    BookBase Save(BookBase book);

    BookBase Edit(BookBase book, int id);

    void Remove(int id);
}