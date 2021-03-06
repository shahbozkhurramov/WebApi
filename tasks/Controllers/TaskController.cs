using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tasks.Mapper;
using tasks.Model;
using tasks.Services;

namespace tasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly IStorageService _storage;

        public TasksController(ILogger<TasksController> logger, IStorageService storage)
        {
            _logger = logger;
            _storage = storage;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateTask([FromBody]NewTask newTask)
        {
            var taskEntity = newTask.ToTaskEntity();
            var insertResult = await _storage.InsertTaskAsync(taskEntity);

            if(insertResult.IsSuccess)
            {
                return CreatedAtAction("CreateTask", taskEntity);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, new { message = insertResult.exception.Message });
        }


        [HttpPut]
        public async Task<IActionResult> UpdateTaskAsync([FromBody]UpdatedTask updatedTask)
        {
            var entity = updatedTask.ToTaskEntity();
            var updateResult = await _storage.UpdateTaskAsync(entity);

            if(updateResult.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(updateResult.exception.Message);
        }
        
        [HttpGet]
        public async Task<IActionResult> QueryTasks([FromQuery]TaskQuery query)
        {
            var tasks = await _storage.GetTaskAsync(title: query.Title);

            if(tasks.Any())
            {
                return Ok(tasks);
            }

            return NotFound("No tasks exist!");
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveTaskAsync([FromQuery]TaskQuery deletedTask)
        {
            try
            {
                var tasks = await _storage.GetTaskAsync(title: deletedTask.Title);
                var deleteResult = await _storage.DeleteTaskAsync(tasks[0]);

                if(deleteResult.IsSuccess)
                {
                    return Ok(tasks[0]);
                }
                return BadRequest(deleteResult.exception.Message);
            }

            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}