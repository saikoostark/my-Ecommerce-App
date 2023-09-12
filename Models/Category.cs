using System.ComponentModel.DataAnnotations;
namespace my_Ecommerce_App.Models;

public class Category
{

    public int ID { get; set; }

    [Required]
    [MaxLength(40)]
    public string? Name { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<Product>? Products { get; set; }

}
