using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataRooms.Contracts;
using DataRooms.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DataRooms.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageDataroomController : ControllerBase
    {
        
        public ISqlRepository<DataRoom> _dataroomRepository;

        public ManageDataroomController(ISqlRepository<DataRoom> dataroomRepository)
        {
            _dataroomRepository = dataroomRepository;
        }

        [Route("savedataroom")]
        [HttpPost]
        public async Task<int> SaveDataRoom([FromBody] DataRoom dataroom)
        {
            try
            {              
                await _dataroomRepository.CreateAsync(dataroom);
                return dataroom.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updatedataroom")]
        [HttpPut]
        public async Task UpdateDataRoom([FromBody] DataRoom dataroom)
        {
            try
            {
                await _dataroomRepository.UpdateAsync(dataroom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deletedataroom")]
        [HttpPost]
        public async Task DeleteDataRoom([FromBody] DataRoom dataroom)
        {
            try
            {
                await _dataroomRepository.DeleteAsync(dataroom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
