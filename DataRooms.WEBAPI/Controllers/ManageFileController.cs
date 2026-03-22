using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataRooms.Contracts;
using DataRooms.Entity;
using DataRooms.WEBAPI.Helpers;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DataRooms.WEBAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ManageFileController : ControllerBase
    {
        public ISqlRepository<File> _fileRepository;
        private readonly IConfiguration _config;
        public ISqlRepository<FileVersion> _fileVersionRepository;
        private readonly IDataProtector _dataProtector;

        public ManageFileController(ISqlRepository<File> fileRepository,
            IConfiguration config,
            ISqlRepository<FileVersion> fileVersionRepository,
            IDataProtectionProvider dataProtectionProvider)
        {
            _config = config;
            _fileRepository = fileRepository;
            _fileVersionRepository = fileVersionRepository;
            _dataProtector = dataProtectionProvider.CreateProtector(_config["EncryptionKey"]);
        }

        [Route("getindividualfile/{fileid}")]
        [System.Web.Http.HttpGet]
        public async Task<File> GetIndividualFile(int fileid)
        {
            try
            {
                var file = await _fileRepository.GetSingleAsync(x => x.Id == fileid);
                //if (!string.IsNullOrEmpty(file.RelativePath))
                //    file.RelativePath = _dataProtector.Unprotect(file.RelativePath);
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        [Route("savefile")]
        [System.Web.Http.HttpPost]
        public async Task<int> SaveFile([System.Web.Http.FromBody] File file)
        {
            try
            {
                //if(!string.IsNullOrEmpty(file.RelativePath))
                //    file.RelativePath = _dataProtector.Protect(file.RelativePath);
                await _fileRepository.CreateAsync(file);
                return file.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 0;
        }

        [Route("savefileversion")]
        [System.Web.Http.HttpPost]
        public async Task<int> SaveFileVersion([System.Web.Http.FromBody] FileVersion file)
        {
            try
            {
                //if (!string.IsNullOrEmpty(file.RelativePath))
                //    file.RelativePath = _dataProtector.Protect(file.RelativePath);
                await _fileVersionRepository.CreateAsync(file);
                return file.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 0;
        }

        [Route("getfileversions/{fileid}")]
        [System.Web.Http.HttpGet]
        public async Task<IEnumerable<FileVersion>> GetFileVersions(int fileid)
        {
            try
            {
                IEnumerable<FileVersion> versions = await _fileVersionRepository.GetAsync(x => x.FileId == fileid);
                //if(versions!=null && versions.Count() > 0)
                //{
                //    foreach(var version in versions)
                //    {
                //        if (!string.IsNullOrEmpty(version.RelativePath))
                //            version.RelativePath = _dataProtector.Unprotect(version.RelativePath);
                //    }
                //}
                return versions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        [Route("getwaitingwith/{fileid}")]
        [System.Web.Http.HttpGet]
        public async Task<string> GetWaitingWith(int fileid)
        {
            try
            {
                string waitingwith = await _fileVersionRepository.GetWaitingWithofFile(fileid);
                return waitingwith;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("uploadfile")]
        [System.Web.Http.HttpPost]
        public async Task<int> UploadFile([System.Web.Http.FromBody] File file)
        {
            try
            {
                //if (!string.IsNullOrEmpty(file.RelativePath))
                //    file.RelativePath = _dataProtector.Protect(file.RelativePath);
                await _fileRepository.CreateAsync(file);
                return file.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updatefile")]
        [System.Web.Http.HttpPut]
        public async Task UpdateFile([System.Web.Http.FromBody] File file)
        {
            try
            {
                //if (!string.IsNullOrEmpty(file.RelativePath))
                //    file.RelativePath = _dataProtector.Unprotect(file.RelativePath);
                await _fileRepository.UpdateAsync(file);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deletefile")]
        [System.Web.Http.HttpPost]
        public async Task DeleteFile(File file)
        {
            try
            {
                //if (!string.IsNullOrEmpty(file.RelativePath))
                //    file.RelativePath = _dataProtector.Protect(file.RelativePath);
                await _fileRepository.DeleteAsync(file);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
