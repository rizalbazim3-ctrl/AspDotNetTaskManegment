using Microsoft.AspNetCore.Mvc;
using TaskList.Models;
using TaskList.Services;

namespace TaskList.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult GetAction()
        {
            return Ok(_taskService.getTask());
        }

        [HttpPost]
        public IActionResult PostAction(TaskItems data)
        {
            _taskService.addTask(data);
            return StatusCode(201,data);
        }
        [HttpPut("{id}")]
        public IActionResult PutAction(TaskItems data, int id)
        {
            bool status = _taskService.editTask(data,id);

            if (status)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAction(int id)
        {
            bool status = _taskService.deleteTask(id);

            if (status)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}