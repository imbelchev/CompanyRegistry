using CompanyRegistry.DataAccess.Entitites;
using CompanyRegistry.ModelFactory.DataModel;
using CompanyRegistry.ModelFactory.ViewModels;
using CompanyRegistry.ModelFactory.ViewModels.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyRegistry.Repository.Abstract
{
    public interface ICompanyRepository
    {
        Task<CompanyViewModel> GetAllCompanies();

        Task<CompanyViewModel> GetCompany(string id);

        Task<CompanyItem> UpdateCompany(CompanyItem model);

        Task<CompanyViewModel> SearchCompanies(string name);
    }
}
