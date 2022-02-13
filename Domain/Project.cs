using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Project
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int Year { get; set; }
        public int StudentID { get; set; }

        public User Student { get; set; }
        public string Description { get; set; }
        public List<Task> Tasks { get; set; }




        public override string ToString()
        {
            return $"Project ID: {ProjectID}, Name: {ProjectName}, Student: {Student.GetFullName()}, Year: {Year},Description: {Description}. ";
        }
    }
}