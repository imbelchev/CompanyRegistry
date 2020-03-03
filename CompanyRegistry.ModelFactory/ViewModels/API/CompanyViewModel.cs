using CompanyRegistry.ModelFactory.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyRegistry.ModelFactory.ViewModels.API
{
    public class CompanyViewModel
    {
        private List<CompanyItem> companies;

        public List<CompanyItem> Companies
        {
            get => companies == null ? companies = new List<CompanyItem>() : companies; 
            set => companies = value; 
        }
    }
}
