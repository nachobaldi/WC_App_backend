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
    public class StatusController : ControllerBase
    {
        private readonly DataContext _dataContext;

    
        
        public StatusController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        // GET: api/<StatusController>
        [HttpGet]
        public ActionResult<List<Status>> GetStatuses()
        {
            if (_dataContext.Statuses != null)
            {
                return Ok(_dataContext.Statuses.ToList());
            }

            return NotFound();
        }

        // GET api/<StatusController>/5
        [HttpGet("{statusId}")]
        public ActionResult<Status> GetStatus(int statusId)
        {
            var status = _dataContext.Statuses.FirstOrDefault(s => s.StatusId == statusId);
            if (status != null) return Ok(status);
            return NotFound();
        }

        // POST api/<StatusController>
        [HttpPost("addStatus")]
        public ActionResult<Status> AddStatus([FromForm] Status status)
        {
            _dataContext.Statuses.Add(status);
            _dataContext.SaveChanges();
            return Created("", $"{status} Created!");
        }

        // PUT api/<StatusController>/5
        [HttpPut("updateStatus")]
        public ActionResult<Status> UpdateStatus([FromBody] Status statusUpdate)
        {
            var existingStatus = _dataContext.Statuses.FirstOrDefault(s => s.StatusId == statusUpdate.StatusId);
            if (existingStatus == null) return NotFound();
            if (statusUpdate.StatusName != null)
            {
                existingStatus.StatusName = statusUpdate.StatusName;
            }
            _dataContext.SaveChanges();
            return Ok($"Status: {statusUpdate.StatusId} was updated successfully");
        }

        // DELETE api/<StatusController>/5
        [HttpDelete("{statusId}")]
        public ActionResult<Status> DeleteStatus(int statusId)
        {
            var existingStatus = _dataContext.Statuses.FirstOrDefault(s => s.StatusId == statusId);
            if (existingStatus == null) return NotFound();
            _dataContext.Statuses.Remove(existingStatus);
            _dataContext.SaveChanges();
            return Ok($"Status: {existingStatus.StatusName} was deleted!");
        }
    }
}
