using Microsoft.AspNetCore.Identity;
using mvc.Models;
using mvc.Data;
using Microsoft.EntityFrameworkCore;

namespace mvc.Services;

public class UserService
{
    private readonly UserManager<UserModel> _userManager;
    private readonly SignInManager<UserModel> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _context;

    public UserService(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
    {
        _userManager         = userManager;
        _signInManager       = signInManager;
        _httpContextAccessor = httpContextAccessor;
        _context             = context;
    }

    public async Task<UserModel> GetUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user;
    }

    public async Task<UserModel> GetUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user;
    }

    public async Task<IdentityResult> DeleteUser(UserModel user)
    {
        var result = await _userManager.DeleteAsync(user);
        return result;
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