using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tasks.Model;
using tasks.Services;

namespace tasks.Controllers
{
    [ApiController]
    [Route("[controller")]
    public class TaskController
    {
        private readonly ILogger<TaskController> _logger;
        private readonly IStorageService _storage;

        public TaskController(ILogger<TaskController> logger, IStorageService storage)
        {
            _logger=logger;
            _storage=storage;
        }
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateTask([FromBody]NewTask newTask)
        {
            
        }
    }
}