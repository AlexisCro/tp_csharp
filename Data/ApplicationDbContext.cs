using Microsoft.EntityFrameworkCore;
using mvc.Models;

namespace mvc.Data;

public class ApplicationDbContext : DbContext
{
  public DbSet<TeacherModel> Teachers { get; set; }

  public DbSet<StudentModel> Students { get; set; }

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
  }
}