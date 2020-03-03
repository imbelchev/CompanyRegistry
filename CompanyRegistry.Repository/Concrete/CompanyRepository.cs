using CompanyRegistry.DataAccess.Entitites;
using CompanyRegistry.ModelFactory.DataModel;
using CompanyRegistry.ModelFactory.ViewModels.API;
using CompanyRegistry.Repository.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyRegistry.Repository.Concrete
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(IMongoContext context) : base(context)
        {

        }

        public async Task<CompanyViewModel> GetAllCompanies()
        {
            CompanyViewModel companyViewModel = new CompanyViewModel();

            IEnumerable<Company> companies = await Get();

            foreach (Company company in companies)
            {
                CompanyItem companyItem = new CompanyItem(company);

                companyViewModel.Companies.Add(companyItem);
            }

            return companyViewModel;
        }

        public async Task<CompanyViewModel> GetCompany(string id)
        {
            CompanyViewModel companyViewModel = new CompanyViewModel();

            Company company = await Get(id);

            if (company != null)
            {
                CompanyItem companyItem = new CompanyItem(company);

                companyViewModel.Companies.Add(companyItem);
            }

            return companyViewModel;
        }

        public async Task<CompanyItem> UpdateCompany(CompanyItem companyItem)
        {
            Company newCompany;

            Company company = new Company();

            company.Id = companyItem.Id;

            company.Name = companyItem.Name;

            company.Vat = companyItem.VAT;

            if (string.IsNullOrWhiteSpace(companyItem.Id))
            {
                newCompany = await Insert(company);
            }
            else
            {
                newCompany = await Update(company);
            }

            return new CompanyItem(newCompany);
        }

        public async Task<CompanyViewModel> SearchCompanies(string searchTerm)
        {
            CompanyViewModel companyViewModel = new CompanyViewModel();
            
            IEnumerable<Company> companies = await Search(searchTerm);

            foreach (Company company in companies)
            {
                CompanyItem companyItem = new CompanyItem(company);

                companyViewModel.Companies.Add(companyItem);
            }

            return companyViewModel;
        }
    }
}
