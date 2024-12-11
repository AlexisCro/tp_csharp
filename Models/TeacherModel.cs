using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace mvc.Models;

public class TeacherModel : IdentityUser
{
  [Required]
  public string Firstname { get; set; }

  [Required]
  public string Lastname { get; set; }

}