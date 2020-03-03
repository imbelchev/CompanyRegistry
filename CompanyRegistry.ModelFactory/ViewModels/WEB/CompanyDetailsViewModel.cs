using CompanyRegistry.ModelFactory.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyRegistry.ModelFactory.ViewModels.WEB
{
    public class CompanyDetailsViewModel
    {
        public bool IsError { get; set; }

        public string ErrorMessage { get; set; }

        public string SuccessMEssage { get; set; }

        private CompanyItem company;

        public CompanyItem Company
        {
            get => company == null ? company = new CompanyItem() : company;

            set => company = value; 
        }
    }
}
