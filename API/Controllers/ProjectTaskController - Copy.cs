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
    [Route("api/[controller]")]
    [ApiController]
    public class TasksPerProjectController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public TasksPerProjectController(DataContext dataContext)
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
            var task = _dataContext.Tasks.FirstOrDefault(t => t.TaskId == taskId);
            if (task != null) return Ok(task);
            return NotFound();
        }


        // POST api/<ProjectTaskController>
        [HttpPost]
        public ActionResult<ProjectTask> AddTask([FromForm] ProjectTask task)
        {

            _dataContext.Tasks.Add(task);
            _dataContext.SaveChanges();
            return Created("", $"{task} Created!");
        }

        // PUT api/<ProjectTaskController>/5
        [HttpPut("updateTask")]
        public ActionResult<ProjectTask> UpdateProject([FromBody] ProjectTask projectTask)
        {
            var existingTask = _dataContext.Tasks.FirstOrDefault(t => t.TaskId == projectTask.TaskId);
            if (existingTask == null) return NotFound();
            if (projectTask.TaskName != null)
            {
                existingTask.TaskName = projectTask.TaskName;
            }
            if (projectTask.TaskPercentage > 0)
            {
                existingTask.TaskPercentage = projectTask.TaskPercentage;
            }

            _dataContext.SaveChanges();
            return Ok($"Task: {projectTask.TaskName} was updated successfully");
        }

        // DELETE api/<ProjectTaskController>/5
    }
}
