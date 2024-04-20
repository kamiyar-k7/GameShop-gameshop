

using Domain.entities.Cart;


namespace Application.ViewModel.AdminSide;

public record AdminOrdersViewModel : AdminBaseViewModel
{
    public OrdersViewModel OneOrder { get; set; }
    public List< OrdersViewModel> ListOfOrders { get; set; }
     public List<CartDeatailsViewModel> cartDeatails { get; set; }
   
    public AdminLocationViewModel Location { get; set; }
}

public record OrdersViewModel
{
  
    public int CartId { get; set; }
    public int UserId { get; set; }
    public decimal Price { get; set; }
    public int LocationId { get; set; }
    public bool IsFinally { get; set; }
    public OrderStatus Status { get; set; }
    public string TrackingCode { get; set; }


}

public record CartDeatailsViewModel
{
    public int CartId { get; set; }
    public int CartDetailsId { get; set; }
    public string ItemImage { get; set; }
    public int GameId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Platform { get; set; }
    public string GameName { get; set; }



}

public class AdminLocationViewModel
{

    

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long PosstCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string OrderNote { get; set; }



}
