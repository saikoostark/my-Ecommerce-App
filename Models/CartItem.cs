using System.ComponentModel.DataAnnotations.Schema;

namespace my_Ecommerce_App.Models;

public class CartItem
{
    public int ID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    public int Amount { get; set; }

    public int ProductID { get; set; }
    public Product? Product { get; set; }

    public int UserID { get; set; }
    public User? User { get; set; }


    public int? OrderID { get; set; }
    public Order? Order { get; set; }


}