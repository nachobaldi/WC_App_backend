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
    public class UserTypeController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UserTypeController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
    
        // GET: api/<UserTypeController>
        [HttpGet]
        public ActionResult<List<UserType>> GetUserTypes()
        {
            if (_dataContext.UserTypes != null)
            {
                return Ok(_dataContext.UserTypes.ToList());
            }

            return NotFound();
        }

        // GET api/<UserTypeController>/5
        [HttpGet("{typeId}")]
        public ActionResult<UserType> GetUserType(int typeId)
        {
            var userType = _dataContext.UserTypes.FirstOrDefault(t => t.TypeId== typeId);
            if (userType != null) return Ok(userType);
            return NotFound();
        }

        // POST api/<UserTypeController>
        [HttpPost]
        public ActionResult<UserType> AddUserType([FromForm] UserType userType)
        {
            _dataContext.UserTypes.Add(userType);
            _dataContext.SaveChanges();
            return Created("", $"{userType} Created!");
        }

        // PUT api/<UserTypeController>/5
        [HttpPut("updateUserType")]
        public ActionResult<Type> UpdateType([FromBody] UserType typeUpdate)
        {
            var existingType = _dataContext.UserTypes.FirstOrDefault(t=>t.TypeId == typeUpdate.TypeId);
            if (existingType == null) return NotFound();
            if (typeUpdate.TypeName != null)
            {
                existingType.TypeName = typeUpdate.TypeName;
            }
            _dataContext.SaveChanges();
            return Ok($"User Type: {existingType.TypeName} was updated successfully");
        }

        // DELETE api/<UserTypeController>/5
        [HttpDelete("{typeid}")]
        public ActionResult<UserType> DeleteType(int typeid)
        {
            var existingType = _dataContext.UserTypes.FirstOrDefault(t => t.TypeId== typeid);
            if (existingType == null) return NotFound();
            _dataContext.UserTypes.Remove(existingType);
            _dataContext.SaveChanges();
            return Ok($"User Type: {existingType.TypeName} was deleted!");
        }
    }
}
