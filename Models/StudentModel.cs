using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace mvc.Models;

public class StudentModel : UserModel
{
  // Define the relation Has_many events
  // A student can be registered to many events but it's optional
  public ICollection<ParticipantModel> Participants { get; set; }
}
