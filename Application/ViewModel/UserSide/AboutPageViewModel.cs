
namespace Application.ViewModel.UserSide;

public class AboutPageViewModel
{
    public AboutUsViewModel? AboutUsViewModel { get; set; }
    public ContactUsViewModel ContactUsViewModel { get; set; }
}
public class AboutUsViewModel
{
    public string? Title { get; set; }
    public string? Description { get; set; } 
    public string? Author { get; set; }
    public string? AuthorUrl { get; set; }

    
}
public class ContactUsViewModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
}
