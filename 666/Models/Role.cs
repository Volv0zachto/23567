using System.ComponentModel.DataAnnotations;

namespace _666.Models;

public class Role
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<User> Users { get; set; }
}