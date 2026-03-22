using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DataRooms.Contracts;
using DataRooms.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DataRooms.WEBAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        public ISqlRepository<ActivityLog> _activityLogRepository;
        public ISqlRepository<DataLog> _dataLogRepository;

        public LoggerController(
            ISqlRepository<ActivityLog> activityLogRepository,
            ISqlRepository<DataLog> dataLogRepository)
        {
            _activityLogRepository = activityLogRepository;
            _dataLogRepository = dataLogRepository;
        }

        [Route("activitylogs/{sql}")]
        [System.Web.Http.HttpGet]
        public async Task<IEnumerable<ActivityLog>> GetActivityLogs(string sql)
        {
            try
            {
                return await _activityLogRepository.DbGetDatawithQueryAsync(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("user/{id}")]
        [System.Web.Http.HttpGet]
        public async Task<ActivityLog> GetActivitybyId(int id)
        {
            try
            {
                return await _activityLogRepository.GetSingleAsync(x=>x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("saveactivity")]
        [System.Web.Http.HttpPost]
        public async Task<int> SaveUser([System.Web.Http.FromBody] ActivityLog log)
        {
            try
            {
                return await _activityLogRepository.CreateAsync(log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("savedatalog")]
        [System.Web.Http.HttpPost]
        public async Task SaveDataLog([System.Web.Http.FromBody] DataLog log)
        {
            try
            {
                await _dataLogRepository.CreateAsync(log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("datalogs/{activitylogid}")]
        [System.Web.Http.HttpGet]
        public async Task<IEnumerable<DataLog>> GetDataLogs(int activitylogid)
        {
            try
            {
                return await _dataLogRepository.GetAsync(x=>x.ActivityLogId == activitylogid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
