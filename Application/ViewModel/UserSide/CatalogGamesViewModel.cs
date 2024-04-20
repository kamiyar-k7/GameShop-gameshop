

using Application.ViewModel.AdminSide;

namespace Application.ViewModel.UserSide;

public record CatalogViewModel 
{
   public  List<CatalogGamesViewModel?> Games { get; set; }
    public List<CatalogPlatformsViewModel?> Platforms { get; set; }
    public List<CatalogGenreViewModel?> Genre { get; set; }
    public CatalogSearchViewModel? search { get; set; }  

}

public record CatalogGamesViewModel
{
    public int GameId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string Company { get; set; }
    public decimal Price { get; set; }
    public int? Quantitiy { get; set; }
    public float Rating { get; set; }
    public string Trailer { get; set; }
    public string SystemRequirements { get; set; }
    public List<string> Screenshots { get; set; }





}
public record CatalogPlatformsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }


}
public record CatalogGenreViewModel
{
    public int Id { get; set; }
    public string GenreName { get; set; }

}
public record CatalogSearchViewModel
{
    // sarch 
    public string? SearchString { get; set; }
    public int? PlatfromId { get; set; }
    public int? GenreId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int PageId { get; set; }
}
