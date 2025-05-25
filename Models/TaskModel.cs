using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomieManager.Models
{
    public class TaskModel
    {
        [Key]
        public int taskID { get; set; }
        [Required(ErrorMessage = "Enter task type")]
        public int typeID { get; set; }
        [ForeignKey("typeID")]
        public TaskTypeModel taskType { get; set; }
        public int? roomieID { get; set; }
        [ForeignKey("roomieID")]
        public RoomieModel? roomie { get; set; }
        public DateTime? plannedStartDateTime { get; set; }
        public DateTime? plannedFinishDateTime { get; set; }
        public DateTime? reviewDateTime { get; set; }
        public string? reviewNote { get; set; }
        public int? reviewRoomieID { get; set; }
        [ForeignKey("reviewRoomieID")]
        public RoomieModel? reviewRoomie { get; set; }

    }
    
}
