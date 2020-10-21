using Microsoft.EntityFrameworkCore;
using OpenWebData.Data.Contact;
using OpenWebData.Data.LinkContactSkill;
using OpenWebData.Data.Skill;

namespace OpenWebData.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {   
            
        }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<LinkContactSkill> LinkContactSkill { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Firstname).IsRequired();

                entity.Property(e => e.Lastname).IsRequired();
            });
            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Level).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });
            modelBuilder.Entity<LinkContactSkill>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.IdContact).IsRequired();

                entity.Property(e => e.IdSkill).IsRequired();
            });
        }

    }
}
