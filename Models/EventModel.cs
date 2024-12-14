using System.ComponentModel.DataAnnotations;

namespace mvc.Models;

public class EventModel
{
  public int Id { get; set; }
  [Required]
  [StringLength(100)]
  public string Title { get; set; }
  [Required]
  [StringLength(500)]
  public string Description { get; set; }
  [Required]
  [Display(Name = "Date de l'événement")]
  [DataType(DataType.DateTime)]
  public DateTime EventDate { get; set; }
  [Required]
  [Range(10, 200)]
  [Display(Name = "Nombre maximum de participants")]
  public int MaxParticipants { get; set; }
  public int ParticipantsCount { get; set; } = 0;

  [Required]
  [StringLength(100)]
  public string Location { get; set; }
  [Display(Name = "Date de création")]
  public DateTime CreatedAt { get; set; } = DateTime.Now;

  // Define the relation has_many student
  // An event can have many students registered but it's optional
  public ICollection<ParticipantModel> Participants { get; set; } = new List<ParticipantModel>();

}