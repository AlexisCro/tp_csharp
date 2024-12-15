using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace mvc.Models
{
  public class UserModel : IdentityUser
  {
    [Required]
    public string Firstname { get; set; }

    [Required]
    public string Lastname { get; set; }


    // Define the association between a User and a Role
    // Here we are using the RoleId as a foreign key
    // And the relation is optional
    public int? RoleId { get; set; }
    public RoleModel? Role { get; set; }
  }
}