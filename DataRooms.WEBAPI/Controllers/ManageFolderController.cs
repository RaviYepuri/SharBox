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
    public class ManageFolderController : ControllerBase
    {
        
        public ISqlRepository<Folder> _folderRepository;

        public ManageFolderController(ISqlRepository<Folder> folderRepository)
        {
            _folderRepository = folderRepository;
        }

        [Route("getfolderhierarchy/{folderid}")]
        [System.Web.Http.HttpGet]
        public async Task<IEnumerable<Folder>> GetFolderHierarchy(int folderid)
        {
            try
            {
                string query = string.Format(@"Declare @FolderId int;
set @FolderId={0};
With FolderCTE
AS
(
Select Id,FolderName,ParentFolderId,ParentFolderName,DataRoomId,DataroomName from Folder Where Id = @FolderId

UNION ALL 

Select Folder.Id,Folder.FolderName,Folder.ParentFolderId,Folder.ParentFolderName,Folder.DataRoomId,Folder.DataRoomName from Folder Join FolderCTE on Folder.Id = FolderCTE.ParentFolderId
)

Select * From FolderCTE;",folderid);
                return await _folderRepository.DbGetDatawithQueryAsync(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("savefolder")]
        [System.Web.Http.HttpPost]
        public async Task<int> SaveFolder([System.Web.Http.FromBody] Folder folder)
        {
            try
            {
                await _folderRepository.CreateAsync(folder);
                return folder.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updatefolder")]
        [System.Web.Http.HttpPut]
        public async Task UpdateFolder([System.Web.Http.FromBody] Folder folder)
        {
            try
            {
                await _folderRepository.UpdateAsync(folder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deletefolder")]
        [System.Web.Http.HttpPost]
        public async Task DeleteFolder([System.Web.Http.FromBody] Folder folder)
        {
            try
            {
                await _folderRepository.DeleteAsync(folder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
