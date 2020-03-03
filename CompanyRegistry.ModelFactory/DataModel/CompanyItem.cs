using CompanyRegistry.DataAccess.Entitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompanyRegistry.ModelFactory.DataModel
{
    public class CompanyItem
    {
        #region Constructors

        public CompanyItem() { }

        public CompanyItem(Company company)
        {
            Id = company.Id;

            Name = company.Name;

            VAT = company.Vat;
        }

        #endregion

        public string Id { get; set; }

        [StringLength(200, ErrorMessage = "'Company Name' should not be less than 3 characters and exceed 200 characters.", MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [RegularExpression(@"(^BG)([0-9]{9,10}$)", ErrorMessage = "'Company VAT Number' should start with 'BG' and be followed by a block of 9 or 10 digits.")]
        [StringLength(12, ErrorMessage = "'Company VAT number' should not be less than 11 exceed 12 characters.", MinimumLength = 11)]
        [Required]
        public string VAT { get; set; }
    }
}
