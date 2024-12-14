using System.ComponentModel.DataAnnotations;

namespace mvc.ViewModels;
public class AccountViewModel
{
  [Required]
  public string Firstname { get; set; }
  [Required]
  public string Lastname { get; set; }
  [Required]
  public string PasswordHashed { get; set; }
  [Required]
  public string ConfirmedPassword { get; set; }
  public string Name { get; set; }
}
