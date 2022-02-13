using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class User
    {

        public User(int userId, string firstname, string lastname, string password, string email, string phoneNum, UserType type, Status status)
        {
          
        }

        

        [Key]
        public int UserID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(10)]
        public string PhoneNum { get; set; }

        [Required]
        public UserType Type { get; set; }

        [Required]
        public Status Status { get; set; }

        public List<Task> Tasks { get; set; }




        public override string ToString()
        {

            return $"ID: {UserID}, Name:{GetFullName()}, phone: {PhoneNum}, email: {Email}, status: {Status.StatusName}";
        }
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }


    }
}