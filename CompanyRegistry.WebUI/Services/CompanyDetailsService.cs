using CompanyRegistry.ModelFactory.DataModel;
using CompanyRegistry.ModelFactory.ViewModels.API;
using CompanyRegistry.ModelFactory.ViewModels.WEB;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompanyRegistry.WebUI.Services
{
    public class CompanyDetailsService : ICompanyDetailsService
    {
        protected readonly IConfiguration configuration;

        private readonly string apiBaseUrl, apiKeyUsername, apiKeyPassword;

        public CompanyDetailsService(IConfiguration configuration)
        {
            this.configuration = configuration;

            apiBaseUrl = configuration.GetValue<string>(key: "ApiConnection:BaseUrl");

            apiKeyUsername = configuration.GetValue<string>(key: "ApiConnection:Username");

            apiKeyPassword = configuration.GetValue<string>(key: "ApiConnection:Password");
        }

        public async Task<CompanyDetailsViewModel> GetCompany(string id)
        {
            CompanyDetailsViewModel companyDetailsViewModel = new CompanyDetailsViewModel();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Username", apiKeyUsername);

            client.DefaultRequestHeaders.Add("Password", apiKeyPassword);

            if (!string.IsNullOrWhiteSpace(id))
            {
                string response = await client.GetStringAsync($"{apiBaseUrl}/api/company/{id}");

                companyDetailsViewModel.Company = JsonConvert.DeserializeObject<CompanyViewModel>(response).Companies[0];
            }

            return companyDetailsViewModel;
        }

        public async Task<CompanyDetailsViewModel> SaveCompany(CompanyDetailsViewModel model)
        {
            Match vatRegex = Regex.Match(model.Company.VAT, @"(^BG)([0-9]{9,10}$)");

            if (!vatRegex.Success)
            {
                throw new Exception("'Company VAT Number' should start with 'BG' and be followed by a block of 9 or 10 digits.");
            }

            CompanyDetailsViewModel companyDetailsViewModel = new CompanyDetailsViewModel();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Username", apiKeyUsername);

            client.DefaultRequestHeaders.Add("Password", apiKeyPassword);

            string stringifiedModel = JsonConvert.SerializeObject(model.Company);

            StringContent stringContent = new StringContent(stringifiedModel, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{apiBaseUrl}/api/company", stringContent);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                companyDetailsViewModel.Company = JsonConvert.DeserializeObject<CompanyItem>(result);

                companyDetailsViewModel.SuccessMEssage = "Company saved successfully.";
            }
            else
            {
                companyDetailsViewModel.IsError = true;

                companyDetailsViewModel.ErrorMessage = "Something went worng. Please try again.";
            }

            return companyDetailsViewModel;
        }
    }
}
