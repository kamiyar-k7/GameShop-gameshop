using Domain.entities.Cart;
using Domain.entities.GamePart.Genre;
using Domain.entities.GamePart.Platform;
using System.ComponentModel.DataAnnotations;


namespace Domain.entities.GamePart.Game;

public class Game
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string Company { get; set; }
    public decimal Price { get; set; }
    public int? Quantitiy { get; set; }
    public float Rating { get; set; }
    public string Trailer { get; set; }
    public string SystemRequirements { get; set; }
    public bool IsDelete { get; set; } = false;




    #region Rels
    public ICollection<Screenshot> Screenshots { get; set; }
    public ICollection<GemeSelectedGenre> gemeSelectedGenres { get; set; }
    public ICollection<GameSelectedPlatform> gameSelectedPlatforms { get; set; }
    public List<CartDeatails> CartDeatails { get; set; }
    public ICollection<Comments.Comments> Comments { get; set; }
    public GameStatus GamesStatus { get; set; }
    #endregion


}

public class Screenshot
{

    public int Id { get; set; }
    public string AvararUrl { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; }

}

