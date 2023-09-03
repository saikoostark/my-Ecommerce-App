namespace my_Ecommerce_App.Models;

public class ProductCategory
{

    public int ID { get; set; }

    public int ProductID { get; set; }
    public Product? Product { get; set; }

    public int CategoryID { get; set; }
    public Category? Category { get; set; }
}