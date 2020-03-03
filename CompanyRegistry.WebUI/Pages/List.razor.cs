using CompanyRegistry.ModelFactory.ViewModels.WEB;
using CompanyRegistry.WebUI.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace CompanyRegistry.WebUI.Pages
{
    public class ListBase : ComponentBase
    {
        [Inject] private ICompanyListService CompanyListService { get; set; }

        protected CompanyListViewModel CompanyModel { get; set; } = new CompanyListViewModel();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                CompanyModel = await CompanyListService.GetCompanies();
            }
            catch (Exception ex)
            {
                CompanyModel.IsError = true;

                CompanyModel.ErrorMessage = ex.Message;
            }
        }

        protected async Task Search()
        {
            try
            {
                CompanyModel.FilteredCompanies = await CompanyListService.Search(CompanyModel);
            }
            catch (Exception ex)
            {
                CompanyModel.IsError = true;

                CompanyModel.ErrorMessage = ex.Message;
            }
        }
    }
}
