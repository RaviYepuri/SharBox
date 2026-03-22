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
    public class ManageItemTrackerController : ControllerBase
    {
        private readonly ISqlRepository<ItemTrackerControl> _sqlItemTrackerControlRepository;
        private readonly ISqlRepository<ItemTrackerData> _sqlItemTrackerDataRepository;
        private readonly ISqlRepository<ItemTrackerMetaData> _sqlItemTrackerRepository;
        private readonly ISqlRepository<ItemTrackerPermission> _sqlItemTrackerPermissionRepository;
        private readonly ISqlRepository<ItemTrackerHistory> _sqlItemTrackerHistoryRepository;
        public ManageItemTrackerController(
            ISqlRepository<ItemTrackerControl> sqlItemTrackerControlRepository,
            ISqlRepository<ItemTrackerData> sqlItemTrackerDataRepository,
            ISqlRepository<ItemTrackerMetaData> sqlItemTrackerRepository,
            ISqlRepository<ItemTrackerPermission> sqlItemTrackerPermissionRepository,
            ISqlRepository<ItemTrackerHistory> sqlItemTrackerHistoryRepository
            )
        {
            _sqlItemTrackerControlRepository = sqlItemTrackerControlRepository;
            _sqlItemTrackerDataRepository = sqlItemTrackerDataRepository;
            _sqlItemTrackerRepository = sqlItemTrackerRepository;
            _sqlItemTrackerPermissionRepository = sqlItemTrackerPermissionRepository;
            _sqlItemTrackerHistoryRepository = sqlItemTrackerHistoryRepository;
        }

        [Route("createconfigcontrol")]
        [System.Web.Http.HttpPost]
        public async Task<int> CreateItemTrackerControl([System.Web.Http.FromBody] ItemTrackerControl model)
        {
            try
            {
                await _sqlItemTrackerControlRepository.CreateAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updateconfigcontrol")]
        [System.Web.Http.HttpPost]
        public async Task<int> UpdateItemTrackerConfigControl([System.Web.Http.FromBody] ItemTrackerControl model)
        {
            try
            {
                await _sqlItemTrackerControlRepository.UpdateAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deleteconfigcontrol")]
        [System.Web.Http.HttpDelete]
        public async Task DeleteItemTrackerConfigControl([System.Web.Http.FromBody] ItemTrackerControl model)
        {
            try
            {
                await _sqlItemTrackerControlRepository.DeleteAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("createitemtrackerdata")]
        [System.Web.Http.HttpPost]
        public async Task<int> CreateItemTrackerData([System.Web.Http.FromBody] ItemTrackerData model)
        {
            try
            {
                await _sqlItemTrackerDataRepository.CreateAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updateitemtrackerdata")]
        [System.Web.Http.HttpPost]
        public async Task<int> UpdateItemTrackerData([System.Web.Http.FromBody] ItemTrackerData model)
        {
            try
            {
                await _sqlItemTrackerDataRepository.UpdateAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deleteitemtrackerdata")]
        [System.Web.Http.HttpDelete]
        public async Task DeleteItemTrackerData([System.Web.Http.FromBody] ItemTrackerData model)
        {
            try
            {
                await _sqlItemTrackerDataRepository.DeleteAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("createitemtracker")]
        [System.Web.Http.HttpPost]
        public async Task<int> CreateItemTracker([System.Web.Http.FromBody] ItemTrackerMetaData model)
        {
            try
            {
                await _sqlItemTrackerRepository.CreateAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updateitemtracker")]
        [System.Web.Http.HttpPost]
        public async Task<int> UpdateItemTracker([System.Web.Http.FromBody] ItemTrackerMetaData model)
        {
            try
            {
                await _sqlItemTrackerRepository.UpdateAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deleteitemtracker")]
        [System.Web.Http.HttpDelete]
        public async Task DeleteItemTracker([System.Web.Http.FromBody] ItemTrackerMetaData model)
        {
            try
            {
                await _sqlItemTrackerRepository.DeleteAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("saveitemtrackerpermission")]
        [System.Web.Http.HttpPost]
        public async Task<int> CreateItemTrackerPermission([System.Web.Http.FromBody] ItemTrackerPermission model)
        {
            try
            {
                await _sqlItemTrackerPermissionRepository.CreateAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updateitemtrackerpermission")]
        [System.Web.Http.HttpPost]
        public async Task<int> UpdateItemTrackerPermission([System.Web.Http.FromBody] ItemTrackerPermission model)
        {
            try
            {
                await _sqlItemTrackerPermissionRepository.UpdateAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deleteitemtrackerpermission")]
        [System.Web.Http.HttpPost]
        public async Task DeleteItemTrackerPermission([System.Web.Http.FromBody] ItemTrackerPermission model)
        {
            try
            {
                await _sqlItemTrackerPermissionRepository.DeleteAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("saveitemtrackerhistory")]
        [System.Web.Http.HttpPost]
        public async Task SaveItemTrackerHistory([System.Web.Http.FromBody] ItemTrackerHistory model)
        {
            try
            {
                await _sqlItemTrackerHistoryRepository.CreateAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getitemtrackerhistory/{itemtrackerid}/{rowguid}")]
        [System.Web.Http.HttpGet]
        public async Task<IEnumerable<ItemTrackerHistory>> GetItemTrackerHistory(int itemtrackerid,string rowguid)
        {
            try
            {
                return await _sqlItemTrackerHistoryRepository.GetAsync(x=>x.ItemTrackerRowGuid == rowguid && x.ItemTrackerId == itemtrackerid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
