using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataRooms.Contracts;
using DataRooms.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataRooms.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageEmailConfigController : ControllerBase
    {
        private readonly ISqlRepository<EmailConfiguration> _emailRepository;
        public ManageEmailConfigController(ISqlRepository<EmailConfiguration> emailRepository)
        {
            _emailRepository = emailRepository;
        }

        [Route("saveemailconfig")]
        [System.Web.Http.HttpPost]
        public async Task SaveEmailConfig([FromBody]EmailConfiguration email)
        {
            try
            {
                await _emailRepository.CreateAsync(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updateemailconfig")]
        [System.Web.Http.HttpPost]
        public async Task UpdateEmailConfig([FromBody] EmailConfiguration email)
        {
            try
            {
                await _emailRepository.UpdateAsync(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
