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
    public class ManageFolderPermissionController : ControllerBase
    {
        public ISqlRepository<FolderPermission> _folderPermissionRepository;

        public ManageFolderPermissionController(ISqlRepository<FolderPermission> folderPermissionRepository)
        {
            _folderPermissionRepository = folderPermissionRepository;
        }

        [Route("savefolderpermission")]
        [HttpPost]
        public async Task<int> SaveFolderPermission([FromBody] FolderPermission folderpermission)
        {
            try
            {
                await _folderPermissionRepository.CreateAsync(folderpermission);
                return folderpermission.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("savefolderpermissions")]
        [HttpPost]
        public async Task SaveFolderPermissions([FromBody] List<FolderPermission> folderpermissions)
        {
            try
            {
                await _folderPermissionRepository.CreateRangeAsync(folderpermissions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updatefolderpermission")]
        [HttpPut]
        public async Task UpdateFolderPermission([FromBody] FolderPermission folderpermission)
        {
            try
            {
                await _folderPermissionRepository.UpdateAsync(folderpermission);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deletefolderpermission")]
        [HttpPost]
        public async Task DeleteFolderPermissiom([FromBody] FolderPermission folderpermission)
        {
            try
            {
                await _folderPermissionRepository.DeleteAsync(folderpermission);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deleterangefolderpermission")]
        [HttpPost]
        public async Task DeleteRangeFolderPermissions([FromBody] IEnumerable<FolderPermission> folderpermissions)
        {
            try
            {
                await _folderPermissionRepository.DeleteRangeAsync(folderpermissions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
