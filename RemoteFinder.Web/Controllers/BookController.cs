using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemoteFinder.BLL.Services.BookService;
using RemoteFinder.Models;
using RemoteFinder.Models.Constants;
using RemoteFinder.Models.Shared;

namespace RemoteFinder.Web.Controllers;

[Route("[controller]")]
[Authorize(Policy = Policies.User)]
public class BookController : Controller
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public DataGridModel<BookBase> GetAll(RequestQueryModel query)
    {
        return _bookService.GetAll(query);
    }
    
    [HttpGet]
    [Route("{id}")]
    public BookBase GetOne(int id)
    {
        return _bookService.GetOne(id);
    }

    [HttpPost]
    public BookBase Save([FromBody] BookBase book)
    {
        return _bookService.Save(book);
    }
    
    [HttpPut]
    [Route("{id}")]
    public BookBase Update([FromBody] BookBase book, int id)
    {
        return _bookService.Edit(book, id);
    }

    [HttpDelete]
    [Route("{id}")]
    public void Delete(int id)
    {
        _bookService.Remove(id);
    }
}