using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomieManager.Models
{

    public class PrioritiesModel
    {
        [Required]
        public int typeID { get; set; }
        [ForeignKey("typeID")]
        public TaskTypeModel taskType { get; set; }
        [Required]
        public int roommieID { get; set; }
        [ForeignKey("roommieID")]
        public RoomieModel roomie { get; set; }
        [Required(ErrorMessage = "Enter priority (in percentage)")]
        [Range(0, 100, ErrorMessage = "Priority must be between 0 and 100")]
        public int priority { get; set; }
    }
}