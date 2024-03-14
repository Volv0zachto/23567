
using _666.Data;
using _666.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace _666.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
[HttpGet]
    public IActionResult Index()
    {
    
        return View(_db.Categories.ToList());
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Category viewModel)
    {
        
            var category = _db.Categories.FirstOrDefault(x => x.Title == viewModel.Title);
            if (category == null)
            {
                Console.WriteLine("PRIVET");
                category = new Category() { Title = viewModel.Title };
                _db.Categories.Add(category);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        

    }
[HttpPost]
    public IActionResult Delete(int id)
    {
            var category = _db.Categories.FirstOrDefault(x => x.Id == id);
            _db.Categories.Remove(category);
            _db.SaveChanges();
        
        
        return RedirectToAction("Index");    
    }

    public IActionResult Edit(int id)
    {
        var product = _db.Categories.FirstOrDefault(u => u.Id == id);
        var newCategory = new Category()
        {
            Title = product.Title,
            Id =id
        }; 
        return View(newCategory);    
    }
    [HttpPost]
    public IActionResult Edit(Category viewModel)
    {
        var oldcategory = _db.Categories.FirstOrDefault(x => x.Id == viewModel.Id);
        var category = _db.Categories.FirstOrDefault(x => x.Title == viewModel.Title);
        if (category == null)
        {


            oldcategory.Title = viewModel.Title;
            _db.SaveChanges();
        }
        else
        {
            ModelState.AddModelError("Title", "Категория уже существует");

            return View(viewModel);
        }

        return RedirectToAction("Index");

    }
}