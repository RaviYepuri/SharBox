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
    public class ManageTaskController : ControllerBase
    {
        public ISqlRepository<ToDoTask> _todoTaskRepository;

        public ManageTaskController(ISqlRepository<ToDoTask> todoTaskRepository)
        {
            _todoTaskRepository = todoTaskRepository;
        }

        [Route("savetodotask")]
        [HttpPost]
        public async Task<int> SaveTodoTask([FromBody] ToDoTask todoTask)
        {
            try
            {
                await _todoTaskRepository.CreateAsync(todoTask);
                return todoTask.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("updatetodotask")]
        [HttpPut]
        public async Task UpdateTodoTask([FromBody] ToDoTask todoTask)
        {
            try
            {
                await _todoTaskRepository.UpdateAsync(todoTask);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("deletetodotask")]
        [HttpPost]
        public async Task DeleteTodoTask([FromBody] ToDoTask todoTask)
        {
            try
            {
                await _todoTaskRepository.DeleteAsync(todoTask);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
