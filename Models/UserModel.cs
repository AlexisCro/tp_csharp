using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;

namespace mvc.Models
{
  public class UserModel : IdentityUser
  {
    [Required]
    public string Firstname { get; set; }

    [Required]
    public string Lastname { get; set; }
    public RoleModel Role { get; set; }
  }
}