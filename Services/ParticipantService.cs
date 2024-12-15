using Microsoft.AspNetCore.Identity;
using mvc.Models;
using mvc.Data;
using Microsoft.EntityFrameworkCore;

namespace mvc.Services;

public class ParticipantService : UserService
{
    private readonly UserManager<UserModel> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _context;

    public ParticipantService(UserManager<UserModel> userManager, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(userManager, httpContextAccessor, context)
    {
        _userManager         = userManager;
        _context             = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> IsParticipant(int eventId, string userId)
    {
        var participant = await _context.Participants.FirstOrDefaultAsync(p => p.EventId == eventId && p.StudentId == userId);
        return participant != null;
    }
}