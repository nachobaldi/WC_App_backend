using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbUtils;
using Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    //This controller take care all the request for tasks
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ProjectTaskController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        // GET: api/<ProjectTaskController>
        [HttpGet]
        public ActionResult<List<ProjectTask>> GetAll()
        {
            if (_dataContext.Tasks != null)
            {
                return Ok(_dataContext.Tasks.ToList());
            }

            return NotFound();
        }

        // GET api/<ProjectTaskController>/5
        [HttpGet("{taskId}")]
        public ActionResult<ProjectTask> GetTask(int taskId)
        {
            //check if task exists
            var task = _dataContext.Tasks.FirstOrDefault(t => t.TaskId == taskId);
            if (task != null) return Ok(task);
            return NotFound();
        }
        // GET api/<ProjectTaskController>/getTaskid/code
        [HttpGet("getTaskid/{taskName}")]
        public ActionResult<ProjectTask> GetTaskId(string taskName)
        {
            //check if task exists
            var task = _dataContext.Tasks.FirstOrDefault(t => t.TaskName == taskName);
            if (task != null) return Ok(task.TaskId);
            return NotFound();
        }

        // POST api/<ProjectTaskController>/AddTask
        [HttpPost("AddTask")]
        public ActionResult<ProjectTask> AddTask( ProjectTask task)
        {
            task.TaskPercentage /= 100;
            var sum = _dataContext.Tasks.Sum(x => x.TaskPercentage);
            if (sum + task.TaskPercentage > 1)
            {
                return BadRequest("Task percentage over 100%");
                //return Problem(statusCode: 400, title: "Task percentage over 100%");
            }
            _dataContext.Tasks.Add(task);
            _dataContext.SaveChanges();
            return Created("", $"{task} Created!");
        }

        // PUT api/<ProjectTaskController>/updateTask/5
        [HttpPut("UpdateTask/{taskId}")]
        public ActionResult<ProjectTask> UpdateProject([FromBody] ProjectTask projectTask)
        {
            //trasform to float
            projectTask.TaskPercentage /= 100;
            //take all the tasks without the updated task
            var tasks = _dataContext.Tasks.Where(x => x.TaskId != projectTask.TaskId).Sum(x => x.TaskPercentage);
            var sum = tasks;
            //check if the sum is not over the 1 (1 is equal to 100%)
            if (sum + projectTask.TaskPercentage > 1)
            {
                return BadRequest("Task percentage over 100%");
            }
            var existingTask = _dataContext.Tasks.FirstOrDefault(t => t.TaskId == projectTask.TaskId);
            if (existingTask == null) return NotFound();
            
            //check what was updated
            if (projectTask.TaskName != existingTask.TaskName)
            {
                existingTask.TaskName = projectTask.TaskName;
            }
            if (projectTask.TaskPercentage != existingTask.TaskPercentage)
            {
                existingTask.TaskPercentage = projectTask.TaskPercentage;
            }

            _dataContext.SaveChanges();
            return Ok($"Task: {projectTask.TaskName} was updated successfully");
        }

        // DELETE api/<ProjectTaskController>/Delete/5
        [HttpDelete("Delete/{taskId}")]
        public ActionResult<User> DeleteTask(int taskId)
        {
            //check if task exists
            var task = _dataContext.Tasks.FirstOrDefault(t => t.TaskId == taskId);

            if (task == null) return NotFound();
            _dataContext.Tasks.Remove(task);
            _dataContext.SaveChanges();
            return Ok($"{task.TaskId}-{task.TaskName} Deleted");

        }
    }
}
