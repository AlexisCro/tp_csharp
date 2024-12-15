using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using mvc.Models;

namespace mvc.Data;

public class ApplicationDbContext : IdentityDbContext<UserModel>
{
  public DbSet<UserModel> Users { get; set; }
  public DbSet<StudentModel> Students { get; set; }
  public DbSet<TeacherModel> Teachers { get; set; }
  public DbSet<EventModel> Events { get; set; }
  public DbSet<RoleModel> Roles { get; set; }
  public DbSet<ParticipantModel> Participants { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    builder.Entity<UserModel>(entity    => { entity.ToTable("Users"); });
    builder.Entity<StudentModel>(entity => { entity.ToTable("Students"); });
    builder.Entity<TeacherModel>(entity => { entity.ToTable("Teachers"); });
    builder.Entity<EventModel>(entity   => { entity.ToTable("Events"); });
    builder.Entity<RoleModel>(entity    => { entity.ToTable("Roles"); });
    builder.Entity<ParticipantModel>(entity => { entity.ToTable("Participants"); });
    builder.Entity<ParticipantModel>().HasKey(participant => new { participant.EventId, participant.StudentId });

    // Define the relation Has_many events
    // A student can be registered to many events but it's optional
    builder.Entity<ParticipantModel>()
      .HasOne(p => p.Student)
      .WithMany(s => s.Participants)
      .HasForeignKey(p => p.StudentId);

    // Define the relation Has_many participants
    // An event can have many participants
    builder.Entity<ParticipantModel>()
      .HasOne(p => p.Event)
      .WithMany(e => e.Participants)
      .HasForeignKey(p => p.EventId);
  }

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
  }
}