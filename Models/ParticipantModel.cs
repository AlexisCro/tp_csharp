namespace mvc.Models;
// TODO : In the table we have StudentId1, I think it's because we use a student Id as string (UUID) and not as an integer

public class ParticipantModel
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string StudentId { get; set; }
    public EventModel Event { get; set; }
    public StudentModel Student { get; set; }
}