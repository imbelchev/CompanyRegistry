using CompanyRegistry.API.Infrastructure;
using CompanyRegistry.API.Models;
using CompanyRegistry.ModelFactory.DataModel;
using CompanyRegistry.ModelFactory.ViewModels;
using CompanyRegistry.ModelFactory.ViewModels.API;
using CompanyRegistry.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CompanyRegistry.API.Controllers
{
    [ApiController]
    [Route("api")]
    [CompanyRegistryApiKeyValidator]
    public class CompanyController : ControllerBase
    {
        #region Constructors

        private readonly ICompanyRepository companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        #endregion

        /// <summary>
        /// Gets a list of all companies.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("companies")]
        #region Swager Description Attributes
        [SwaggerResponse(HttpStatusCode.OK, "Get all companies", typeof(CompanyViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Unauthorized")]
        #endregion
        public async Task<ActionResult<CompanyViewModel>> GetAllCompanies()
        {
            try
            {
                CompanyViewModel model = await companyRepository.GetAllCompanies();

                return Ok(model);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get company by specified ID.
        /// </summary>
        /// <param name="id">Company ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("company/{id}")]
        #region Swager Description Attributes
        [SwaggerResponse(HttpStatusCode.OK, "Get company by ID", typeof(CompanyViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Unauthorized")]
        #endregion
        public async Task<ActionResult<CompanyViewModel>> GetCompany(string id)
        {
            try
            {
                CompanyViewModel model = await companyRepository.GetCompany(id);

                if (model.Companies.Count == 0)
                {
                    return NotFound();
                }

                return Ok(model);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Create / Update company
        /// </summary>
        /// <param name="companyItem">Company</param>
        /// <returns></returns>
        [HttpPost]
        [Route("company")]
        #region Swager Description Attributes
        [SwaggerResponse(HttpStatusCode.OK, "Update company", typeof(CompanyViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Unauthorized")]
        #endregion
        public async Task<ActionResult<CompanyItem>> UpdateCompany(CompanyItem companyItem)
        {
            try
            {
                CompanyItem model = await companyRepository.UpdateCompany(companyItem);

                return Ok(model);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Search company by name.
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <returns></returns>
        [HttpGet]
        [Route("company")]
        #region Swager Description Attributes
        [SwaggerResponse(HttpStatusCode.OK, "Search company by name", typeof(CompanyViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Unauthorized")]
        #endregion
        public async Task<ActionResult<CompanyViewModel>> SearchCompanies(string searchTerm)
        {
            try
            {
                CompanyViewModel model = await companyRepository.SearchCompanies(searchTerm);

                return Ok(model);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
