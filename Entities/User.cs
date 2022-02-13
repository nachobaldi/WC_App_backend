using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{

    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Password { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        
        [MaxLength(10)]
        [MinLength(10)]
        public string PhoneNum { get; set; }
        
        public int TypeId { get; set; }
        
        public int StatusId { get; set; }
        
        [JsonIgnore]
        [AllowNull]
        public virtual Status Status { get; set; }
      
        [JsonIgnore]
        [AllowNull]
         public virtual UserType Type { get; set; }



        public User(int userId, string firstName, string lastName, string password, string email, string phoneNum, int typeId, int statusId)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
            PhoneNum = phoneNum;
            StatusId = statusId;
            TypeId = typeId;
        }

        public User()
        {
            
        }

        public User(User user)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Password = user.Password;
            Email = user.Email;
            PhoneNum = user.PhoneNum;
            TypeId = user.TypeId;
            StatusId = user.StatusId;
        }

        
        public override string ToString()
        {

            return $"ID: {UserId}, Name:{GetFullName()}, phone: {PhoneNum}, email: {Email}";
        }
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }


    }
}