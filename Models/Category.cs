namespace my_Ecommerce_App.Models;

public class Category
{

    public int ID { get; set; }
    public string? Name { get; set; }

    public virtual ICollection<ProductCategory>? ProductCategorys { get; set; }

}
