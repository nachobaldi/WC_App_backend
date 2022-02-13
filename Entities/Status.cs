using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public Status(string statusName)
        {
            StatusName = statusName;
        }

        public Status()
        {
            
        }
    }
}