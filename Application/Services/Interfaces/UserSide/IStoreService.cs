using Application.DTOs.UserSide.StorePart;


namespace Application.Services.Interfaces.UserSide;

public interface IStoreService
{
    #region general
    Task<List<StoreDto>> ShowGames();
    #endregion
}
