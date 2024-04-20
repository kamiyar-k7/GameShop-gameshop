

namespace Application.ViewModel.AdminSide;

public record CommentsViewModel :AdminBaseViewModel
{
    public  List<CommentModel> Allcoments { get; set; }
    public CommentModel OneComment { get; set; }

}

public record CommentModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Comment { get; set; }
    public decimal Ratings { get; set; }
    public DateTime CreatedAt { get; set; } 
    public int UserId { get; set; }
    public string UserName { get; set; }    
        public string GameName { get; set; }
}
