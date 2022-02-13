using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Entities
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int Year { get; set; }
        public int StudentId { get; set; }

        public string Description { get; set; }
        [JsonIgnore]
        public List<TasksPerProject> TasksPerProject { get; set; }
        [JsonIgnore]
        public User Student { get; set; }

        public Project(string projectName, int year, int studentId,string description)
        {
            ProjectName = projectName;
            Year = year;
            StudentId = studentId;
            Description = description;
        }

        public Project()
        {
            
        }

        public override string ToString()
        {
            return $"Project ID: {ProjectId}, Name: {ProjectName}, Student: {StudentId}, Year: {Year},Description: {Description}. ";
        }
    }
}