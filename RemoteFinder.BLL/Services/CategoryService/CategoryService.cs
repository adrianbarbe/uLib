using Microsoft.EntityFrameworkCore;
using RemoteFinder.BLL.Exceptions;
using RemoteFinder.BLL.Extensions;
using RemoteFinder.BLL.Mappers;
using RemoteFinder.BLL.Services.AuthorizationService;
using RemoteFinder.BLL.Validators;
using RemoteFinder.DAL;
using RemoteFinder.Entities.Storage;
using RemoteFinder.Models;
using Serilog;

namespace RemoteFinder.BLL.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly MainContext _mainContext;
    private readonly IMapper<CategoryEntity, Category> _mapperCategory;
    private readonly IAuthorizationService _authorizationService;

    public CategoryService(MainContext mainContext, 
        IMapper<CategoryEntity, Category> mapperCategory,
        IAuthorizationService authorizationService)
    {
        _mainContext = mainContext;
        _mapperCategory = mapperCategory;
        _authorizationService = authorizationService;
    }
    
    public List<Category> GetAll()
    {
        var currentUserId = _authorizationService.GetCurrentUserId();
        
        return _mainContext.Category
            .Where(c => c.UserSocialId == currentUserId)
            .Include(b => b.Books)
            .Select(c => _mapperCategory.Map(c))
            .ToList();
    }

    public Category GetOne(int id)
    {
        var currentUserId = _authorizationService.GetCurrentUserId();

        var categoryEntity = _mainContext.Category.FirstOrDefault(c => c.Id == id && c.UserSocialId == currentUserId);

        if (categoryEntity == null)
        {
            throw new NotFoundException("Category is not found");
        }

        return _mapperCategory.Map(categoryEntity);
    }

    public Category Save(Category category)
    {
        var currentUserId = _authorizationService.GetCurrentUserId();

        if (category == null)
        {
            // For tesing purpose
            Log.ForContext<CategoryService>().Warning("Category model cannot be empty, userId {CurrentUserId}", currentUserId);
        }

        var validator = new CategoryValidator();
        var validationResult = validator.Validate(category);

        if (!validationResult.IsValid)
        {
            var validationErrors = validationResult.Errors.CreateErrorsList();

            throw new ValidationFormException(validationErrors);
        }
        
        var categoryEntity = _mapperCategory.Map(category);

        categoryEntity.UserSocialId = currentUserId;

        _mainContext.Category.Add(categoryEntity);
        _mainContext.SaveChanges();

        return category;
    }

    public Category Edit(Category category, int id)
    {
        var currentUserId = _authorizationService.GetCurrentUserId();

        if (category == null)
        {
            Log.ForContext<CategoryService>().Warning("Category model cannot be empty, userId {CurrentUserId}", currentUserId);
            throw new ValidationException("Category model cannot be empty");
        }

        var validator = new CategoryValidator();
        var validationResult = validator.Validate(category);

        if (!validationResult.IsValid)
        {
            var validationErrors = validationResult.Errors.CreateErrorsList();

            throw new ValidationFormException(validationErrors);
        }
        
        var categoryEntity = _mainContext.Category.FirstOrDefault(c => c.Id == id && c.UserSocialId == currentUserId);

        if (categoryEntity == null)
        {
            throw new NotFoundException("Category is not found");
        }

        categoryEntity.Id = category.Id;
        categoryEntity.Name = category.Name;

        _mainContext.Category.Update(categoryEntity);
        _mainContext.SaveChanges();

        return category;
    }
    public void Remove(int id)
    {
        var currentUserId = _authorizationService.GetCurrentUserId();
        var categoryEntity = _mainContext.Category.FirstOrDefault(c => c.Id == id && c.UserSocialId == currentUserId);

        if (categoryEntity == null)
        {
            Log.ForContext<CategoryService>().Warning("Category is not found on remove, userId {CurrentUserId}", currentUserId);

            throw new NotFoundException("Category is not found");
        }

        _mainContext.Category.Remove(categoryEntity);
        _mainContext.SaveChanges();
    }
}