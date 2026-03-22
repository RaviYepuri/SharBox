using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataRooms.Contracts;
using DataRooms.Entity;
using DataRooms.WEBAPI.Helpers;
using LoggerModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog;

namespace DataRooms.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        public IConfiguration _configuration;
        public ISqlRepository<User> _userRepository;
        public ISqlRepository<UserRoleMapping> _userRoleMappingRepository;
        private ILoggerManager _logger;

        public AuthenticateController(
            IConfiguration configuration,
            ISqlRepository<User> userRepository,
            ISqlRepository<UserRoleMapping> userRoleMappingRepository,
            ILoggerManager logger)
        {
            _logger = logger;
            _configuration = configuration;
            _userRepository = userRepository;
            _userRoleMappingRepository = userRoleMappingRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]AuthenticateRequest model)
        {
            try
            {
                _logger.LogDebug("Enter into login");
                IEnumerable<User> userDetails = null;
                if(model.IsADAuth == "Y")
                    userDetails = await _userRepository.GetAsync(x => x.UserName == model.Username);
                else
                    userDetails = await _userRepository.GetAsync(x=>x.UserName == model.Username && x.Password == model.Password);
                if (userDetails == null)
                    return null;

                JWTAuth jWTAuth = new JWTAuth();
                AuthenticateResponse response = jWTAuth.GetResponse(userDetails.First(),_configuration);

                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect" });
                else
                    response.AssignedRoles = await _userRoleMappingRepository.GetAsync(x=>x.UserId == response.Id);

                return Ok(response);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Route("user/{emailid}")]
        [HttpGet]
        public async Task<User> GetUserByEmailId(string emailid)
        {
            try
            {
                return await _userRepository.GetSingleAsync(x=>x.EmailId.ToLower() == emailid.ToLower());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updatepassword")]
        [HttpPost]
        public async Task UpdatePasswordforUser(User user)
        {
            try
            {
                var userDetails = await _userRepository.GetSingleAsync(x=>x.Id == user.Id);
                userDetails.Password = user.Password;
                await _userRepository.UpdateAsync(userDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
