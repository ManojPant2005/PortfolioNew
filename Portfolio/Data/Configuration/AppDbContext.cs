using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Models;

namespace Portfolio.Data.Configuration
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Registration> Registrations { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<NewPost> NewPosts { get; set; }

        public DbSet<Review> Reviews { get; set; }  
        public DbSet<Contact> Contacts { get; set; }    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine);
#endif
        }


    }
}
