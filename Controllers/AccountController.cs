using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using mvc.Models;
using mvc.ViewModels;

public class AccountController : Controller
{
  private readonly SignInManager<TeacherModel> _signInManager;
  private readonly UserManager<TeacherModel> _userManager;

  public AccountController(SignInManager<TeacherModel> signInManager, UserManager<TeacherModel> userManager)
  {
    _signInManager = signInManager;
    _userManager   = userManager;
  }

  [HttpGet]
  public IActionResult Register()
  {
    return View(new AccountViewModel());
  }

  [HttpPost]
  public async Task<IActionResult> Register(AccountViewModel model)
  {
    
    var user = new TeacherModel
      {
        Email = model.Firstname + "_" + model.Lastname + "@school.com",
        PasswordHash = model.PasswordHashed,
        Firstname = model.Firstname,
        Lastname = model.Lastname,
        UserName = model.Firstname.ToLower() + "_" + model.Lastname.ToLower()
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
      return RedirectToAction("Index", "Home");
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

