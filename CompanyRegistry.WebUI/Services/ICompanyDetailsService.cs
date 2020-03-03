using CompanyRegistry.ModelFactory.ViewModels.WEB;
using System.Threading.Tasks;

namespace CompanyRegistry.WebUI.Services
{
    public interface ICompanyDetailsService
    {
        Task<CompanyDetailsViewModel> GetCompany(string id);

        Task<CompanyDetailsViewModel> SaveCompany(CompanyDetailsViewModel model);
    }
}