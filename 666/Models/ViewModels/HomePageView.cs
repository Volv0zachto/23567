
namespace _666.Models;

public class HomePageView
{
    public string Title { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<Category> Categories { get; set; }
}