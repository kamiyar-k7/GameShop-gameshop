using Domain.entities.GamePart.Game;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.entities.Cart;

public class CartDeatails
{
    [Key]
    public int CartDetailsId { get; set; }
    [ForeignKey("Cart")]
    public int CartId { get; set; }
    public int GameId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Platform { get; set; }


    #region Rels
    public Carts Cart { get; set; }
    public Game Game {  get; set; }
    
    #endregion
}
