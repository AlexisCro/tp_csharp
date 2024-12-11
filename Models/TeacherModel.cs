using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace mvc.Models;

public class TeacherModel : IdentityUser
{
  // Managed by Identity
  // public int Id { get; set; }

  [Required]
  public string Firstname { get; set; }

  [Required]
  public string Lastname { get; set; }

  // Managed by Identity
  // public string Email { get; set; } = string.Empty;

}