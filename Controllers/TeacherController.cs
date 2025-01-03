using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using mvc.Models;
using mvc.Services;

namespace mvc.Controllers;

public class TeacherController : Controller
{
  private readonly UserManager<TeacherModel> _userManager;
  private readonly UserService _userService;

  // Constructeur and connect to the database
  public TeacherController(UserManager<TeacherModel> userManager, UserService userService)
  {
      _userManager = userManager;
      _userService = userService;
  }

  public async Task<ActionResult> Index()
  {
    var teachers = _userManager.Users;
    return View(teachers.ToList());
  }

  public async Task<ActionResult> Show(string id)
  {
    var teacher = await _userManager.FindByIdAsync(id);
    return View(teacher);
  }

  public async Task<ActionResult> Edit(string id)
  {
    if (!await _userService.GetCurrentUserIsTeacher())
    {
      TempData["Error"] = "Vous n'êtes pas autorisé à modifier un Professeur";
      return RedirectToAction("Index", "Home");
    }

    var teacher = await _userManager.FindByIdAsync(id);
    return View(teacher);
  }

  public async Task<ActionResult> Update(TeacherModel teacher)
  {
    if (!await _userService.GetCurrentUserIsTeacher())
    {
      TempData["Error"] = "Vous n'êtes pas autorisé à modifier un Professeur";
      return RedirectToAction("Index", "Home");
    }

    if (!ModelState.IsValid)
    {
      return View("Edit");
    }

    // TODO : See why the teacherToUpdate is null
    var teacherToUpdate = await _userManager.FindByIdAsync(teacher.Id);

    teacherToUpdate.Firstname = teacher.Firstname;
    teacherToUpdate.Lastname  = teacher.Lastname;
    teacherToUpdate.UserName  = teacher.Firstname.ToLower() + "_" + teacher.Lastname.ToLower();
    teacherToUpdate.Email     = teacher.Firstname.ToLower() + "_" + teacher.Lastname.ToLower() + "@school.com";

    var result = await _userManager.UpdateAsync(teacherToUpdate);

    if (!result.Succeeded)
      {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(teacherToUpdate);
      }
    return RedirectToAction("Index");
  }

  public async Task<ActionResult> Delete(string id)
  {
    if (!await _userService.GetCurrentUserIsTeacher())
    {
      TempData["Error"] = "Vous n'êtes pas autorisé à supprimer un Professeur";
      return RedirectToAction("Index", "Home");
    }

    var teacher = await _userManager.FindByIdAsync(id);
    var result  = await _userManager.DeleteAsync(teacher);

    if (!result.Succeeded)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }
      return View("Index");
    }

    return RedirectToAction("Index");
  }
}