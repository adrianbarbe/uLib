using RemoteFinder.Models;
using RemoteFinder.Models.Shared;

namespace RemoteFinder.BLL.Services.BookService;

public interface IBookService
{
    DataGridModel<BookBase> GetAll(RequestQueryModel query);
    
    BookBase GetOne(int id);

    BookBase Save(BookBase book);

    BookBase Edit(BookBase book, int id);

    void Remove(int id);
}