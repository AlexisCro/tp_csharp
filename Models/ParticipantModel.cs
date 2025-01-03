using mvc.Data;

namespace mvc.Models;

public class ParticipantModel
{
    public readonly ApplicationDbContext _context;
    public ParticipantModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public int EventId { get; set; }
    public EventModel Event { get; set; }
    public string StudentId { get; set; } // UUID so we need to use string
    public StudentModel Student { get; set; }
}