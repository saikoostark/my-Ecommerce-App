using System.ComponentModel.DataAnnotations;
namespace my_Ecommerce_App.Models;

public class Category
{

    public int ID { get; set; }

    [Required]
    [MaxLength(20)]
    public string? Name { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<ProductCategory>? ProductCategorys { get; set; }

}
