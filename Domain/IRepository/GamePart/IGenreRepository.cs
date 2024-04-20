using Domain.entities.GamePart.Game;
using Domain.entities.GamePart.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository.GamePart
{
    public interface IGenreRepository
    {
        #region General 
        Task<List<Genre>> GetListOfGenres();
        Task<List<Genre>> GetGenresById(int Id);
        Task<List<Game>> GetGamesByGenres(List<Genre> genres);
        Task AddSelectedGenres(GemeSelectedGenre gemeSelectedGenre);

         Task<List<GemeSelectedGenre>> GameSelectedGenre(int id);


        void DeleteGameGenres(List<GemeSelectedGenre> gemeSelectedGenres);
        Task<Genre?> GetGenreById(int id);
        Task AddNewGenre(Genre genre);
        Task UpdateGenre(Genre genre);
        Task RemoveGenre(Genre genre);
        #endregion
    }
}
