using Microsoft.EntityFrameworkCore;
using RemoteFinder.BLL.Exceptions;
using RemoteFinder.BLL.Extensions;
using RemoteFinder.BLL.Mappers;
using RemoteFinder.BLL.Services.AuthorizationService;
using RemoteFinder.BLL.Validators;
using RemoteFinder.DAL;
using RemoteFinder.Entities.Storage;
using RemoteFinder.Models;
using RemoteFinder.Models.Shared;

namespace RemoteFinder.BLL.Services.BookService;

public class BookService : IBookService
{
    private readonly MainContext _mainContext;
    private readonly IMapper<BookEntity, BookBase> _mapperBook;
    private readonly IAuthorizationService _authorizationService;

    public BookService(MainContext mainContext, 
        IMapper<BookEntity, BookBase> mapperBook,
        IAuthorizationService authorizationService)
    {
        _mainContext = mainContext;
        _mapperBook = mapperBook;
        _authorizationService = authorizationService;
    }
    
    public DataGridModel<BookBase> GetAll(RequestQueryModel query)
    {
        var currentUserId = _authorizationService.GetCurrentUserId();
        
        var skip = (query.PageNumber - 1) * query.ItemsPerPage;

        var itemsQuery = _mainContext.Book
            .Where(b => b.UserSocialId == currentUserId)
            .AsQueryable();
        
        var totalCount = itemsQuery.Count();
        
            var items = itemsQuery
            .Include(b => b.File)
            .Include(b => b.Category)
            .Skip(skip)
            .Take(query.ItemsPerPage)
            .Select(b => _mapperBook.Map(b))
            .ToList();

        return new DataGridModel<BookBase>
        {
            Total = totalCount,
            Items = items,
        };
    }

    public BookBase GetOne(int id)
    {
        var currentUserId = _authorizationService.GetCurrentUserId();

        var bookEntity = _mainContext.Book
            .Include(b => b.Category)
            .Include(b => b.File)
            .FirstOrDefault(c => c.Id == id && c.UserSocialId == currentUserId);

        if (bookEntity == null)
        {
            throw new NotFoundException("Book is not found");
        }

        return _mapperBook.Map(bookEntity);
    }

    public BookBase Save(BookBase book)
    {
        if (book == null)
        {
            throw new ValidationException("Book form cannot be empty");
        }
        
        var currentUserId = _authorizationService.GetCurrentUserId();

        var validator = new BookBaseValidator();
        var validationResult = validator.Validate(book);

        if (!validationResult.IsValid)
        {
            var validationErrors = validationResult.Errors.CreateErrorsList();

            throw new ValidationFormException(validationErrors);
        }
        
        var bookEntity = _mapperBook.Map(book);
        
        bookEntity.UserSocialId = currentUserId;
        bookEntity.FileId = book.File.Id;

        _mainContext.Book.Add(bookEntity);
        _mainContext.SaveChanges();

        return book;
    }

    public BookBase Edit(BookBase book, int id)
    {
        if (book == null)
        {
            throw new ValidationException("Book model cannot be empty");
        }

        var validator = new BookBaseEditValidator();
        var validationResult = validator.Validate(book);

        if (!validationResult.IsValid)
        {
            var validationErrors = validationResult.Errors.CreateErrorsList();

            throw new ValidationFormException(validationErrors);
        }

        var currentUserId = _authorizationService.GetCurrentUserId();

        var bookEntity = _mainContext.Book.FirstOrDefault(c => c.Id == id && c.UserSocialId == currentUserId);

        if (bookEntity == null)
        {
            throw new NotFoundException("Book is not found");
        }

        bookEntity.Id = book.Id;
        bookEntity.Name = book.Name;
        bookEntity.CategoryId = book.Category.Id;

        _mainContext.Book.Update(bookEntity);
        _mainContext.SaveChanges();

        return book;
    }

    public void Remove(int id)
    {
        var currentUserId = _authorizationService.GetCurrentUserId();

        var bookEntity = _mainContext.Book.FirstOrDefault(c => c.Id == id && c.UserSocialId == currentUserId);

        if (bookEntity == null)
        {
            throw new NotFoundException("Book is not found");
        }

        _mainContext.Book.Remove(bookEntity);
        _mainContext.SaveChanges();
    }
}