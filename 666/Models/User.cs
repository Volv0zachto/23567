using System.ComponentModel.DataAnnotations;

namespace _666.Models;

public class User
{
    [Key]
    
    public int Id { get; set; }

    [Required(ErrorMessage ="Введите Логин")]
    public string Login { get; set; }

    [Required(ErrorMessage ="Введите пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    public int Role { get; set; }
}