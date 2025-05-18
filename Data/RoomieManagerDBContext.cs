using Microsoft.EntityFrameworkCore;
using RoomieManager.Models;

namespace RoomieManager.Data
{
    public class RoomieManagerDBContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoomieModel> Roomies { get; set; }
        public DbSet<TaskTypeModel> TaskTypes { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<PrioritiesModel> Priorities { get; set; }

        public RoomieManagerDBContext(DbContextOptions<RoomieManagerDBContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrioritiesModel>()
                .HasKey(p => new { p.typeID, p.roommieID });
        }
    }
    
}