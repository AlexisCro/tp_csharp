using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mvc.Models;
using mvc.Data;
using mvc.ViewModels;

public class AccountController : Controller
{
  private readonly SignInManager<UserModel> _signInManager;
  private readonly UserManager<UserModel> _userManager;
  private readonly ApplicationDbContext _context;

  public AccountController(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager, ApplicationDbContext context)
  {
    _signInManager = signInManager;
    _userManager   = userManager;
    _context       = context;
  }

  [HttpGet]
  public IActionResult Register()
  {
    return View(new AccountViewModel());
  }

  [HttpPost]
  public async Task<IActionResult> Register(AccountViewModel model)
  {
    if (_context.Roles.FirstOrDefault(r => r.Name == "Teacher") == null)
    {
      _context.Roles.Add(new RoleModel { Name = "Teacher" });
      _context.SaveChanges();

    }

    var user = new TeacherModel
      {
        Email        = model.Firstname + "_" + model.Lastname + "@school.com",
        PasswordHash = model.PasswordHashed,
        Firstname    = model.Firstname,
        Lastname     = model.Lastname,
        UserName     = model.Firstname.ToLower() + "_" + model.Lastname.ToLower(),
        Role         = await _context.Roles.FirstOrDefaultAsync(role => role.Name == "Teacher"),
      };

    var result = await _userManager.CreateAsync(user, model.PasswordHashed);

    if (!ModelState.IsValid || !result.Succeeded)
    {
      return View(model);
    }

    foreach (var error in result.Errors)
    {
        ModelState.AddModelError(string.Empty, error.Description);
    }


    if (result.Succeeded)
    {
      await _signInManager.SignInAsync(user, isPersistent: false);
      return RedirectToAction("Index", "Home");
    }

    return View(model);
  }

  public IActionResult RegisterStudent()
  {
    return View(new AccountStudentViewModel());
  }

[HttpPost]
  public async Task<IActionResult> RegisterStudent(AccountStudentViewModel model)
  {
    if (_context.Roles.FirstOrDefault(r => r.Name == "Student") == null)
    {
      _context.Roles.Add(new RoleModel { Name = "Student" });
      _context.SaveChanges();

    }

    var user = new StudentModel
      {
        Email        = model.Firstname + "_" + model.Lastname + "@school.com",
        PasswordHash = model.PasswordHashed,
        Firstname    = model.Firstname,
        Lastname     = model.Lastname,
        UserName     = model.Firstname.ToLower() + "_" + model.Lastname.ToLower(),
        Role         = await _context.Roles.FirstOrDefaultAsync(role => role.Name == "Student"),
      };


    var result = await _userManager.CreateAsync(user, model.PasswordHashed);
    if (!ModelState.IsValid || !result.Succeeded)
    {
      return View(model);
    }

    foreach (var error in result.Errors)
    {
      ModelState.AddModelError(string.Empty, error.Description);
    }

    return View(model);
  }

  [HttpGet]
  public IActionResult Login()
  {
      return View();
  }

  [HttpPost]
  public async Task<IActionResult> Login(LoginViewModel model)
  {
    if (!ModelState.IsValid)
    {
      return View(model);
    }

    var result = await _signInManager.PasswordSignInAsync(
      model.Username,
      model.PasswordHashed,
      false,
      false
    );

    if (result.Succeeded)
    {
      return RedirectToAction("Index", "Home", model);
    }

    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    return RedirectToAction("Login", "Account");
  }

  public async Task<IActionResult> Logout()
  {
    await _signInManager.SignOutAsync();
    return RedirectToAction("Index", "Home");
  }
}

