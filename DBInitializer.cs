using RoomieManager.Models;

namespace RoomieManager.Data
{
    public static class DbInitializer
    {
        public static void Initialize(RoomieManagerDBContext context)
        {
            if(context.Users.Any())
            {
                return; // DB has been seeded
            }

            context.SaveChanges();
        }
    }
}