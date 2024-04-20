

namespace Application.ViewModel.UserSide;

public class CheckOutViewModel
{
    public BillingViewModel Billing { get; set; }
    public List<OrderDetailsViewModel> OrderDetails { get; set; }
    public OrderDetailsViewModel oreder { get; set; }
}

public class BillingViewModel
{
    public string BillingId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long PosstCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string OrderNote { get; set; }

}
public class OrderDetailsViewModel
{
    public int CartDetailsId { get; set; }
    public int CartId { get; set; }
    public int GameId { get; set; }
    public string GameName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Platform { get; set; }
    public string ItemImage { get; set; }
    public string TrackingPostCode {  get; set; }  
}

