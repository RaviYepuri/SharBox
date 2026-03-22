using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataRooms.Contracts;
using DataRooms.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DataRooms.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageDataRoomPermissionController : ControllerBase
    {
        
        public ISqlRepository<DataRoomPermission> _dataroomPermissionRepository;

        public ManageDataRoomPermissionController(ISqlRepository<DataRoomPermission> dataroomPermissionRepository)
        {
            _dataroomPermissionRepository = dataroomPermissionRepository;
        }

        [Route("savedataroompermission")]
        [HttpPost]
        public async Task<int> SaveDataRoomPermission([FromBody] DataRoomPermission dataroompermission)
        {
            try
            {
                await _dataroomPermissionRepository.CreateAsync(dataroompermission);
                return dataroompermission.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updatedataroompermission")]
        [HttpPut]
        public async Task UpdateDataRoomPermission([FromBody] DataRoomPermission dataroompermission)
        {
            try
            {
                await _dataroomPermissionRepository.UpdateAsync(dataroompermission);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deletedataroompermission")]
        [HttpPost]
        public async Task DeleteDataRoomPermission([FromBody] DataRoomPermission dataroompermission)
        {
            try
            {
                await _dataroomPermissionRepository.DeleteAsync(dataroompermission);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
