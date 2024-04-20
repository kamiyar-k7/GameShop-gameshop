

namespace Application.ViewModel.AdminSide;


// change later 
public record UsersViewModel : AdminBaseViewModel
{
    public List<AllUsersViewModel> AllUsers { get; set; }
}

public record AllUsersViewModel
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string ScreenShot { get; set; }
    public DateTime Created { get; set; }

}
