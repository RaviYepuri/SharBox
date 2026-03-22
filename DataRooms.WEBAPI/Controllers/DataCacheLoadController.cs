using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataRooms.Contracts;
using DataRooms.Entity;
using LoggerModule;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DataRooms.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataCacheLoadController : ControllerBase
    {
        public ISqlRepository<User> _userRepository;
        public ISqlRepository<RoleMaster> _roleRepository;
        public ISqlRepository<UserRoleMapping> _userRoleMappingRepository;
        public ISqlRepository<PermissionMaster> _permissionRepository;
        public ISqlRepository<DataRoomPermission> _dataroomPermissionRepository;
        public ISqlRepository<DataRoom> _dataroomRepository;
        public ISqlRepository<Folder> _folderRepository;
        public ISqlRepository<File> _fileRepository;
        public ISqlRepository<FolderPermission> _folderPermissionRepository;
        public ISqlRepository<FilePermission> _filePermissionRepository;
        public ISqlRepository<FileVersion> _fileVersionRepository;
        public ISqlRepository<Company> _companyRepository;
        public ISqlRepository<WorkFlowMaster> _workflowRepository;
        public ISqlRepository<DataRoomWorkFlowUser> _dataroomWorkFlowUserRepository;
        public ISqlRepository<ToDoTask> _todoTaskRepository;
        public ISqlRepository<Setting> _settingRepository;
        public ISqlRepository<EmailConfiguration> _emailRepository;
        public ISqlRepository<ADInfo> _adRepository;
        public ISqlRepository<ItemTrackerControl> _itemTrackerControlRepository;
        public ISqlRepository<ItemTrackerData> _itemTrackerData;
        public ISqlRepository<ItemTrackerMetaData> _itemTrackerMetaDataRepository;
        public ISqlRepository<ItemTrackerPermission> _itemTrackerPermissionRepository;
        private readonly IConfiguration _config;
        private readonly IDataProtector _dataProtector;
        private ILoggerManager _logger;

        public DataCacheLoadController(
            ISqlRepository<User> userRepository,
            ISqlRepository<RoleMaster> roleRepository,
            ISqlRepository<UserRoleMapping> userRoleMappingRepository,
            ISqlRepository<PermissionMaster> permissionRepository,
            ISqlRepository<DataRoomPermission> dataroomPermissionRepository,
            ISqlRepository<DataRoom> dataroomRepository,
            ISqlRepository<Folder> folderRepository,
            ISqlRepository<File> fileRepository,
            ISqlRepository<FolderPermission> folderPermissionRepository,
            ISqlRepository<FilePermission> filePermissionRepository,
            ISqlRepository<FileVersion> fileVersionRepository,
            ISqlRepository<Company> companyRepository,
            ISqlRepository<WorkFlowMaster> workflowRepository,
            ISqlRepository<DataRoomWorkFlowUser> dataroomWorkFlowUserRepository,
            ISqlRepository<ToDoTask> todoTaskRepository,
            ISqlRepository<Setting> settingRepository,
            ISqlRepository<EmailConfiguration> emailRepository,
            ISqlRepository<ADInfo> adRepository,
            ISqlRepository<ItemTrackerControl> itemTrackerControlRepository,
            ISqlRepository<ItemTrackerData> itemTrackerData,
            ISqlRepository<ItemTrackerMetaData> itemTrackerMetaDataRepository,
            ISqlRepository<ItemTrackerPermission> itemTrackerPermissionRepository,
            IConfiguration config,
            IDataProtectionProvider dataProtectionProvider,
            ILoggerManager logger)
        {
            _logger = logger;
            _config = config;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleMappingRepository = userRoleMappingRepository;
            _permissionRepository = permissionRepository;
            _dataroomPermissionRepository = dataroomPermissionRepository;
            _dataroomRepository = dataroomRepository;
            _folderRepository = folderRepository;
            _fileRepository = fileRepository;
            _folderPermissionRepository = folderPermissionRepository;
            _filePermissionRepository = filePermissionRepository;
            _fileVersionRepository = fileVersionRepository;
            _companyRepository = companyRepository;
            _workflowRepository = workflowRepository;
            _dataroomWorkFlowUserRepository = dataroomWorkFlowUserRepository;
            _todoTaskRepository = todoTaskRepository;
            _settingRepository = settingRepository;
            _emailRepository = emailRepository;
            _adRepository = adRepository;
            _itemTrackerControlRepository = itemTrackerControlRepository;
            _itemTrackerData = itemTrackerData;
            _itemTrackerMetaDataRepository = itemTrackerMetaDataRepository;
            _itemTrackerPermissionRepository = itemTrackerPermissionRepository;
            _dataProtector = dataProtectionProvider.CreateProtector(_config["EncryptionKey"]);
        }

        [Route("getcompanies")]
        [HttpGet]
        public async Task<IEnumerable<Company>> GetCompanies()
        {
            try
            {
                return await _companyRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getusers")]
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                return await _userRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getroles")]
        [HttpGet]
        public async Task<IEnumerable<RoleMaster>> GetRoles()
        {
            try
            {
                return await _roleRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getuserrolemappings")]
        [HttpGet]
        public async Task<IEnumerable<UserRoleMapping>> GetUserRoleMappings()
        {
            try
            {
                return await _userRoleMappingRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getpermissions")]
        [HttpGet]
        public async Task<IEnumerable<PermissionMaster>> GetPermissions()
        {
            try
            {
                return await _permissionRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getdataroompermissions")]
        [HttpGet]
        public async Task<IEnumerable<DataRoomPermission>> GetDataRoomPermissions()
        {
            try
            {
                return await _dataroomPermissionRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getdatarooms")]
        [HttpGet]
        public async Task<IEnumerable<DataRoom>> GetDataRooms()
        {
            try
            {
                return await _dataroomRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getfolders")]
        [HttpGet]
        public async Task<IEnumerable<Folder>> GetFolders()
        {
            try
            {
                return await _folderRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getfileswithfilters")]
        [HttpGet]
        public async Task<IEnumerable<File>> GetFileswithFilters([FromBody]FilterModel model)
        {
            try
            {
                //return await _fileRepository.DbGetDatawithQueryAsync("select * from DR_1");
                return await _fileRepository.GetDataAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getfiles")]
        [HttpGet]
        public async Task<IEnumerable<File>> GetFiles()
        {
            try
            {
                return await _fileRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getfolderpermissions")]
        [HttpGet]
        public async Task<IEnumerable<FolderPermission>> GetFolderPermissions()
        {
            try
            {
                return await _folderPermissionRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getfilepermissions")]
        [HttpGet]
        public async Task<IEnumerable<FilePermission>> GetFilePermissions()
        {
            try
            {
                return await _filePermissionRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updatefiles")]
        [HttpPost]
        public async Task UpdateFiles([FromBody]IEnumerable<File> files)
        {
            try
            {
                if(files!=null && files.Count() > 0)
                {
                    List<int> dataroomids = new List<int>();
                    dataroomids = files.Select(x => x.DataRoomId).Distinct().ToList();
                    foreach(var dataroomid in dataroomids)
                    {
                        var dataroomfiles = files.Where(x => x.DataRoomId == dataroomid).ToList();
                        double fileSize = 0;
                        foreach (var file in dataroomfiles)
                        {
                            fileSize += Convert.ToDouble(file.FileSize);
                        }
                        var dataroom = await _dataroomRepository.GetSingleAsync(x => x.Id == dataroomid);
                        var remainingSize = Convert.ToDouble(dataroom.DataRoomSize) - fileSize;
                        dataroom.DataRoomSize = Convert.ToString(remainingSize);
                        await _dataroomRepository.UpdateAsync(dataroom);
                    }
                    foreach (var file in files)
                    {                       
                        await _fileRepository.UpdateAsync(file);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("syncadusers")]
        [HttpPost]
        public async Task SyncADUsers([FromBody] IEnumerable<User> users)
        {
            try
            {
                _logger.LogDebug("Web Service--" + users.Count());
                if (users != null && users.Count() > 0)
                {
                    foreach(var user in users.ToList())
                    {
                        _logger.LogDebug("Web Service--user.Email" + user.EmailId);
                        if (!string.IsNullOrEmpty(user.EmailId))
                        {
                            var userdetails = await _userRepository.GetAsync(x => x.EmailId == user.EmailId);
                            if(userdetails!=null && userdetails.Count() > 0)
                            {
                                user.Id = userdetails.First().Id;                                
                                await _userRepository.UpdateAsync(user);
                            }
                            else
                            {
                                
                                await _userRepository.CreateAsync(user);
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Service -- " + ex.Message,ex);
                throw ex;
            }
        }

        [Route("deletefiles")]
        [HttpPost]
        public async Task DeleteFiles([FromBody] IEnumerable<File> files)
        {
            try
            {
                List<int> fileids = new List<int>();
                fileids = files.Select(x => x.Id).Distinct().ToList();
                IEnumerable<FileVersion> fileVersions = await _fileVersionRepository.GetAsync(x => fileids.Contains(x.FileId));
                await _fileRepository.DeleteRangeAsync(files);
                if(fileVersions!=null && fileVersions.Count() > 0)
                await _fileVersionRepository.DeleteRangeAsync(fileVersions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getworkflows")]
        [System.Web.Http.HttpGet]
        public async Task<IEnumerable<WorkFlowMaster>> GetWorkFlowMaster()
        {
            try
            {
                return await _workflowRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getworkflowusers")]
        [System.Web.Http.HttpGet]
        public async Task<IEnumerable<DataRoomWorkFlowUser>> GetDataRoomWorkFlowUsers()
        {
            try
            {
                return await _dataroomWorkFlowUserRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("gettodotasks")]
        [System.Web.Http.HttpGet]
        public async Task<IEnumerable<ToDoTask>> GetTodoTasks()
        {
            try
            {
                return await _todoTaskRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getsettings")]
        [System.Web.Http.HttpGet]
        public async Task<Setting> GetSetting()
        {
            try
            {
                return await _settingRepository.GetSingleAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getitemtrackercontrols")]
        [System.Web.Http.HttpGet]
        public async Task<IEnumerable<ItemTrackerControl>> GetItemTrackerControls()
        {
            try
            {
                return await _itemTrackerControlRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getemailconfigs")]
        [System.Web.Http.HttpGet]
        public async Task<IEnumerable<EmailConfiguration>> GetEmailConfigurations()
        {
            try
            {
                return await _emailRepository.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getadinfo")]
        [HttpGet]
        public async Task<List<ADInfo>> GetADInfo()
        {
            try
            {
                var adDetails = await _adRepository.GetAsync();
                if(adDetails!=null && adDetails.Count() > 0)
                {
                    //foreach(var ad in adDetails.ToList())
                    //{
                    //    ADInfo adObject = adDetails.First();
                    //    if(!string.IsNullOrEmpty(adObject.IsADSync))
                    //    adObject.IsADSync = _dataProtector.Unprotect(adObject.IsADSync);
                    //    adObject.IPAddress = _dataProtector.Unprotect(adObject.IPAddress);
                    //    adObject.DomainName = _dataProtector.Unprotect(adObject.DomainName);
                    //    adObject.CompanyName = _dataProtector.Unprotect(adObject.CompanyName);
                    //    adInfos.Add(adObject);
                    //}
                    
                    return adDetails.ToList();
                }
                return new List<ADInfo>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getitemtrackerdata")]
        [System.Web.Http.HttpGet]
        public async Task<IEnumerable<ItemTrackerData>> GetItemTrackerData()
        {
            try
            {
                return await _itemTrackerData.GetAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getitemtrackermetadata")]
        [System.Web.Http.HttpGet]
        public async Task<IEnumerable<ItemTrackerMetaData>> GetItemTrackermetaData()
        {
            try
            {
                var data = await _itemTrackerMetaDataRepository.GetAsync();
                if (data != null && data.Count() > 0)
                    return data;
                else
                    return new List<ItemTrackerMetaData>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getitemtrackerpermissions")]
        [System.Web.Http.HttpGet]
        public async Task<IEnumerable<ItemTrackerPermission>> GetItemTrackerPermissions()
        {
            try
            {
                var data = await _itemTrackerPermissionRepository.GetAsync();
                if (data != null && data.Count() > 0)
                    return data;
                else
                    return new List<ItemTrackerPermission>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
