namespace mvc.Models;

public class ParticipantModel
{
    public int EventId { get; set; }
    public EventModel Event { get; set; }
    public string StudentId { get; set; } // UUID so we need to use string
    public StudentModel Student { get; set; }
}