using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomieManager.Models
{
    public class RoomieModel
    {
        [Key]
        public int roomieId { get; set; }
        [Required]
        public int userId { get; set; }
        [ForeignKey("userId")]
        public UserModel user { get; set; }
        [Required(ErrorMessage = "Enter roomie name")]
        public string name { get; set; }
        public string photoURL { get; set; }
    }
}