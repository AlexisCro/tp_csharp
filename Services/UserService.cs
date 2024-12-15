using Microsoft.AspNetCore.Identity;
using mvc.Models;
using mvc.Data;
using Microsoft.EntityFrameworkCore;

namespace mvc.Services;

public class UserService
{
    private readonly UserManager<UserModel> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _context;

    public UserService(UserManager<UserModel> userManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
    {
        _userManager         = userManager;
        _httpContextAccessor = httpContextAccessor;
        _context             = context;
    }

    public async Task<UserModel> GetCurrentStudent()
    {
        var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        if (currentUser == null)
        {
            return null;
        }
        else if (currentUser.RoleId == 1)
        {
            return null;
        }
        else
        {
            return currentUser;
        }
    }

    public async Task<string> GetCurrentUserId()
    {
        var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        if (currentUser == null)
        {
            return null;
        }
        else
        {
            return currentUser.Id;
        }
    }

    public async Task<bool> GetCurrentUserIsTeacher()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        if (user == null)
        {
            return false;
        }
        var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        var roleCurrentUser = await _context.Roles.FirstOrDefaultAsync(r => r.Id == currentUser.RoleId);
        return roleCurrentUser?.Name == "Teacher" ? true : false;
    }
}