

namespace Application.ViewModel.AdminSide;

public record AdminGenresViewModel : AdminBaseViewModel
{
    public List<GenresViewModel> ListOfGenres { get; set; }
    public GenresViewModel genre { get; set; }

}

public record GenresViewModel
{
    public int Id { get; set; } 
    public string Name { get; set; }    
}
