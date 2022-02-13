using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class UserType
    {
        [Key]
        public int TypeID { get; set; }

        public string TypeName { get; set; }


    }
}