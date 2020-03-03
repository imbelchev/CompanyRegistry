using CompanyRegistry.ModelFactory.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyRegistry.ModelFactory.ViewModels.WEB
{
    public class CompanyListViewModel
    {
        public bool IsError { get; set; }

        public string ErrorMessage { get; set; }

        public string SuccessMEssage { get; set; }

        public string SearchTerm { get; set; }

        private List<CompanyItem> companies;

        public List<CompanyItem> Companies
        {
            get => companies == null ? companies = new List<CompanyItem>() : companies;
            set => companies = value;
        }

        private List<CompanyItem> filteredCompanies;

        public List<CompanyItem> FilteredCompanies
        {
            get => filteredCompanies == null ? filteredCompanies = new List<CompanyItem>() : filteredCompanies;
            set => filteredCompanies = value;
        }
    }
}
