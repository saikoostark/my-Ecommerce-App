using System.ComponentModel.DataAnnotations;

namespace my_Ecommerce_App.Models;

public class User
{
    [Key]
    public int ID { get; set; }

    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Email { get; set; }

    public string? Salt { get; set; }

    [Required]
    public string? HashedPassword { get; set; }

    [Required]
    public string? Phone { get; set; }

    [Required]
    public string? Address { get; set; }

    [Required]
    public string? Role { get; set; }

    public virtual ICollection<Order>? Orders { get; set; }

}