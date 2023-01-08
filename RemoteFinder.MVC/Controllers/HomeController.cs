using System.Data;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RemoteFinder.BLL.Services.FileService;
using RemoteFinder.BLL.Services.UserSocialService;
using RemoteFinder.Models;
using RemoteFinder.MVC.Models;

namespace RemoteFinder.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IFileService _fileService;
    private readonly IUserSocialService _userSocialService;

    public HomeController(ILogger<HomeController> logger, IFileService fileService, IUserSocialService userSocialService)
    {
        _logger = logger;
        _fileService = fileService;
        _userSocialService = userSocialService;
    }

    public IActionResult Index()
    {
        var files = _fileService.GetAll();
        return View(files);
    }

    public IActionResult Details(int id)
    {
        var file = _fileService.Get(id);
        return View(file);
    }
    
    public IActionResult Create()
    {
        var users = _userSocialService.GetAll();

        List<SelectListItem> userItems = users.Select(u => new SelectListItem
        {
            Value = u.Id.ToString(),
            Text = $"{u.Username} - {u.FirstName} {u.LastName}"
        }).ToList();

        ViewBag.UserItems = userItems;
        
        return View();
    }
    
    [HttpPost, ActionName("Create")]
    public IActionResult Create([FromForm] FileStorage fileStorage)
    {
        if (ModelState.IsValid)
        {
            if (int.TryParse(fileStorage.UserSocial, out var userSocialId))
            {
                fileStorage.UserSocialId = userSocialId;
                
                _fileService.Create(fileStorage);
            }
            else
            {
                throw new Exception("Cannot parse the value");
            }

            return RedirectToAction("Index");
        }
        
        return View(fileStorage);
    }
    public IActionResult Edit(int id)
    {
        if (id == null)
        {
            throw new Exception("No id provided");
        }
        
        var file = _fileService.Get(id);

        return View(file);
    }
    
    [HttpPost, ActionName("Edit")]
    public IActionResult EditSave(int id, [FromForm] FileStorage fileStorage)
    {
        if (id == null)
        {
            throw new Exception("No id provided");
        }
        
        if (ModelState.IsValid)
        {
            _fileService.Update(id, fileStorage);
            
            return RedirectToAction("Index");
        }

        return View(fileStorage);
    }

    
    public IActionResult Delete(int id)
    {
        var file = _fileService.Get(id);
        return View(file);
    }
    
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirm(int id)
    {
        _fileService.Delete(id);
        
        return RedirectToAction("Index");

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}