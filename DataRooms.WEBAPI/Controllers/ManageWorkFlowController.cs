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
    public class ManageWorkFlowController : ControllerBase
    {
        public ISqlRepository<WorkFlowMaster> _workflowRepository;
        public ManageWorkFlowController(ISqlRepository<WorkFlowMaster> workflowRepository)
        {
            _workflowRepository = workflowRepository;
        }

        [Route("saveworkflow")]
        [System.Web.Http.HttpPost]
        public async Task<int> SaveWorkFlow([System.Web.Http.FromBody] WorkFlowMaster workflow)
        {
            try
            {
                await _workflowRepository.CreateAsync(workflow);
                return workflow.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 0;
        }

        [Route("updateworkflow")]
        [System.Web.Http.HttpPut]
        public async Task<int> UpdateWorkFlow([System.Web.Http.FromBody] WorkFlowMaster workflow)
        {
            try
            {
                await _workflowRepository.UpdateAsync(workflow);
                return workflow.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 0;
        }

        [Route("deleteworkflow")]
        [System.Web.Http.HttpPost]
        public async Task<int> DeleteWorkFlow([System.Web.Http.FromBody] WorkFlowMaster workflow)
        {
            try
            {
                await _workflowRepository.DeleteAsync(workflow);
                return workflow.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 0;
        }
    }
}
