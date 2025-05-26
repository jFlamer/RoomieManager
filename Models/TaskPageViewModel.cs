using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomieManager.Models
{
    public class TaskPageViewModel
    {
        public List<TaskModel> Tasks { get; set; }
        public List<TaskTypeModel> TaskTypes { get; set; }
        public List<RoomieModel> Roomies { get; set;}
    }
}
