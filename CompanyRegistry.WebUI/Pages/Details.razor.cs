using CompanyRegistry.ModelFactory.ViewModels.WEB;
using CompanyRegistry.WebUI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CompanyRegistry.WebUI.Pages
{
    public class DetailsBase : ComponentBase
    {
        [Inject] private ICompanyDetailsService CompanyDetailsService { get; set; }

        [Parameter] public string Id { get; set; }

        protected CompanyDetailsViewModel CompanyDetailsModel { get; set; } = new CompanyDetailsViewModel();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                CompanyDetailsModel = await CompanyDetailsService.GetCompany(Id);
            }
            catch (Exception ex)
            {
                CompanyDetailsModel.IsError = true;

                CompanyDetailsModel.ErrorMessage = ex.Message;
            }
        }

        protected async Task HandleValidSubmit()
        {
            try
            {
                CompanyDetailsModel = await CompanyDetailsService.SaveCompany(CompanyDetailsModel);
            }
            catch (Exception ex)
            {
                CompanyDetailsModel.IsError = true;

                CompanyDetailsModel.ErrorMessage = ex.Message;
            }
        }
    }
}
