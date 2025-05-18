using System.ComponentModel.DataAnnotations;

namespace RoomieManager.Models
{
    public class UserModel {
        [Key]
        public int userId { get; set; }
        [Required(ErrorMessage = "Enter username")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Enter password")]
        public string password { get; set; }
        [Required]
        public bool isAdmin { get; set; }
    }


}