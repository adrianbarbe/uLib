using RemoteFinder.Models;

namespace RemoteFinder.BLL.Services.CategoryService;

public interface ICategoryService
{
    List<Category> GetAll();

    Category GetOne(int id);
    
    Category Save(Category category);

    Category Edit(Category category, int id);

    void Remove(int id);
}