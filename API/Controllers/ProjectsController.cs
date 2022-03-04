using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using DbUtils;
using Entities;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{

    //This controller take care all the request for projects
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ProjectsController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        // GET: api/<ProjectsController>
        [HttpGet]
        [HttpGet("GetAll")]
        public ActionResult<List<Project>> GetAll()
        {
            if (_dataContext.Projects != null)
            {
                return Ok(_dataContext.Projects.ToList());
            }

            return NotFound();
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{projectId}")]
        public ActionResult<Project> GetProject(int projectId)
        {
            var project = _dataContext.Projects.FirstOrDefault(p => p.ProjectId == projectId);
            if (project != null) return Ok(project);
            return NotFound();
        }

        // GET api/<ProjectsController>/projectByUserId/5
        [HttpGet("projectByUserId/{userId}")]
        public ActionResult<Project> GetProjectByUserId(int userId)
        {
            var project = _dataContext.Projects.FirstOrDefault(p => p.StudentId== userId);
            if (project != null) return Ok(project);
            return NotFound();
        }


        // POST api/<ProjectsController>/AddProject
        [HttpPost("AddProject")]
        public ActionResult<Project> AddProject( Project project)
        {
            //check if project exists, or student already have project
            var projectExist = _dataContext.Projects.FirstOrDefault(p => (p.ProjectName== project.ProjectName && p.Year == project.Year ) || p.StudentId==project.StudentId);
            if (projectExist != null)
            {
                return BadRequest("User already have a project.");
            }
            _dataContext.Projects.Add(project);
            _dataContext.SaveChanges();
            return Created("", $"{project} Created!");
        }

        // PUT api/<ProjectsController>/Update/5
        [HttpPut("Update/{projectId}")]
        public ActionResult<Project> UpdateProject([FromBody] Project projectUpdate)
        {
            //check if project exists
            var existingProject = _dataContext.Projects.FirstOrDefault(p => p.ProjectId == projectUpdate.ProjectId);
            if (existingProject == null) return NotFound();
            if (projectUpdate.ProjectName != existingProject.ProjectName)
            {
                existingProject.ProjectName = projectUpdate.ProjectName;
            }
            if (projectUpdate.Description != existingProject.Description)
            {
                existingProject.Description = projectUpdate.Description;
            }
            if (projectUpdate.StudentId > existingProject.StudentId)
            {
                existingProject.StudentId = projectUpdate.StudentId;
            }


            _dataContext.SaveChanges();
            return Ok($"Project: {projectUpdate.ProjectId} was updated successfully");
        }

        // DELETE api/<ProjectsController>/Delete/5
        [HttpDelete("Delete/{projectId}")]
        public ActionResult<Project> DeleteProject(int projectId)
        {
            //check if project exists
            var projectToRemove = _dataContext.Projects.FirstOrDefault(p => p.ProjectId == projectId);
            if (projectToRemove == null) return NotFound();
            _dataContext.Projects.Remove(projectToRemove);
            _dataContext.SaveChanges();
            return Ok($"{projectToRemove.ProjectId}-{projectToRemove.ProjectName} Deleted");

        }
    }
}
