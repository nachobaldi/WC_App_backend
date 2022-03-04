using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DbUtils;
using Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    //This controller take care all the request for all the task in project
    [Route("api/[controller]")]
    [ApiController]
    public class TasksPerProjectController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public TasksPerProjectController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        // GET: api/<TasksPerProjectController>
        [HttpGet]
        public ActionResult<List<TasksPerProject>> GetAll()
        {
            if (_dataContext.TasksPerProject != null)
            {
                return Ok(_dataContext.TasksPerProject.ToList());
            }

            return NotFound();
        }

        // GET api/<TasksPerProjectController>/5/1
        [HttpGet("{projectId}/{taskId}")]
        public ActionResult<TasksPerProject> GetTaskPerProjectByProjectIdAndTaskId(int projectId,int taskId)
        {
            //check if the project have this task
            var taskPerProject = _dataContext.TasksPerProject.FirstOrDefault(t => t.TaskId == taskId && t.ProjectId == projectId);
            if (taskPerProject != null) return Ok(taskPerProject);
            return NotFound();
        }
        // GET api/<TasksPerProjectController>/5
        [HttpGet("{projectId}")]
        public ActionResult<TasksPerProject> GetTasksPerProjectByProject(int projectId)
        {
            //check if project have tasks, if true return them
            var taskPerProject = _dataContext.TasksPerProject.Where(t => t.ProjectId == projectId).ToList().OrderBy(x=>x.ProjectId);
            if (taskPerProject != null) return Ok(taskPerProject);
            return NotFound();
        }

        // POST api/<TasksPerProjectController>/AddTask
        [HttpPost("AddTask")]
        public ActionResult<TasksPerProject> AddTask(TasksPerProject task)
        {
            //check if the task for this project exists
            var taskPerProject = GetTaskPerProjectByProjectIdAndTaskId(task.ProjectId, task.TaskId);
            if (taskPerProject.Value != null) return BadRequest("task already exists for this project");
            _dataContext.TasksPerProject.Add(task);
            _dataContext.SaveChanges();
            return Created("", $"{task} Created!");
        }

        // PUT api/<TasksPerProjectController>/updateTaskPerProject
        [HttpPut("updateTaskPerProject")]
        public ActionResult<TasksPerProject> UpdateProject([FromBody] TasksPerProject projectTask)
        {
            //check if the task for this project exists
            var existingTask = _dataContext.TasksPerProject.FirstOrDefault(t => t.TaskId == projectTask.TaskId && t.ProjectId == projectTask.ProjectId);
            if (existingTask == null) return NotFound();

            if (projectTask.Grade != existingTask.Grade)
            {
                existingTask.Grade = projectTask.Grade;
            }
            _dataContext.SaveChanges();
            return Ok($"Grade: {projectTask.Grade} to task {projectTask.TaskId} was updated successfully");
        }

        // DELETE api/<TasksPerProjectController>Delete/3/5
        [HttpDelete("Delete/{projectId}/{taskId}")]
        public ActionResult<User> DeleteTaskPerProject(int projectId,int taskId)
        {
            //check if the task for this project exists
            var task = _dataContext.TasksPerProject.FirstOrDefault(t => t.TaskId == taskId && t.ProjectId == projectId);
            if (task == null) return NotFound();
            _dataContext.TasksPerProject.Remove(task);
            _dataContext.SaveChanges();
            return Ok($"{task.ProjectId}-{task.TaskId} Deleted");

        }
        // DELETE api/<TasksPerProjectController>/Delete/5
        [HttpDelete("Delete/{projectId}")]
        public ActionResult<User> DeleteTasksFromProject(int projectId)
        {
            //check if the task for this project exists
            var tasks = _dataContext.TasksPerProject.Where(t => t.ProjectId == projectId).ToList();
            if (tasks == null) return NotFound();
            //delete all tasks from this project
            foreach (var task in tasks)
            {
                _dataContext.TasksPerProject.Remove(task);
            }
            _dataContext.SaveChanges();
            return Ok($"Tasks from project {projectId} Deleted");

        }
    }
}
