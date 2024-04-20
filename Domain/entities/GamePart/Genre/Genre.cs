using System.Collections.ObjectModel;
namespace Domain.entities.GamePart.Genre;

public class Genre
{
    public int Id { get; set; }
    public string GenreName { get; set; }

    #region Rels

    public Collection<GemeSelectedGenre> gemeSelectedGenres { get; set; }

    #endregion




}
