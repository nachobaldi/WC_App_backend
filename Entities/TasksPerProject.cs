using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities
{
    public class TasksPerProject
    {
        [AllowNull]
        public int Grade { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public int TaskId { get; set; }
        
        public ProjectTask ProjectTask { get; set; }


        public TasksPerProject(int projectId, int taskId, int grade)
        {
            ProjectId = projectId;
            TaskId = taskId;
            Grade = grade;
        }

        public TasksPerProject()
        {

        }

        public override string ToString()
        {
            return $"Task Id : {TaskId}, Project Id: {ProjectId}, Grade: {Grade}";
        }


    }
}