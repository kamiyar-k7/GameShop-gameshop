

namespace Application.ViewModel.AdminSide;

public record AdminPlatformsViewModel : AdminBaseViewModel
{
    public List<PlatfromsViewModel> ListOfPlats {  get; set; }    
        public PlatfromsViewModel Platform {  get; set; }   
}

public record PlatfromsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
