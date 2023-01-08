using RemoteFinder.BLL.Exceptions;
using RemoteFinder.BLL.Extensions;
using RemoteFinder.BLL.Mappers;
using RemoteFinder.BLL.Validators;
using RemoteFinder.DAL;
using RemoteFinder.Entities.Storage;
using RemoteFinder.Models;

namespace RemoteFinder.BLL.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly MainContext _mainContext;
    private readonly IMapper<CategoryEntity, Category> _mapperCategory;

    public CategoryService(MainContext mainContext, IMapper<CategoryEntity, Category> mapperCategory)
    {
        _mainContext = mainContext;
        _mapperCategory = mapperCategory;
    }
    
    public List<Category> GetAll()
    {
        return _mainContext.Category
            .Select(c => _mapperCategory.Map(c))
            .ToList();
    }

    public Category GetOne(int id)
    {
        var categoryEntity = _mainContext.Category.FirstOrDefault(c => c.Id == id);

        if (categoryEntity == null)
        {
            throw new NotFoundException("Category is not found");
        }

        return _mapperCategory.Map(categoryEntity);
    }

    public Category Save(Category category)
    {
        if (category == null)
        {
            throw new ValidationException("Category model cannot be empty");
        }

        var validator = new CategoryValidator();
        var validationResult = validator.Validate(category);

        if (!validationResult.IsValid)
        {
            var validationErrors = validationResult.Errors.CreateErrorsList();

            throw new ValidationFormException(validationErrors);
        }
        
        var categoryEntity = _mapperCategory.Map(category);

        _mainContext.Category.Add(categoryEntity);
        _mainContext.SaveChanges();

        return category;
    }

    public Category Edit(Category category, int id)
    {
        if (category == null)
        {
            throw new ValidationException("Category model cannot be empty");
        }

        var validator = new CategoryValidator();
        var validationResult = validator.Validate(category);

        if (!validationResult.IsValid)
        {
            var validationErrors = validationResult.Errors.CreateErrorsList();

            throw new ValidationFormException(validationErrors);
        }

        var categoryEntity = _mainContext.Category.FirstOrDefault(c => c.Id == id);

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
        var categoryEntity = _mainContext.Category.FirstOrDefault(c => c.Id == id);

        if (categoryEntity == null)
        {
            throw new NotFoundException("Category is not found");
        }

        _mainContext.Category.Remove(categoryEntity);
    }
}