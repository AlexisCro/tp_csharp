using System.ComponentModel.DataAnnotations;

namespace mvc.Models;

public class StudentModel
{
  public int Id { get; set; }

  [Required]
  public string Firstname { get; set; }

  [Required]
  public string Lastname { get; set; }
  public string Email { get; set; } = string.Empty;
  public int Age { get; set; }
}