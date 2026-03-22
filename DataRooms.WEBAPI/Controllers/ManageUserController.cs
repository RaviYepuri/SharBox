using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataRooms.Contracts;
using DataRooms.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DataRooms.WEBAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ManageUserController : ControllerBase
    {
        public ISqlRepository<User> _userRepository;
        private readonly IDataProtector _dataProtector;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ISqlRepository<ADInfo> _adinfoRepository;

        public ManageUserController(ISqlRepository<User> userRepository,
            IConfiguration config,
            ISqlRepository<ADInfo> adinfoRepository,
            IDataProtectionProvider dataProtectionProvider)
        {
            _config = config;
            _userRepository = userRepository;
            _adinfoRepository = adinfoRepository;
            _dataProtector = dataProtectionProvider.CreateProtector(_config["EncryptionKey"]);
        }
        
        

        [Route("saveuser")]
        [HttpPost]
        public async Task<int> SaveUser([FromBody]User user)
        {
            try
            {
                await _userRepository.CreateAsync(user);
                return user.Id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Route("updateuser")]
        [HttpPut]
        public async Task UpdateUser([FromBody]User user)
        {
            try
            {
                await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deleteuser")]
        [HttpPost]
        public async Task DeleteUser([FromBody] User user)
        {
            try
            {
                await _userRepository.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("saveadinfo")]
        [System.Web.Http.HttpPost]
        public async Task<int> SaveADInfo([System.Web.Http.FromBody] ADInfo model)
        {
            try
            {
                //model.IsADSync = _dataProtector.Protect(model.IsADSync);
                //model.IPAddress = _dataProtector.Protect(model.IPAddress);
                //model.DomainName = _dataProtector.Protect(model.DomainName);
                //model.CompanyName = _dataProtector.Protect(model.CompanyName);
                return await _adinfoRepository.CreateAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updateadinfo")]
        [System.Web.Http.HttpPost]
        public async Task<int> UpdateADInfo([System.Web.Http.FromBody] ADInfo model)
        {
            try
            {
                //if(!string.IsNullOrEmpty(model.IsADSync))
                //model.IsADSync = _dataProtector.Protect(model.IsADSync);
                //model.IPAddress = _dataProtector.Protect(model.IPAddress);
                //model.DomainName = _dataProtector.Protect(model.DomainName);
                //model.CompanyName = _dataProtector.Protect(model.CompanyName);
                return await _adinfoRepository.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
