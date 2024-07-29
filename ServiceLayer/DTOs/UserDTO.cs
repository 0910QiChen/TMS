using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTOs
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]

        public string UserPassword { get; set; }
    }
}