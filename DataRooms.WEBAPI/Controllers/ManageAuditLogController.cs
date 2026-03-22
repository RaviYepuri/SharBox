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
    public class ManageAuditLogController : ControllerBase
    {
        public ISqlRepository<AuditLog> _auditLogRepository;

        public ManageAuditLogController(ISqlRepository<AuditLog> auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        [Route("saveauditlog")]
        [HttpPost]
        public async Task<int> SaveAuditLog([FromBody] AuditLog auditLog)
        {
            try
            {
                await _auditLogRepository.CreateAsync(auditLog);
                return auditLog.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getauditlog/{fileid}")]
        [HttpGet]
        public async Task<IEnumerable<AuditLog>> GetAuditLog(int fileid)
        {
            try
            {
                return await _auditLogRepository.GetAsync(x=>x.FileId == fileid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
