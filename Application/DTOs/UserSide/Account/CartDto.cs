
namespace Application.DTOs.UserSide.Account;

public class CartDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int GameId { get; set; }
    public string GameName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Screenshot { get; set; }
    public string Platform { get; set; }
  
}
