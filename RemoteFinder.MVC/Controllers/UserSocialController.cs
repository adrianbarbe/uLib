using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RemoteFinder.BLL.Services.UserSocialService;
using RemoteFinder.Models;

namespace RemoteFinder.MVC.Controllers
{
    public class UserSocialController : Controller
    {
        private readonly IUserSocialService _userSocialService;

        public UserSocialController(IUserSocialService userSocialService)
        {
            _userSocialService = userSocialService;
        }
        
        public IActionResult Index()
        {
            var users = _userSocialService.GetAll();
            return View(users);
        }
        
        public IActionResult Create()
        {
            return View();
        }
    
        [HttpPost, ActionName("Create")]
        public IActionResult Create([FromForm] UserSocial user)
        {
            if (ModelState.IsValid)
            {
                _userSocialService.Create(user);
            
                return RedirectToAction("Index");
            }
        
            return View(user);
        }
    }
}