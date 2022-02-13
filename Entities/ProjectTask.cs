using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Entities
{
    public class ProjectTask
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        
        public double TaskPercentage { get; set; }
        [JsonIgnore]
        public List<TasksPerProject> TasksPerProject { get; set; }


        public ProjectTask( string taskName, double taskPercentage)
        {
            TaskName = taskName;
            TaskPercentage = taskPercentage;

        }

        public ProjectTask()
        {
            
        }


        public override string ToString()
        {
            return $"Task Id : {TaskId}, Task Name: {TaskName}, TaskPercentage :{TaskPercentage}";
        }
    }
}