using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace my_Ecommerce_App.Models;

public class Product
{
    public int ID { get; set; }
    public string? Name { get; set; }

    [MaxLength(200)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    [Range(0, 100)]
    public int DiscountPercentage { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<Category>? Categorys { get; set; }


}
