

using Domain.entities.Cart;
using Microsoft.AspNetCore.Http;


namespace Application.ViewModel.UserSide;

public class UserAccountViewModel
{
    public UserDeatailViewModel Deatail { get; set; }
    public List<UserCommentsViewModel> Comments { get; set; }
    public List<UserOrdersViewModel> Orders { get; set; }
       

     
}

public record UserDeatailViewModel
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    public IFormFile? AvatarForm {  get; set; }
    public DateTime DateTime { get; set; }
}

public record UserOrdersViewModel
{
    public int CartId { get; set; }
    public int UserId { get; set; }
    public decimal Price { get; set; }
    public OrderStatus? Status { get; set; }
    public DateTime? RegestredDate { get; set; }
    public string? TrackingPostCode { get; set; }


}

public record UserCommentsViewModel
{
 
    public string Title { get; set; }
    public string Comment { get; set; }
    public decimal Ratings { get; set; }
    public DateTime CreatedAt { get; set; } 
    public int GameId { get; set; }
    public string GameName { get; set; }
}
