using System.ComponentModel.DataAnnotations;

namespace RoomieManager.Models
{
    public class TaskTypeModel
    {
        [Key]
        public int taskTypeId { get; set; }
        [Required(ErrorMessage = "Enter task type name")]
        public string name { get; set; }
        [Required(ErrorMessage = "Enter task type description")]
        public string description { get; set; }
        [Required(ErrorMessage = "Enter task type duration [in minutes]")]
        public int duration { get; set; }
        [Required (ErrorMessage = "Enter effort points")]
        public int effortPoints { get; set; }
    }
}