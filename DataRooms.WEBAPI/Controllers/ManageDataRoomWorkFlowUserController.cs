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
    public class ManageDataRoomWorkFlowUserController : ControllerBase
    {
        public ISqlRepository<DataRoomWorkFlowUser> _repository;

        public ManageDataRoomWorkFlowUserController(ISqlRepository<DataRoomWorkFlowUser> repository)
        {
            _repository = repository;
        }

        [Route("saveuser")]
        [System.Web.Http.HttpPost]
        public async Task<int> SaveWorkFlowUser([System.Web.Http.FromBody] DataRoomWorkFlowUser user)
        {
            try
            {
                await _repository.CreateAsync(user);
                return user.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updateuser")]
        [System.Web.Http.HttpPut]
        public async Task UpdateWorkFlowUser([System.Web.Http.FromBody] DataRoomWorkFlowUser user)
        {
            try
            {
                await _repository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deleteuser")]
        [System.Web.Http.HttpPost]
        public async Task DeleteWorkFlowUser([System.Web.Http.FromBody] DataRoomWorkFlowUser user)
        {
            try
            {
                await _repository.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
