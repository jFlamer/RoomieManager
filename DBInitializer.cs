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
            var admin = new UserModel
            {
                userName = "admin",
                password = "21232F297A57A5A743894A0E4A801FC3", // MD5 for 'admin'
                isAdmin = true
            };
            context.Users.Add(admin);
            context.SaveChanges();
        }
    }
}