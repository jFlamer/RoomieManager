namespace RoomieManager.Models
{
    public class RankedRoomiesViewModel
    {
        public int RoomieId { get; set; }
        public string Name { get; set; }
        public string PhotoURL { get; set; }
        public int totalEffortPoints { get; set; }
        public int totalTasks { get; set; }
    }
}
