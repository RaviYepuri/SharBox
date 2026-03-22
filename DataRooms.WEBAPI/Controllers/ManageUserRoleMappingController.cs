using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataRooms.Contracts;
using DataRooms.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataRooms.WEBAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ManageUserRoleMappingController : ControllerBase
    {
        public ISqlRepository<UserRoleMapping> _userRoleMappingRepository;

        public ManageUserRoleMappingController(ISqlRepository<UserRoleMapping> userRoleMappingRepository)
        {
            _userRoleMappingRepository = userRoleMappingRepository;
        }

        [Route("saveuserrole")]
        [HttpPost]
        public async Task AddUserRole([FromBody]List<UserRoleMapping> userroles)
        {
            try
            {                
                await _userRoleMappingRepository.CreateRangeAsync(userroles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updateuserrole")]
        [HttpPut]
        public async Task UpdateUserRole([FromBody] UserRoleMapping userrole)
        {
            try
            {
                await _userRoleMappingRepository.UpdateAsync(userrole);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deleteuserrole")]
        [HttpPost]
        public async Task DeleteUserRole([FromBody] UserRoleMapping userrole)
        {
            try
            {
                await _userRoleMappingRepository.DeleteAsync(userrole);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
