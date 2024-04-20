

namespace Application.ViewModel.AdminSide;

public record AdminHomeViewModel : AdminBaseViewModel
{
 
    public CountsViewModel counts { get; set; }
    public List<ContactMessagesViewModel> contactMessages { get; set; }
    public AdminAboutUsViewModel aboutUs { get; set; }

}

public record CountsViewModel
{
    public int OrdersCount { get; set; }
    public int UserCount { get; set; }
    public int AdminCount { get; set; }
    public int GameCount { get; set; }

}

public record ContactMessagesViewModel
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime Time { get; set; }
}

public record AdminAboutUsViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public string AuthorUrl { get; set; }
}

