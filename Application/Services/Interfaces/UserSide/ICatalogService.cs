using Application.DTOs.UserSide.StorePart;
using Application.ViewModel.UserSide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.UserSide
{
    public interface ICatalogService
    {
        #region General
        Task<CatalogViewModel> GetCatalogAsync();
        Task<CatalogViewModel> SearchCatalog(CatalogViewModel searchViewModel);
        #endregion
    }
}
