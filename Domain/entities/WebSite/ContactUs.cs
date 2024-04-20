namespace Domain.entities.WebSite;

public class ContactUs
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime DateTime { get; set; } = DateTime.UtcNow;

}
