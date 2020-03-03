using CompanyRegistry.ModelFactory.DataModel;
using CompanyRegistry.ModelFactory.ViewModels.WEB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyRegistry.WebUI.Services
{
    public interface ICompanyListService
    {
        Task<CompanyListViewModel> GetCompanies();

        Task<List<CompanyItem>> Search(CompanyListViewModel model);
    }
}