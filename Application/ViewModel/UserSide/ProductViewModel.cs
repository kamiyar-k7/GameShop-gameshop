using Application.ViewModel.AdminSide;
using Domain.entities.GamePart.Game;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel.UserSide;



public record ProductViewModel  : AdminBaseViewModel
{
    public GameViewModelProduct Game { get; set; }
    public List<GameViewModelProduct> ListGames  { get; set; }
    public List<PlatformViewModelProduct> Platforms { get; set; }
    public List<GenreViewModelProduct> Genres { get; set; }
    public List<RelatedGamesProduct> RelatedGames { get; set; }
    public List<CommentsViewModelProduct> Comments { get; set; }
   
    public List<GenreViewModelProduct> AllGenres { get; set; }
    public List<PlatformViewModelProduct> AllPlatforms { get; set; }


    #region Post
    public int Quantity { get; set; }
    public int Platformid { get; set; }
    public string Comment {  get; set; }
    public string Title { get; set; }
    public decimal Rating { get; set; }
    #endregion

}



public record GameViewModelProduct
{
    public int Id { get; set; }
    [Required(ErrorMessage = "This Field is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "This Field is required")]
    public string Description { get; set; }
    [Required(ErrorMessage = "This Field is required")]
    public DateOnly ReleaseDate { get; set; }
    [Required(ErrorMessage = "This Field is required")]
    public string Company { get; set; }
    [Required(ErrorMessage = "This Field is required")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "This Field is required")]
    public float Rating { get; set; }
    
    public string? Trailer { get; set; }

    public IFormFile VideoFile { get; set; }
    [Required(ErrorMessage = "This Field is required")]
    public string SystemRequirements { get; set; }
    
    public List<string>? ScreenShots { get; set; }
 
    public List<IFormFile> FormFiles { get; set; }

   
    public GameStatus Status { get; set; }

    [Required(ErrorMessage = "This Field is required")]
    public int selectedStatus {  get; set; }

    [Required(ErrorMessage = "This Field is required")]
    public int? Quantity { get; set; }
}



public record PlatformViewModelProduct
{
    public int Id { get; set; }
    public string Name { get; set; }

}



public record GenreViewModelProduct
{
    public int Id { get; set; }
    public string GenreName { get; set; }

}
 
public record RelatedGamesProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public float Rating { get; set; }
    public List<string> ScreenShots { get; set; }
}
public record CommentsViewModelProduct
{
    public int Id { get; set; }
     public int UserId { get; set; }
    public string UserName { get; set; }
    public string UserAvatar { get; set; }
    public int GameId { get; set; }
    public string Title { get; set; }
    public string Comment { get; set; }
    public decimal Ratings { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
  

}
