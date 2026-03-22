using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataRooms.Contracts;
using DataRooms.Entity;
using LoggerModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DataRooms.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageCompanyController : ControllerBase
    {
        public ISqlRepository<Company> _companyRepository;
        private ILoggerManager _logger;

        public ManageCompanyController(ISqlRepository<Company> companyRepository, ILoggerManager logger)
        {
            _companyRepository = companyRepository;
            _logger = logger;
        }

        [Route("savecompany")]
        [HttpPost]
        public async Task<int> SaveCompany([FromBody] Company company)
        {
            try
            {
                await _companyRepository.CreateAsync(company);
                return company.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updatecompany")]
        [HttpPut]
        public async Task UpdateCompany([FromBody] Company company)
        {
            try
            {
                _logger.LogDebug("Update Company...Start" + JsonConvert.SerializeObject(company));
                await _companyRepository.UpdateAsync(company);
                _logger.LogDebug("Update Company...End");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }
        }

        [Route("deletecompany")]
        [HttpPost]
        public async Task DeleteCompany([FromBody] Company company)
        {
            try
            {
                await _companyRepository.DeleteAsync(company);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
