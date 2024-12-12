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

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    builder.Entity<UserModel>(entity    => { entity.ToTable("Users"); });
    builder.Entity<StudentModel>(entity => { entity.ToTable("Students"); });
    builder.Entity<TeacherModel>(entity => { entity.ToTable("Teachers"); });
  }

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
  }
}