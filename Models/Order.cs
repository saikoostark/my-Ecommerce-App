using System.ComponentModel.DataAnnotations.Schema;

namespace my_Ecommerce_App.Models;

public class Order
{

    public int ID { get; set; }
    public DateTime OrderDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalPrice { get; set; }

    public int UserID { get; set; }
    public User? User { get; set; }


    public virtual ICollection<CartItem>? CartItems { get; set; }



}