using mvc.Data;
using mvc.Models;
using mvc.ViewModels;

namespace mvc.ViewModels;
public class AccountViewModel
{
  public string Firstname { get; set; }
  public string Lastname { get; set; }
  public string PasswordHashed { get; set; }
  public string ConfirmedPassword { get; set; }
  public string Name { get; set; }
}
