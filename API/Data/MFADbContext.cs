using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MFADbContext : DbContext
    {
        public MFADbContext(DbContextOptions<MFADbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                .IsUnique();

                entity.HasKey(e => e.Id);

                entity.HasData(new User
                {
                    AuthenCode = "000000",
                    UserName = "user1",
                    Password = BCrypt.Net.BCrypt.HashPassword("Password"),
                    CodeExpireTime = DateTime.UtcNow.AddMinutes(1),
                    FullName = "Test user one",
                    Id = Guid.NewGuid()
                }, new User
                {
                    AuthenCode = "000000",
                    UserName = "user2",
                    Password = BCrypt.Net.BCrypt.HashPassword("Password"),
                    CodeExpireTime = DateTime.UtcNow.AddMinutes(1),
                    FullName = "Test user two",
                    Id = Guid.NewGuid()
                });
            });
        }
    }
}
