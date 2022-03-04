using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DbUtils;
using Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    //This controller take care all the request for users
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public UserController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        // GET: api/<UserController>
        [HttpGet("GetAll")]
        [HttpGet("")]
        public ActionResult<List<User>> GetAll()
        {
            if (_dataContext.Users != null)
            {
                return Ok(_dataContext.Users.ToList().OrderBy(x=>x.LastName));

            }
            return NotFound();
        }


        // GET: api/<UserController>/admins/getAll
        [HttpGet("admins/getAll")]
        public ActionResult<List<User>> GetAdmins()
        {
            if (_dataContext.Users != null)
            {
                return Ok(_dataContext.Users.Where(x => x.TypeId==1).ToList());

            }
            return NotFound();
        }
        // GET: api/<UserController>/admins/getAll
        [HttpGet("students/getAll")]
        public ActionResult<List<User>> GetStudents()
        {
            if (_dataContext.Users != null)
            {
                return Ok(_dataContext.Users.Where(x=> x.TypeId==3).ToList().OrderBy(x => x.LastName));

            }
            return NotFound();
        }

        // GET: api/<UserController>/admins/getAll
        [HttpGet("mentors/getAll")]
        public ActionResult<List<User>> GetMentors()
        {
            if (_dataContext.Users != null)
            {
                return Ok(_dataContext.Users.Where(x => x.TypeId == 2).ToList().OrderBy(x => x.LastName));

            }
            return NotFound();
        }

        // GET: api/<UserController>/admins/getAll
        [HttpGet("login/{email}/{password}")]
        public ActionResult<User> Login(string email, string password)
        {
            //validate the user login
            var user = _dataContext.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (user != null)
            {
                return user;
            }
            return NotFound("Email or password are incorrect.");
        }

        // GET: api/<UserController>/admins/getAll
        [HttpGet("getUserByEmail/{email}")]
        public ActionResult<User> GetUserByEmail(string email)
        {
            //check if user with this email exists
            var user = _dataContext.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                return user;
            }
            return NotFound();
        }
        // GET: api/<UserController>/admins/getAll
        [HttpGet("{userId}")]
        public ActionResult<User> GetUser(int userId)
        {
            //check if the task for this project exists
            var user = _dataContext.Users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                return user;
            }
            return NotFound();
        }

        // POST api/<UsersController>AddUser
        [HttpPost("AddUser")]
        public ActionResult<User> AddUser( User user)
        {
            //check if user already exists
            var userExist = _dataContext.Users.FirstOrDefault(p => p.UserId == user.UserId);
            if (userExist != null)
            {
                return BadRequest("User with this id already exists");
            }
            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();
            return Created("", $"{user} Created!");
        }

        // PUT api/<UsersController>/editUser/5
        [HttpPut("editUser/{userid}")]
        public ActionResult<User> UpdateUser([FromBody] User userUpdate)
        {
            //check if user exists
            var existingUser = GetUser(userUpdate.UserId);
            if (existingUser == null) return NotFound();
            //check what was updated
            if (userUpdate.FirstName != existingUser.Value.FirstName)
            {
                existingUser.Value.FirstName = userUpdate.FirstName;
            }
            if (userUpdate.LastName != existingUser.Value.LastName)
            {
                existingUser.Value.LastName = userUpdate.LastName;
            }
            if (userUpdate.Password != existingUser.Value.Password)
            {
                existingUser.Value.Password = userUpdate.Password;
            }
            if (userUpdate.Email != existingUser.Value.Email)
            {
                existingUser.Value.Email = userUpdate.Email;
            }
            if (userUpdate.PhoneNum != existingUser.Value.PhoneNum)
            {
                existingUser.Value.PhoneNum = userUpdate.PhoneNum;
            }
            if (userUpdate.TypeId != existingUser.Value.TypeId)
            {
                existingUser.Value.TypeId = userUpdate.TypeId;
            }
            if (userUpdate.StatusId != existingUser.Value.StatusId)
            {
                existingUser.Value.StatusId = userUpdate.StatusId;
            }

            _dataContext.SaveChanges();

            return Ok(
                $"The changes are: {existingUser.Value.UserId},{existingUser.Value.GetFullName()},{existingUser.Value.Email},{existingUser.Value.PhoneNum}");
        }

        // DELETE api/<UsersController>/Delete/5
        [HttpDelete("Delete/{userID}")]
            public ActionResult<User> DeleteUser(int userID)
            {
                //check if user exists
                var user = GetUser(userID);
                if (user.Value == null) return NotFound();
                _dataContext.Users.Remove(user.Value);
                _dataContext.SaveChanges();
                return Ok($"{user.Value.UserId}-{user.Value.GetFullName()} Deleted");

            }
        }
}
