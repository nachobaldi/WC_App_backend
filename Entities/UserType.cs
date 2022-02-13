using System.ComponentModel.DataAnnotations;

namespace Entities
{

    public class UserType
    {
        [Key]
        public int TypeId { get; set; }

        public string TypeName { get; set; }

        public UserType(string typeName)
        {
            TypeName = typeName;
        }

        public UserType()
        {
            
        }
    }
}