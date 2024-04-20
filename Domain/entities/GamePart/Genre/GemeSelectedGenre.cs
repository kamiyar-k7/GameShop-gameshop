

namespace Domain.entities.GamePart.Genre;

public class GemeSelectedGenre
{
    public int Id { get; set; }

    public int GameId { get; set; }

    public int GenreId { get; set; }



    #region Rels
    public Game.Game Game { get; set; }
    public Genre Genre { get; set; }
    #endregion
}
