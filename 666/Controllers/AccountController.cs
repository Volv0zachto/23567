using System.Security.Claims;
using _666.Data;
using _666.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _666.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _db;

    public AccountController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(User model)
    {
        if (ModelState.IsValid)
        {
            var user = _db.Users.FirstOrDefault(x => x.Login == model.Login);
            if (user == null)
            {
                user = new User()
                {
                    Login = model.Login,
                    Password = model.Password,
                    Role = 1,
                };
                _db.Users.Add(user);
                _db.SaveChanges();
               var claim =  Authenticate(user);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claim));


                return RedirectToAction("Index", "Product");
            }
            else
            {
                ModelState.AddModelError("Login", "Этот логин уже занят");
                return View(model);
            }

            return View(model);
        }

        return View(model);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(User model)
    {
        if (ModelState.IsValid)
        {
            var user = _db.Users.FirstOrDefault(x => x.Login == model.Login);
            if (user != null)
            {
                if ((user.Password == model.Password))
                {
                    var claims = Authenticate(user);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claims));
                    return RedirectToAction("Index", "Product");

                }
               
            }
            else
            {
                ModelState.AddModelError("Login", "Неправильный логин или пароль");
                return View(model);
            }
        }

        return View(model);
    }
    [Authorize]
    public IActionResult LogOut()
    {

        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Product");
    }

    private ClaimsIdentity Authenticate(User account)
    {

        var claims = new List<Claim>()
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, account.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role.ToString())
        };
       
        return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}