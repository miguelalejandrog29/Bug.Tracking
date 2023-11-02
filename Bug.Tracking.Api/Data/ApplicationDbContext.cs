using Bug.Tracking.Api.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Bug.Tracking.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserBug> UserBugs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            

            #region - Relaciones entre Tablas -
            //one User has many UserBugs
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserBugs)
                .WithOne(ub => ub.User)
                .HasForeignKey(ub => ub.UserId).OnDelete(DeleteBehavior.NoAction);

            //one Project has many UserBugs
            modelBuilder.Entity<Project>()
                .HasMany(p => p.UserBugs)
                .WithOne(ub => ub.Project)
                .HasForeignKey(ub => ub.ProjectId).OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region - Auto generated column -
            modelBuilder.Entity<UserBug>()
                .Property(ub => ub.CreatedDate)
                .HasDefaultValueSql("getdate()");
            #endregion

            #region - Data Seeding -
            modelBuilder.Entity<User>().
                HasData(
                new { Id = (long)1, Name = "Federico", Surname = "Guerra" },
                new { Id = (long)2, Name = "Frank", Surname = "Montalvo" },
                new { Id = (long)3, Name = "Adriel", Surname = "Chacon" },
                new { Id = (long)4, Name = "Ana", Surname = "Olivera" },
                new { Id = (long)5, Name = "Guillen", Surname = "Gonzalez" }
                );

            modelBuilder.Entity<Project>().
                HasData(
                new { Id = (long)1, Name = "Compact", Description = "sdfhkjdghdkfhgkdjf" },
                new { Id = (long)2, Name = "Fritz", Description = "etertbewt etrwtrwtwetwe wetwertw etwert" },
                new { Id = (long)3, Name = "4una", Description = "Cha4356456b 456453b56bw4srtwecon" },
                new { Id = (long)4, Name = "Diler", Description = "563b456bery y tyertyertyhf ggh dfghuye" },
                new { Id = (long)5, Name = "Brench", Description = "4564 rtgwtywey wage rte dfd fgdfgertewrwer werwerwerwerfsdfsdlez" }
                );
            #endregion
        }
    }
}
