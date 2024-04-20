

namespace Application.ViewModel.AdminSide;


public record AdminsViewModel : AdminBaseViewModel
{
    
    public List<ListOfAdmins> Admins { get; set; }
    
}
public record ListOfAdmins
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string ScreenShot { get; set; }
    public DateTime Created { get; set; }
}
