using DataModels;
using DataModels.Complex;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BackEnd.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Session_Chair>()
                .HasKey(k => new { k.SessionID, k.PersonID });

            modelBuilder.Entity<Session_Chair>()
                .HasOne<Session>()
                .WithMany()
                .HasForeignKey(fk => fk.SessionID);

            modelBuilder.Entity<Session_Chair>()
                .HasOne<Person>()
                .WithMany()
                .HasForeignKey(fk => fk.PersonID);


            modelBuilder.Entity<Session_Talks>()
                .HasKey(k => new { k.SessionID, k.TalkID });

            modelBuilder.Entity<Session_Talks>()
                .HasOne<Session>()
                .WithMany()
                .HasForeignKey(fk => fk.SessionID);

            modelBuilder.Entity<Session_Talks>()
                .HasOne<Talk>()
                .WithMany()
                .HasForeignKey(fk => fk.TalkID);


            modelBuilder.Entity<Talk_Persons>()
                .HasKey(k => new { k.TalkID, k.PersonID });

            modelBuilder.Entity<Talk_Persons>()
                .HasOne<Talk>()
                .WithMany()
                .HasForeignKey(fk => fk.TalkID);

            modelBuilder.Entity<Talk_Persons>()
                .HasOne<Person>()
                .WithMany()
                .HasForeignKey(fk => fk.PersonID);


            modelBuilder.Entity<Conference_Tags>()
                .HasKey(k => new { k.ConferenceID, k.TagID });

            modelBuilder.Entity<Conference_Tags>()
                .HasOne<Conference>()
                .WithMany()
                .HasForeignKey(s => s.ConferenceID);

            modelBuilder.Entity<Conference_Tags>()
                .HasOne<Tag>()
                .WithMany()
                .HasForeignKey(s => s.TagID);

            modelBuilder.Entity<Session>()
               .HasOne<Location>()
               .WithMany()
               .HasForeignKey(s => s.LocationID);

            modelBuilder.Entity<Session>()
                .HasOne<Conference>()
                .WithMany()
                .HasForeignKey(s => s.ConferenceID);

        }
        public DbSet<Location> Location { get; set; }
        public DbSet<Conference> Conference { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Session_Chair> Session_Chair { get; set; }
        public DbSet<Talk> Talk { get; set; }
        public DbSet<Session_Talks> Session_Talks { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Conference_Tags> Conference_Tags { get; set; }
        public DbSet<Talk_Persons> Talk_Persons { get; set; }

    }
}
