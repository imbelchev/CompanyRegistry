using CompanyRegistry.ModelFactory.DataModel;
using CompanyRegistry.ModelFactory.ViewModels.API;
using CompanyRegistry.ModelFactory.ViewModels.WEB;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CompanyRegistry.WebUI.Services
{
    public class CompanyListService : ICompanyListService
    {
        protected readonly IConfiguration configuration;

        string apiBaseUrl, apiKeyUsername, apiKeyPassword;

        public CompanyListService(IConfiguration configuration)
        {
            this.configuration = configuration;

            apiBaseUrl = configuration.GetValue<string>(key: "ApiConnection:BaseUrl");

            apiKeyUsername = configuration.GetValue<string>(key: "ApiConnection:Username");

            apiKeyPassword = configuration.GetValue<string>(key: "ApiConnection:Password");
        }

        public async Task<CompanyListViewModel> GetCompanies()
        {
            CompanyListViewModel companyListViewModel = new CompanyListViewModel();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Username", apiKeyUsername);

            client.DefaultRequestHeaders.Add("Password", apiKeyPassword);

            string response = await client.GetStringAsync($"{apiBaseUrl}/api/companies");

            CompanyViewModel companyViewModel = JsonConvert.DeserializeObject<CompanyViewModel>(response);

            companyListViewModel.Companies = companyViewModel.Companies;

            companyListViewModel.FilteredCompanies = companyViewModel.Companies;

            return companyListViewModel;
        }

        public async Task<List<CompanyItem>> Search(CompanyListViewModel model)
        {
            List<CompanyItem> companyItems = new List<CompanyItem>();

            if (!string.IsNullOrWhiteSpace(model.SearchTerm))
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Add("Username", apiKeyUsername);

                client.DefaultRequestHeaders.Add("Password", apiKeyPassword);

                var response = await client.GetStringAsync($"{apiBaseUrl}/api/company?searchTerm={model.SearchTerm}");

                CompanyViewModel companyViewModel = JsonConvert.DeserializeObject<CompanyViewModel>(response);

                companyItems = companyViewModel.Companies;
            }
            else
            {
                companyItems = model.Companies;
            }

            return companyItems;
        }
    }
}
