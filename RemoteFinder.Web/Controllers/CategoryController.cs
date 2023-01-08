using Microsoft.AspNetCore.Mvc;
using RemoteFinder.BLL.Services.CategoryService;
using RemoteFinder.Models;

namespace RemoteFinder.Web.Controllers;

[Route("[controller]")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public List<Category> GetAll()
    {
        return _categoryService.GetAll();
    }
    
    [HttpGet]
    [Route("{id}")]
    public Category GetOne(int id)
    {
        return _categoryService.GetOne(id);
    }

    [HttpPost]
    public Category Save([FromBody] Category category)
    {
        return _categoryService.Save(category);
    }
    
    [HttpPut]
    [Route("{id}")]
    public Category Update([FromBody] Category category, int id)
    {
        return _categoryService.Edit(category, id);
    }

    [HttpPut]
    [Route("{id}")]
    public void Delete(int id)
    {
        _categoryService.Remove(id);
    }
}