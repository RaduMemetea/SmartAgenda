using DataModels;
using FrontEnd.Areas.Identity.Data;
using FrontEnd.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd.Data
{
    public class IdentityDBContext : IdentityDbContext<User>
    {
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<UserAgenda>()
               .HasKey(k => new { k.UserId, k.ConferenceId, k.SessionId, k.TalkId });

            builder.Entity<UserAgenda>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(fk => fk.UserId);


            builder.Entity<UserOwnership>()
               .HasKey(k => new { k.UserId, k.ConferenceId });

            builder.Entity<UserOwnership>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(fk => fk.UserId);


        }

        public DbSet<UserAgenda> UserAgenda { get; set; }
        public DbSet<UserOwnership> UserOwnership { get; set; }
    }
}

