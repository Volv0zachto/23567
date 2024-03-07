
using _666.Data;
using _666.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _666.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _db;

    public ProductController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var ProductsList = _db.Products.ToList();
        return View(ProductsList);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]

    public IActionResult Add(Product product)
    {
        if (ModelState.IsValid)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(product);
    }

    public IActionResult Edit(int? id)
    {
        var product = _db.Products.FirstOrDefault(u => u.Id == id);
        return View(product);
    }
    [HttpPost("Delete")]
    public IActionResult Delete(int id)
    {
        var product = _db.Products.FirstOrDefault(u => u.Id == id);
        _db.Products.Remove(product);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    
    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(product);
    }

    public IActionResult Details(int? id)
    {
        var product = _db.Products.FirstOrDefault(u => u.Id == id);
        return View(product);
    }





    [HttpPost]
    public IActionResult Clear()
    {

        _db.Products.ExecuteDelete();

        return RedirectToAction("Index");
    }
}