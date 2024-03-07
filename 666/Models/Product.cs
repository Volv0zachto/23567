using System.ComponentModel.DataAnnotations;

namespace _666.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите имя")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Введите цену")]
    [Range(0.1, double.MaxValue, ErrorMessage = "Введите положительное число")]
    public decimal? Price { get; set; }

    [Required(ErrorMessage = "Введите вес")]
    [Range(0.1, double.MaxValue, ErrorMessage = "Введите положительное число")]
    public decimal? Weight { get; set; }
}