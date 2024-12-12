using mvc.Data;
using mvc.Models;
using mvc.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace mvc.ViewModels;
public class AccountStudentViewModel
{
  [Required]
  public string Firstname { get; set; }
  [Required]
  public string Lastname { get; set; }
  [Required]
  public string PasswordHashed { get; set; }
  [Required]
  public string ConfirmedPassword { get; set; }
}
