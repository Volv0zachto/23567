
using _666.Data;
using _666.Models;
using Microsoft.AspNetCore.Authorization;
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
    [HttpPost]
    public IActionResult Index(string? Title)
    {
        IQueryable<Product> productsQuery = _db.Products;

        if (Title!= null)
        {
            productsQuery = productsQuery.Where(p => p.Category.Title == Title);
        }
        else
        {
            productsQuery = _db.Products;
        }

        var HomePage = new HomePageView()
        {
            Categories = _db.Categories.ToList(),
            Products = productsQuery.ToList()
        };
        return View(HomePage);;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var HomePage = new HomePageView()
        {
            Categories = _db.Categories.ToList(),
            Products = _db.Products.ToList()
        };
        
        return View(HomePage);
    }
    [Authorize]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]

    public IActionResult Add(ProductViewModel product)
    {
        if (ModelState.IsValid)
        {
            var category = _db.Categories.FirstOrDefault(x => x.Title == product.Category);
            if (category == null)
            {
                category = new Category() { Title = product.Category };
                _db.Categories.Add(category);
                _db.SaveChanges();
            }

            var newProduct = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Weight = product.Weight,
                Category = category 
            };
            _db.Products.Add(newProduct);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(product);
    }
    [Authorize]
    public IActionResult Edit(int id)
    {
        var product = _db.Products.Include(x=>x.Category).FirstOrDefault(u => u.Id == id);
        var newProduct = new ProductViewModel()
        {
            Name = product.Name,
            Price = product.Price,
            Weight = product.Weight,
            Category = product.Category.Title ,
            Id = id
        }; 
        return View(newProduct);
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        
        var product = _db.Products.Include(u => u.Category).FirstOrDefault(x => x.Id == id);
        var title = product.Category.Title;
        _db.Products.Remove(product);
        _db.SaveChanges();
        var oldproduct = _db.Products.Include(u=>u.Category).FirstOrDefault(x => x.Category.Title == title);
        if (oldproduct == null)
        {
    
           var category = _db.Categories.FirstOrDefault(x => x.Title == product.Category.Title);
            _db.Categories.Remove(category);
            _db.SaveChanges();
        }
        
        
       
                
        
        return RedirectToAction("Index");
    }

    
    [HttpPost]
    public IActionResult Edit(ProductViewModel viewModel)
    {
        
        if (ModelState.IsValid)
        {
            var category = _db.Categories.FirstOrDefault(x => x.Title == viewModel.Category);
            var product = _db.Products.Include(u=>u.Category).FirstOrDefault(x => x.Id == viewModel.Id);

            if (category == null)
            {
                 var oldproduct = _db.Products.Include(u=>u.Category).FirstOrDefault(x => x.Category.Title == product.Category.Title);
                 if (oldproduct == null)
                 {

                     category = _db.Categories.FirstOrDefault(x => x.Title == product.Category.Title);
                     _db.Categories.Remove(category);
                     _db.SaveChanges();
                 }
                
            }
            product = _db.Products.FirstOrDefault(x => x.Id == viewModel.Id);

                product.Name = viewModel.Name;
                product.Price = viewModel.Price;
                product.Weight = viewModel.Weight;
                product.Category.Title =viewModel.Category ;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(viewModel);
    }
    [Authorize]
    public IActionResult Details(int? id)
    {
        var product = _db.Products.Include(x => x.Category).FirstOrDefault(u => u.Id == id);
        
        return View(product);
    }





    [HttpPost]
    public IActionResult Clear()
    {

        _db.Products.ExecuteDelete();

        return RedirectToAction("Index");
    }
}