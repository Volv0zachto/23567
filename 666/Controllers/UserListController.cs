using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _666.Data;
using Microsoft.EntityFrameworkCore;

namespace _666.Controllers;

public class UserListController : Controller
{
    private readonly ApplicationDbContext _db;

    public UserListController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        var users = _db.Users.Include(u =>u.Role).ToList();
        return View(users);
    }
   
}