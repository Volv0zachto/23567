using System.ComponentModel.DataAnnotations;

namespace _666.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    [Display(Name = "Имя")]
    public string Name { get; set; }
    [Display(Name = "Цена")]
    public decimal? Price { get; set; }
    [Display(Name = "Вес")]
    public decimal? Weight { get; set; }
    [Display(Name = "Категория")]
    public string Category { get; set; }
}