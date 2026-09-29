using HelpDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Attachment)
                .WithMany()
                .HasForeignKey(t => t.AttachmentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Priority)
                .WithMany()
                .HasForeignKey(t => t.PriorityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Status)
                .WithMany()
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.CreatedBy)
                .WithMany()
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Assigned)
                .WithMany()
                .HasForeignKey(t => t.AssignedId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Ticket>()
               .HasOne(t => t.Team)
               .WithMany()
               .HasForeignKey(t => t.TeamId)
               .OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<Ticket> Tickets => Set<Ticket>();

        public DbSet<Attachment> Attachments => Set<Attachment>();

        public DbSet<Comment> Comments => Set<Comment>();

        public DbSet<Priority> Priority => Set<Priority>();

        public DbSet<Status> Status => Set<Status>();

        public DbSet<Tag> Tags => Set<Tag>();

        public DbSet<Team> Teams => Set<Team>();

        public DbSet<User> Users => Set<User>();

    }
}
