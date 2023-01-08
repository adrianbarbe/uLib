using RemoteFinder.BLL.Exceptions;
using RemoteFinder.BLL.Extensions;
using RemoteFinder.BLL.Mappers;
using RemoteFinder.BLL.Services.AuthorizationService;
using RemoteFinder.BLL.Validators;
using RemoteFinder.DAL;
using RemoteFinder.Entities.Storage;
using RemoteFinder.Models;

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
    
    public List<BookBase> GetAll()
    {
        var currentUserId = _authorizationService.GetCurrentUserId();

        return _mainContext.Book
            .Where(b => b.UserSocialId == currentUserId)
            .Select(b => _mapperBook.Map(b))
            .ToList();
    }

    public BookBase GetOne(int id)
    {
        var currentUserId = _authorizationService.GetCurrentUserId();

        var bookEntity = _mainContext.Book.FirstOrDefault(c => c.Id == id && c.UserSocialId == currentUserId);

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
            throw new ValidationException("Book model cannot be empty");
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

        var validator = new BookBaseValidator();
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
        bookEntity.FileId = book.File.Id;

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
    }
}