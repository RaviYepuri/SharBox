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
    public class ManageFilePermissionController : ControllerBase
    {
        public ISqlRepository<FilePermission> _filePermissionRepository;

        public ManageFilePermissionController(ISqlRepository<FilePermission> filePermissionRepository)
        {
            _filePermissionRepository = filePermissionRepository;
        }

        [Route("savefilepermission")]
        [HttpPost]
        public async Task<int> SaveFilePermission([FromBody] FilePermission filepermission)
        {
            try
            {
                await _filePermissionRepository.CreateAsync(filepermission);
                return filepermission.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("savefilepermissions")]
        [HttpPost]
        public async Task SaveFilePermissions([FromBody] List<FilePermission> filepermissions)
        {
            try
            {
                await _filePermissionRepository.CreateRangeAsync(filepermissions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updatefilepermission")]
        [HttpPut]
        public async Task UpdateFilePermission([FromBody] FilePermission filepermission)
        {
            try
            {
                await _filePermissionRepository.UpdateAsync(filepermission);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deletefilepermission")]
        [HttpPost]
        public async Task DeleteFilePermission([FromBody] FilePermission filepermission)
        {
            try
            {
                await _filePermissionRepository.DeleteAsync(filepermission);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deleterangefilepermission")]
        [HttpPost]
        public async Task DeleteRangeFolderPermissions([FromBody] IEnumerable<FilePermission> filepermissions)
        {
            try
            {
                await _filePermissionRepository.DeleteRangeAsync(filepermissions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
