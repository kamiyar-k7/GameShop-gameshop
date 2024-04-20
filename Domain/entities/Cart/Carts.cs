using Domain.entities.UserPart.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.entities.Cart;

public class Carts
{
    [Key]
    public int CartId { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public decimal Price { get; set; }
    public int? LocationId { get; set; }
    public bool IsFinally { get; set; }
     public OrderStatus? Status { get; set; }
    public  DateTime? RegestredDate { get; set; } 
    public string? TrackingPostCode { get; set; }

    #region rels
    public  User User { get; set; }
    public List<CartDeatails> CartDeatails { get; set; }
    public Location? Location { get; set; }
    #endregion
}
