using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Task
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public int UserID { get; set; }
        public int MentorID { get; set; }
        public int Grade { get; set; }
        public double TaskPercentage { get; set; }






        public override string ToString()
        {
            return $"Task Id : {TaskID}, Task Name: {TaskName}, TaskPercentage :{TaskPercentage}";
        }
    }
}