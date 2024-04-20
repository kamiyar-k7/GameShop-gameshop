
using Domain.entities.GamePart.Game;
using Domain.entities.UserPart.User;


namespace Domain.entities.Comments;

public class Comments
{

    #region Props
    public int Id { get; set; }
    public string Title { get; set; }
    public string Comment { get; set; }
    public decimal Ratings { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int UserId { get; set; } 
    public int GameId { get; set; }
    #endregion


    #region Rels

    public User User { get; set; }
    public Game Game { get; set; }
    #endregion
}
